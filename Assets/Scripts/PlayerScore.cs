using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public struct Score
{
    [SyncVar] public string nickName;
    [SyncVar] public int kills;
    [SyncVar] public int deaths;

    public Score(string nickName, int kills, int deaths)
    {
        this.nickName = nickName;
        this.kills = kills;
        this.deaths = deaths;
    }
}

public class PlayerScore : NetworkBehaviour
{
    PlayerController pc;
    ScoreManager sm;

    [SyncVar] Score pScore = new Score();

    // Start is called before the first frame update
    void Start()
    {
        if (isLocalPlayer) 
        {
            pc = GetComponent<PlayerController>();
            sm = FindObjectOfType<ScoreManager>();
            pScore.nickName = pc.playerName;
            sm.CMDAddScore(pScore);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public int GetScore() 
    {
        return pScore.kills;
    }

    public int GetDeath() 
    { 
        return pScore.deaths;
    }

    public void IncrementScore() 
    {
        pScore.kills++;
        UpdateScore();
    }

    public void IncrementDeath() 
    {
        pScore.deaths++;
        UpdateScore();
    }

    public void UpdateScore() 
    {
        sm.CmdUpdateScore(pc.playerName, pScore.kills, pScore.deaths);
    }

}
