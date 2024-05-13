using Mirror;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoundController : NetworkBehaviour
{
    [SerializeField] float roundTime;
    [HideInInspector][SyncVar] public float time;

    // Start is called before the first frame update
    void Start()
    {
        time = roundTime;
    }

    // Update is called once per frame
    void Update()
    {
        time -= Time.deltaTime;
    }

    public string GetFormatedTime() 
    { 
        TimeSpan ts = TimeSpan.FromSeconds(time);
        return ts.ToString("mm':'ss");
    }
}
