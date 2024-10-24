using UnityEngine;
using UnityEngine.UI;

public class AttackController : MonoBehaviour
{
    public static AttackController THIS;

    [Header("Components")]
    [SerializeField] private Transform controladorGolpe;
    //[SerializeField] private Image fillImage;

    [Header("Attack Settings")]
    [SerializeField] public float health;
    [SerializeField] private float radioGolpe;
    [SerializeField] public float danoGolpe;
    [SerializeField] public float tiempoEntreAtaques;
    [SerializeField] public float tiempoSiguienteAtaque;
    private Animator animator;
    public bool attacking;
    public bool canAttack;

    private float initialHealth = 100f;

    private void Start()
    {
        animator = GetComponent<Animator>();
        health = initialHealth;
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
        health -= damage;
        //fillImage.fillAmount = health / 100f;

        if (health <= 0)
        {
            health = 0; // Asegurarse de que la vida no sea negativa
        }
    }

    public float GetCurrentHealth()
    {
        return health;
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
                PlayerAnimation.THIS._anim.SetTrigger("Attack");
            }
        }

        Collider2D[] objetos = Physics2D.OverlapCircleAll(controladorGolpe.position, radioGolpe);

        foreach (Collider2D colisionador in objetos)
        {
            if (colisionador.CompareTag("Enemigo"))
            {
                colisionador.transform.GetComponent<EnemyController>().TakeDamage(danoGolpe);
            }
        }
    }

    public void ResetHealth()
    {
        health = initialHealth;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(controladorGolpe.position, radioGolpe);
    }
}
