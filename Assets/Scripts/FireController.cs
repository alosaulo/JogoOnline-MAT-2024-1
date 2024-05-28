using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireController : NetworkBehaviour
{
    [SerializeField] GameObject PlayerCam;
    [SerializeField] LayerMask PlayerMask;
    [SerializeField] PlayerScore playerScore;

    // Start is called before the first frame update
    void Start()
    {
        
    }

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
                    CmdHit(25, playerHealth.netId);
                }
            }
        }
    }

    [Command]
    void CmdHit(float damage, uint targetNetId)
    {
        if (NetworkServer.spawned.TryGetValue(targetNetId, out NetworkIdentity targetIdentity) && targetIdentity != null)
        {
            HealthController playerHealth = targetIdentity.GetComponent<HealthController>();
            if (playerHealth != null)
            {
                ServerHit(damage, playerHealth, connectionToClient);
            }
        }
    }

    [Server]
    void ServerHit(float damage, HealthController playerHealth, NetworkConnectionToClient attackerConnection)
    {
        playerHealth.GetDamage(damage);
    }


}
