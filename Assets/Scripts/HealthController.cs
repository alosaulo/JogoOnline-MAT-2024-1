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

    // Start is called before the first frame update
    void Start()
    {
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
            Debug.Log("Morreu!");
        }
    }

}
