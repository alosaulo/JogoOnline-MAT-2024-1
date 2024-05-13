using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : NetworkBehaviour
{
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
        if(isLocalPlayer)
            CmdRequestScores();
    }

    [Command(requiresAuthority = false)]
    void CmdRequestScores()
    {
        List<string> scores = new List<string>();
        foreach (KeyValuePair<int, NetworkConnectionToClient> conn in NetworkServer.connections)
        {
            if (conn.Value.identity != null)
            {
                PlayerController playerController = conn.Value.identity.gameObject.GetComponent<PlayerController>();
                PlayerScore score = conn.Value.identity.gameObject.GetComponent<PlayerScore>();
                if (score != null)
                {
                    scores.Add($"{playerController.name}: {score.GetScore()}/{score.GetDeath()}");
                }
            }
        }

        TargetReceiveScores(connectionToClient, scores);
    }

    [TargetRpc]
    void TargetReceiveScores(NetworkConnection target, List<string> scores)
    {
        foreach (string score in scores)
        {
            Debug.Log(score);
        }
    }
}
