using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class PlayerController : NetworkBehaviour
{
    CharacterController characterController;

    [SerializeField] Camera playerCamera;

    [SerializeField] GameObject playerModel;

    [SerializeField] Animator animatorFPS;

    [SerializeField] Animator animatorModel;

    [SerializeField] float speed;

    // Start is called before the first frame update
    void Start()
    {
        if (isLocalPlayer)
        {
            playerCamera.gameObject.SetActive(true);
            characterController = GetComponent<CharacterController>();
        }
        else 
        { 
            playerModel.SetActive(true);
        }
    }

    // Update is called once per frame
    void Update()
    {

        if (!isLocalPlayer)
        {
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
}
