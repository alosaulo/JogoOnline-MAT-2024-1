using Mirror;
using TMPro;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthController : NetworkBehaviour
{
    [SerializeField] GameObject Canvas;
    [SerializeField] Slider slider;
    [SerializeField] TextMeshProUGUI healthText;
    [SerializeField][SyncVar(hook = nameof(HealthValueChanged))] float health;
    [SerializeField] PlayerController playerController;
    float maxHealth;


    // Start is called before the first frame update
    void Start()
    {
        maxHealth = health;

        if (!isLocalPlayer) { 
            Canvas.SetActive(false);
            return;
        }
        Canvas.SetActive(true);
        healthText.text = health.ToString();
        slider.maxValue = health;
        slider.value = health;
        slider.minValue = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    [Server]
    public void GetDamage(float damage) 
    {
        health -= damage;
    }

    void HealthValueChanged(float oldHealth, float newHealth) 
    {
        slider.value = health;
        healthText.text = health.ToString();
        if (health <= 0)
        {
            playerController.Die();
        }
    }

    public bool isDead() 
    {
        return health <= 0;
    }

    [Command]
    public void CmdRecover() 
    {
        Recover();
    }

    [Server]
    void Recover() 
    {
        health = maxHealth;
    }

}
