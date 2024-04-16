using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using TMPro;

public class PlayerController : NetworkBehaviour
{
    CharacterController characterController;

    HealthController healthController;

    [SerializeField] GameObject pnlRespawn;

    [SerializeField] TextMeshProUGUI txtRespawn;

    [SerializeField] Camera playerCamera;

    [SerializeField] Camera deathCamera;

    [SerializeField] GameObject playerModel;

    [SerializeField] GameObject weaponModel;

    [SerializeField] Animator animatorFPS;

    [SerializeField] Animator animatorModel;

    [SerializeField] float speed;

    [SyncVar]
    float respawnTime;

    Vector3 posInicial;

    // Start is called before the first frame update
    void Start()
    {
        if (isLocalPlayer)
        {
            posInicial = transform.position;
            playerCamera.gameObject.SetActive(true);
            characterController = GetComponent<CharacterController>();
            healthController = GetComponent<HealthController>();
            playerModel.SetActive(false);
            weaponModel.SetActive(false);
        }
        else 
        {
            playerCamera.gameObject.SetActive(false);
            playerModel.SetActive(true);
            weaponModel.SetActive(true);
        }
    }

    // Update is called once per frame
    void Update()
    {

        if (!isLocalPlayer)
        {
            return;
        }

        if (healthController.isDead()) 
        {
            respawnTime += Time.deltaTime;
            txtRespawn.text = respawnTime.ToString();
            if (respawnTime >= 3) 
            { 
                respawnTime = 0;
                Respawn();
            }
            return;
        }

        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");

        float vAxis = Input.GetAxis("Vertical");
        float hAxis = Input.GetAxis("Horizontal");

        if (Mathf.Abs(vAxis) > 0 || Mathf.Abs(hAxis) > 0)
        {
            animatorFPS.SetBool("walk", true);
            animatorModel.SetBool("walk", true);
        }
        else
        {
            animatorFPS.SetBool("walk", false);
            animatorModel.SetBool("walk", false);
        }

        transform.Rotate(Vector3.up * mouseX);

        playerCamera.gameObject.transform.Rotate(Vector3.left * mouseY);
        
        Vector3 movement = (transform.right * hAxis) + (transform.forward * vAxis);

        characterController.Move(movement * speed * Time.deltaTime);

    }

    public void Die() 
    {
        animatorModel.SetBool("die", true);
        if (isLocalPlayer) 
        {
            pnlRespawn.SetActive(true);
            playerCamera.gameObject.SetActive(false);
            playerModel.SetActive(true);
            deathCamera.gameObject.SetActive(true);
            weaponModel.SetActive(true);
        }
    }

    public void Respawn() 
    {
        animatorModel.SetBool("die", false);
        animatorModel.SetTrigger("respawn");
        healthController.CmdRecover();
        if (isLocalPlayer)
        {
            pnlRespawn.SetActive(false);
            transform.position = posInicial;
            playerCamera.gameObject.SetActive(true);
            playerModel.SetActive(false);
            deathCamera.gameObject.SetActive(false);
            weaponModel.SetActive(false);
        }
    }

}
