using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using static UnityEditor.Experimental.GraphView.GraphView;

public class GameManager : MonoBehaviour
{
    public static GameManager THIS;

    public GameObject _deathCanvas;

    public GameObject player;

    public Player_ST playerST;
    public VidaPlayer vidaPlayer;

    [Header("Death Stats")]
    [SerializeField]private int deathCount = 0;
    [SerializeField]private float deathMultiplier = 0.025f;

    [Header("Stats Multiplier")]
    [SerializeField]private float currentDamageMultiplier = 1.0f;
    [SerializeField]private float currentHealthMultiplier = 1.0f; 
    [SerializeField]private float currentSpeedMultiplier = 1.0f;

    [Header("Zona0 Booleanos")]
    public bool _firstNPC;
    public bool _secondNPC;

    private float vida = 0;
    private float damage = 0;
    private float speed = 0;

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

        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player");
            playerST = player.GetComponent<Player_ST>();
            vidaPlayer = player.GetComponent<VidaPlayer>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player");
            playerST = player.GetComponent<Player_ST>();
            vidaPlayer = player.GetComponent<VidaPlayer>();

            if (vida == 0)
            {
                vida = vidaPlayer.playerMaxHealth;
                damage = playerST.damage;
                speed = playerST.playerSpeed;
            }
            else
            {
                vidaPlayer.playerMaxHealth = vida;
                vidaPlayer.playerHealth = vida;
                playerST.damage = damage;   
                playerST.playerSpeed = speed;   
            }
        }
    }

    public void OnPlayerDeath()
    {
        // Increase the death count and scale the multiplier
        deathCount++;
        deathMultiplier = 0.025f * deathCount; // Multiplier scales with each death
        vidaPlayer.playerHealth = vidaPlayer.playerMaxHealth;
        Death();

    }

    public void UpgradeDamage()
    {
        currentDamageMultiplier += deathMultiplier; // Apply multiplier increment
        float upgradedDamage = damage + currentDamageMultiplier;

        //Apply multiplied value
        damage = upgradedDamage;

        // Apply the upgraded damage
        Debug.Log("Upgraded Damage: " + upgradedDamage);
        Time.timeScale = 1f;
        _deathCanvas.SetActive(false);
        LoadScene(1);
    }

    public void UpgradeHealth()
    {
        currentHealthMultiplier += deathMultiplier; // Apply multiplier increment
        float upgradedHealth = vida + currentHealthMultiplier;

        //Apply multiplied value
        vida = upgradedHealth;

        // Apply the upgraded health
        Debug.Log("Upgraded Health: " + upgradedHealth);
        
        _deathCanvas.SetActive(false);
        Time.timeScale = 1f;
        LoadScene(1);
    }

    public void UpgradeSpeed()
    {
        currentSpeedMultiplier += deathMultiplier; // Apply multiplier increment
        float upgradedSpeed = speed + currentSpeedMultiplier;

        //Apply multiplied value
        speed = upgradedSpeed;

        // Apply the upgraded speed
        Debug.Log("Upgraded Speed: " + upgradedSpeed);

        _deathCanvas.SetActive(false);
        Time.timeScale = 1f;
        LoadScene(1);
    }
    public void LoadScene(int _sceneId)
    {
        SceneManager.LoadScene(_sceneId);
    }

    public void Death()
    {
        _deathCanvas.SetActive(true);
        Time.timeScale = 0f;
    }
}
