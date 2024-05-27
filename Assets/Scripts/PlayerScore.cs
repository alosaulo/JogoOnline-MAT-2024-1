using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public struct Score
{
    [SyncVar] public int score;
    [SyncVar] public int deaths;

    public Score(int score, int deaths)
    {
        this.score = score;
        this.deaths = deaths;
    }
}

public class PlayerScore : NetworkBehaviour
{
    PlayerController pc;
    ScoreManager sm;

    Score pScore = new Score();

    // Start is called before the first frame update
    void Start()
    {
        if (isLocalPlayer) 
        {
            pc = GetComponent<PlayerController>();
            sm = FindObjectOfType<ScoreManager>();
            sm.CMDAddScore(pc.playerName, pScore);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public int GetScore() 
    {
        return pScore.score;
    }

    public int GetDeath() 
    { 
        return pScore.deaths;
    }

    public void IncrementScore() 
    {
        pScore.score++;
        UpdateScore();
    }

    public void IncrementDeath() 
    {
        pScore.deaths++;
        UpdateScore();
    }

    public void UpdateScore() 
    {
        sm.CmdUpdateScore(pc.playerName, pScore.score, pScore.deaths);
    }

}
