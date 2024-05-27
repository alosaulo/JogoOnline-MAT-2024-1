using Mirror;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
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

    [Server]
    void UpdateScore(string playerId, int newScore, int newDeaths)
    {
        if (Scores.ContainsKey(playerId))
        {
            Scores[playerId] = new Score(newScore, newDeaths);
        }
        else
        {
            Debug.LogWarning("Player ID não encontrado no SyncDictionary.");
        }
    }

    [Command(requiresAuthority = false)]
    public void CmdUpdateScore(string playerId, int newScore, int newDeaths)
    {
        UpdateScore(playerId, newScore, newDeaths);
    }

}
