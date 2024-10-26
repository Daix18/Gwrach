    using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private float life;

    public void TakeDamage(float damage)
    {
        life -= damage;
    }
}
