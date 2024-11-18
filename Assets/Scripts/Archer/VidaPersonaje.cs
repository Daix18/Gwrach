using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VidaPersonaje : MonoBehaviour
{
    public int playerHealth;
    public void TakeDamage(int damage)
    {
        playerHealth -= damage;
        if (playerHealth <= 0)
        {
            Destroy(gameObject);
        }
    }
}
