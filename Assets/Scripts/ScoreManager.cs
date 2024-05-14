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

    public void GetScores() 
    {
        foreach (var score in Scores)
        {
            Debug.Log($"{score.Key} - {score.Value.score}/{score.Value.deaths}");
        }
    }

    [Command(requiresAuthority = false)]
    public void CMDAddScore(string a, Score p) 
    {
        Scores.Add(a, p);
    }
}
