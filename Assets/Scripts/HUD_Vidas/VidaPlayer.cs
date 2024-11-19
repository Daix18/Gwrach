using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VidaPlayer : MonoBehaviour
{
    
     public float playerMaxHealth;
      public float playerHealth;
    [SerializeField] private HUD_Vidas healthBar;
    private BossController headkick;
    void Start()
    {
        playerHealth = playerMaxHealth;
        healthBar.InitializeHealthBar(playerHealth, playerMaxHealth);
    }

    // Update is called once per frame
    void Update()
    {
        healthBar.InitializeHealthBar(playerHealth, playerMaxHealth);
        if (playerHealth <= 0)
        {
            Destroy(gameObject);
        }
    }
    
}
