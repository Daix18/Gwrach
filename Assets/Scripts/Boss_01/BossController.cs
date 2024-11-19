using System.Collections;
using UnityEngine;

public class BossController : MonoBehaviour
{
    private Animator anim;
    private Transform player;
    private VidaPlayer playerPlayer;
    
    
    [Header("General Settings")]
    [SerializeField] private float detectionRange = 6f;
    [SerializeField] private float attackRange = 2f;
    [SerializeField] private float walkSpeed = 2f;
    [SerializeField] private float bossMaxHealth;
    [SerializeField] private float bossHealth;
    [SerializeField] private HUD_Vidas healthBar;
    public Transform shootingControler;
    public GameObject gasObject;

    [SerializeField] private float health = 3;

    [Header("Attack Timers")]
    [SerializeField] private Vector2 attackCooldownRange = new Vector2(3f, 8f);
    private float attack1Cooldown;
    private float attack2Cooldown;
    private bool attackTutorialCompleted = false;
    private bool playerInAttackRange = false;
    private bool isAttacking = false;
    public int attack2Damage;

    public Transform attackCheck;
    public float attackCheckRadius;

    [Header("Canvas Tutorial")]
    [SerializeField] private GameObject dodgeTutorialCanvas;

    private bool isIdle = true;
    private bool playerDetected = false;
    private Vector3 initialPosition;

    private int attack1RepeatCount = 0; 

    private void Start()
    {
        bossHealth = bossMaxHealth;
        healthBar.InitializeHealthBar(bossHealth, bossMaxHealth);
        
        anim = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        initialPosition = transform.position;
        ResetAttackCooldowns();
    }

    private void Update()
    {
        healthBar.InitializeHealthBar(bossHealth, bossMaxHealth);
        float distanceToPlayer = Vector2.Distance(transform.position, player.position);

        if (isAttacking) return; 

        if (distanceToPlayer <= detectionRange)
        {
            playerDetected = true;

            if (distanceToPlayer > attackRange)
            {
                WalkTowardsPlayer();
            }
            else
            {
                StopAndAttack(distanceToPlayer);
            }
        }
        else
        {
            if (playerDetected)
            {
                playerDetected = false;
                StartIdle();
            }
        }

        if (playerInAttackRange)
        {
            UpdateAttackTimers();
        }

    }
    

    private void WalkTowardsPlayer()
    {
        if (isIdle)
        {
            isIdle = false;
            anim.SetBool("isWalking", true);
        }

       
        Vector3 direction = (player.position - transform.position).normalized;
        direction.y = 0; 

        
        transform.position += direction * walkSpeed * Time.deltaTime;

        
        FacePlayer();
    }

    private void StopAndAttack(float distanceToPlayer)
    {
        anim.SetBool("isWalking", false);
        isIdle = true;

        if (distanceToPlayer <= attackRange && !playerInAttackRange)
        {
            playerInAttackRange = true;

            if (!attackTutorialCompleted)
            {
                StartCoroutine(HandleTutorial());
            }
        }
    }

    private void FacePlayer()
    {
        if (player.position.x > transform.position.x && transform.localScale.x < 0 ||
            player.position.x < transform.position.x && transform.localScale.x > 0)
        {
            Vector3 flipped = transform.localScale;
            flipped.x *= -1;
            transform.localScale = flipped;
        }
    }

    private void StartIdle()
    {
        anim.SetBool("isWalking", false);
        isIdle = true;
    }

    private IEnumerator HandleTutorial()
    {
        Time.timeScale = 0; 
        dodgeTutorialCanvas.SetActive(true);

       
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.W));

        dodgeTutorialCanvas.SetActive(false);
        Time.timeScale = 1; 
        attackTutorialCompleted = true;

        attack1RepeatCount = 2; 
        StartCoroutine(PerformAttack1());
    }

    private void UpdateAttackTimers()
    {
        if (attackTutorialCompleted)
        {
            attack1Cooldown -= Time.deltaTime;
            attack2Cooldown -= Time.deltaTime;

            if (attack1RepeatCount > 0 && attack1Cooldown <= 0)
            {
                attack1RepeatCount--;
                StartCoroutine(PerformAttack1());
                ResetAttackCooldowns();
            }
            else if (attack1Cooldown<=0 && attack2Cooldown > 0)
            {
                StartCoroutine(PerformAttack1());
                ResetAttackCooldowns();
            }
            else if (attack2Cooldown <= 0 && attack1RepeatCount == 0)
            {
                StartCoroutine(PerformAttack2());
                ResetAttackCooldowns();
            }
        }
    }
    
    private IEnumerator PerformAttack1()
    {
        isAttacking = true;
        anim.SetTrigger("Attack1");

        
        yield return new WaitForSeconds(0.3f); 
        yield return new WaitForSeconds(0.3f); 

        
        InstantiateAttackGas();

        
        yield return new WaitForSeconds(0.4f); 
        yield return new WaitForSeconds(0.5f); 
        isAttacking = false;
    }

    private IEnumerator PerformAttack2()
    {
        isAttacking = true;
        anim.SetTrigger("Attack2");
        yield return new WaitForSeconds(1f); 
        yield return new WaitForSeconds(0.5f); 
        isAttacking = false;
    }

    private void ResetAttackCooldowns()
    {
        attack1Cooldown = Random.Range(attackCooldownRange.x, attackCooldownRange.y);
        attack2Cooldown = Random.Range(attackCooldownRange.x, attackCooldownRange.y);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, detectionRange);

        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(attackCheck.position, attackCheckRadius);
    }
    private void InstantiateAttackGas()
    {
        if (gasObject != null && shootingControler != null)
        {
            Vector3 spawnPosition = shootingControler.position + new Vector3(1.8f * Mathf.Sign(transform.localScale.x), -0.2f, 0);

            GameObject gasInstance = Instantiate(gasObject, spawnPosition, Quaternion.identity);

            Vector3 direction = transform.localScale.x > 0 ? Vector3.right : Vector3.left;
            gasInstance.transform.localScale = new Vector3(Mathf.Sign(direction.x), 1, 1); // Asegura que el gas se gira correctamente
            Destroy(gasInstance,3f);
            
        }
    }
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
      
        if (collision.CompareTag("bala"))
        {
            
            Rayo_Ataque_1 bala = collision.gameObject.GetComponent<Rayo_Ataque_1>();

            if (bala != null)
            {
                
                bossHealth -= bala.damage;

                
                Debug.Log("Daño recibido: " + bala.damage);

                
                Destroy(collision.gameObject);
            }
            else
            {
                Debug.LogWarning("El componente Rayo_Ataque_1 no se encontró en la bala.");
            }
        }
    }
    public void DamageToPlayer(VidaPlayer player)
    {
        player.playerHealth -= attack2Damage;

        Debug.Log($"Daño infligido al jugador: {attack2Damage}. Vida restante: {player.playerHealth}");

        if (player.playerHealth <= 0)
        {
            GameManager.THIS.OnPlayerDeath();
        }
    }
    private void attackTrigger()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(attackCheck.position, attackCheckRadius);
        foreach (var hit in colliders)
        {
            // Buscar si el objeto tiene el componente VidaPlayer
            var player = hit.GetComponent<VidaPlayer>();
            if (player != null)
            {
                // Aplicar daño al jugador
                DamageToPlayer(player);
            }
        }
    }

}
