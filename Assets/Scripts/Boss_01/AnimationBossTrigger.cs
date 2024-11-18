using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationBossTrigger : MonoBehaviour
{
    private BossController bossController;
    // Start is called before the first frame update
    private void attackTrigger()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(bossController.attackCheck.position, bossController.attackCheckRadius);
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
}
