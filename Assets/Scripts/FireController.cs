using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireController : NetworkBehaviour
{
    [SerializeField] GameObject PlayerCam;
    [SerializeField] LayerMask PlayerMask;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1")) 
        {
            Debug.DrawRay(PlayerCam.transform.position, 
                PlayerCam.transform.forward,
                Color.red,
                1);
            if (Physics.Raycast(PlayerCam.transform.position,
                PlayerCam.transform.forward,
                out RaycastHit hit, PlayerMask)) 
            {
                if (hit.collider.TryGetComponent<HealthController>(out HealthController playerHealth)) 
                {
                    if (isServer) 
                    {
                        ServerHit(25, playerHealth);
                        return;
                    }

                    CmdHit(25, playerHealth);
                }
            }
        }
    }

    [Command]
    void CmdHit(float damage, HealthController playerHealth) 
    { 
        ServerHit(damage, playerHealth);
    }

    [Server]
    void ServerHit(float damage, HealthController playerHealth) 
    {
        playerHealth.GetDamage(damage);
    }

}
