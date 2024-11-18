using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AtaqueGas : MonoBehaviour
{
    // Start is called before the first frame update
    private int damage;
    
    public float attackCheckRadius;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void attackTrigger()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, attackCheckRadius);
        foreach (var hit in colliders)
        {
            // Comprobar si hit tiene un BossController
            var boss = hit.GetComponent<BossController>();
            if (boss != null)
            {
                boss.DamageToPlayer();
            }
        }
    }
    private void OnDrawGizmos()
    {
       

        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(transform.position, attackCheckRadius);

    }
}
