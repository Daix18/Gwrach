using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AtaqueGas : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]   private int damage;
    private VidaPlayer playerPlayer;
    
    public float attackCheckRadius;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void DamageToPlayer(VidaPlayer player)
    {
        player.playerHealth -= damage;

        Debug.Log($"Daño infligido al jugador: {damage}. Vida restante: {player.playerHealth}");

        if (player.playerHealth <= 0)
        {
            player.playerHealth = 0; // Asegurarse de que la vida no sea negativa
            GameManager.THIS.OnPlayerDeath();
        }
    }
    private void attackTrigger()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, attackCheckRadius);
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

    private void OnDrawGizmos()
    {
       

        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(transform.position, attackCheckRadius);

    }
}
