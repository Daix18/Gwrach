using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager THIS;

    public GameObject _deathCanvas;

    [Header("Death Stats")]
    [SerializeField]private int deathCount = 0;
    [SerializeField]private float deathMultiplier = 0.025f;

    [Header("Stats Multiplier")]
    [SerializeField]private float currentDamageMultiplier = 1.0f;
    [SerializeField]private float currentHealthMultiplier = 1.0f; 
    [SerializeField]private float currentSpeedMultiplier = 1.0f;

    // Start is called before the first frame update
    void Start()
    {
    }

    private void Awake()
    {
        if (THIS == null)
        {
            THIS = this;
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnPlayerDeath()
    {
        // Increase the death count and scale the multiplier
        deathCount++;
        deathMultiplier = 0.025f * deathCount; // Multiplier scales with each death
    }

    public void UpgradeDamage()
    {
        currentDamageMultiplier += deathMultiplier; // Apply multiplier increment
        float upgradedDamage = AttackController.THIS._playerDamage + currentDamageMultiplier;

        //Apply multiplied value
        AttackController.THIS._playerDamage = upgradedDamage;

        // Apply the upgraded damage
        Debug.Log("Upgraded Damage: " + upgradedDamage);

        //_deathCanvas.SetActive(false);
    }

    public void UpgradeHealth()
    {
        currentHealthMultiplier += deathMultiplier; // Apply multiplier increment
        float upgradedHealth = AttackController.THIS._playerHealth + currentHealthMultiplier;

        //Apply multiplied value
        AttackController.THIS._playerHealth = upgradedHealth;

        // Apply the upgraded health
        Debug.Log("Upgraded Health: " + upgradedHealth);
        
        //_deathCanvas.SetActive(false);
    }

    public void UpgradeSpeed()
    {
        currentSpeedMultiplier += deathMultiplier; // Apply multiplier increment
        float upgradedSpeed = PlayerMovement.THIS._playerSpeed + currentSpeedMultiplier;

        //Apply multiplied value
        PlayerMovement.THIS._playerSpeed= upgradedSpeed;

        // Apply the upgraded speed
        Debug.Log("Upgraded Speed: " + upgradedSpeed);

        //_deathCanvas.SetActive(false);
    }
}
