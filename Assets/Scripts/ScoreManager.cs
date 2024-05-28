using Mirror;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;

public class ScoreManager : NetworkBehaviour
{

    public SyncList<Score> Scores = new SyncList<Score>();

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
            s += $"{score.nickName} - {score.kills}/{score.deaths} \n";
        }
        
        return s;
    }

    [Command(requiresAuthority = false)]
    public void CMDAddScore(Score p) 
    {
        Scores.Add(p);
    }

    [Command(requiresAuthority = false)]
    public void CMDRemoveScore(string nickName)
    {
        Score score = Scores.Find(x => x.nickName == nickName);
        Scores.Remove(score);
    }

    [Server]
    void UpdateScore(string nickName, int newScore, int newDeaths)
    {
        Score score = Scores.Find(x => x.nickName == nickName);
        score.kills = newScore;
        score.deaths = newDeaths;
    }

    [Command(requiresAuthority = false)]
    public void CmdUpdateScore(string playerId, int newScore, int newDeaths)
    {
        UpdateScore(playerId, newScore, newDeaths);
    }

}
