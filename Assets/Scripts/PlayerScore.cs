using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public struct Score
{
    [SerializeField][SyncVar]public int score;

    [SerializeField][SyncVar]public int deaths;
}

public class PlayerScore : NetworkBehaviour
{

    Score pScore = new Score();

    // Start is called before the first frame update
    void Start()
    {
        if (isLocalPlayer) 
        {
            PlayerController pc = GetComponent<PlayerController>();
            ScoreManager sm = FindObjectOfType<ScoreManager>();
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
    }

    public void IncrementDeath() 
    {
        pScore.deaths++;
    }

}
