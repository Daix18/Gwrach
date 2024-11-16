using UnityEngine;
using UnityEngine.UI;

public class AttackController : MonoBehaviour
{
    public static AttackController THIS;

    [Header("Components")]
    [SerializeField] private Transform controladorGolpe;
    //[SerializeField] private Image fillImage;

    [Header("Multiplied floats")]
    [SerializeField] public float _playerHealth;
    [SerializeField] public float _playerDamage;

    [Header("Attack Settings")]
    [SerializeField] private float radioGolpe;
    [SerializeField] public float tiempoEntreAtaques;
    [SerializeField] public float tiempoSiguienteAtaque;
    private Animator animator;
    public bool attacking;
    public bool canAttack;

    private float initialHealth = 100f;

    private void Start()
    {
        animator = GetComponentInChildren<Animator>();
        _playerHealth = initialHealth;
    }

    private void Awake()
    {
        if (THIS == null)
        {
            THIS = this;
        }
    }

    private void Update()
    {
        if (tiempoSiguienteAtaque > 0)
        {
            tiempoSiguienteAtaque -= Time.deltaTime;
        }
    }

    public void TakeDamage(float damage)
    {
        _playerHealth -= damage;
        //fillImage.fillAmount = health / 100f;

        if (_playerHealth <= 0)
        {
            _playerHealth = 0; // Asegurarse de que la vida no sea negativa
            GameManager.THIS.OnPlayerDeath();
            ResetHealth();
        }
    }

    public float GetCurrentHealth()
    {
        return _playerHealth;
    }

    public void Golpe()
    {
        // Comprobar si el golpe ya está activo, si no, activarlo
        if (canAttack)
        {   
            if (!attacking)
            {
                attacking = true;
                canAttack = false;
                animator.SetTrigger("Golpe");
            }
        }

        Collider2D[] objetos = Physics2D.OverlapCircleAll(controladorGolpe.position, radioGolpe);

        foreach (Collider2D colisionador in objetos)
        {
            if (colisionador.CompareTag("Enemy"))
            {
                colisionador.transform.GetComponent<EnemyController>().TakeDamage(_playerDamage);
            }
        }
    }

    // Método llamado desde un evento del Animator al finalizar el golpe

    public void ResetHealth()
    {
        _playerHealth = initialHealth;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(controladorGolpe.position, radioGolpe);
    }
}
