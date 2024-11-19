using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AtaqueGas : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]   private int damage;
    private VidaPlayer playerPlayer;
    
    public float attackCheckRadius;
    public Transform checkLocation;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void DamageToPlayer()
    {
        Debug.Log("damage");
        playerPlayer.playerHealth -= damage;
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
                player.playerHealth -= damage;

                // Actualizar la barra de vida del jugador si es necesario


                Debug.Log($"Daño infligido al jugador: {damage}. Vida restante: {player.playerHealth}");
            }
        }
    }

    private void OnDrawGizmos()
    {
       

        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(checkLocation.position, attackCheckRadius);

    }
}
