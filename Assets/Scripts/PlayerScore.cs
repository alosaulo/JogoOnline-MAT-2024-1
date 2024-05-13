using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScore : NetworkBehaviour
{

    [SerializeField][SyncVar] int score;

    [SerializeField][SyncVar] int deaths;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public int GetScore() 
    {
        return score;
    }

    public int GetDeath() 
    { 
        return deaths;
    }

    public void IncrementScore() 
    {
        score++;
    }

    public void IncrementDeath() 
    {
        deaths++;
    }

}
