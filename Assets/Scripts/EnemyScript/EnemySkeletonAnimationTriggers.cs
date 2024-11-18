using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySkeletonAnimationTriggers : MonoBehaviour
{
    private Enemy_ST_Eskeleton enemy=> GetComponentInParent<Enemy_ST_Eskeleton>();
    private void AnimationTrigger()
    {
        enemy.AnimationFinishTrigger();
    }
}
