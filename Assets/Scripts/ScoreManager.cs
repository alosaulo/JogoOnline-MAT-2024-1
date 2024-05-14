using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : NetworkBehaviour
{
    private ScoreManager scoreManager;

    public SyncDictionary<string, Score> Scores = new SyncDictionary<string, Score>();

    private void Awake()
    {
        scoreManager = GameObject.FindObjectOfType<ScoreManager>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public string GetScores() 
    {
        string s = "";
        
        foreach (var score in Scores)
        {
            s += $"{score.Key} - {score.Value.score}/{score.Value.deaths} \n";
        }
        
        return s;
    }

    [Command(requiresAuthority = false)]
    public void CMDAddScore(string a, Score p) 
    {
        Scores.Add(a, p);
    }

    [Command(requiresAuthority = false)]
    public void CMDRemoveScore(string a)
    {
        Scores.Remove(a);
    }

}
