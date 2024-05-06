using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Mirror;

public class LoginManager : MonoBehaviour
{

    public TMP_InputField nicknameInput;
    public NetworkManager networkManager;

    // Start is called before the first frame update
    void Start()
    {
        networkManager = FindObjectOfType<NetworkManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LoginClick() 
    { 
        string nickname = nicknameInput.text;
        if (!string.IsNullOrWhiteSpace(nickname))
        {
            networkManager.StartClient();
        }
        else 
        {
            Debug.Log("Nickname fora do padrão");
        }
    }

}
