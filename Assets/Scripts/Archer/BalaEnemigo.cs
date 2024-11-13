using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BalaEnemigo : MonoBehaviour
{
    public int arrowSpeed;
    public int damage;
    private void Update()
    {
        transform.Translate(Time.deltaTime* arrowSpeed*Vector2.right); 
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.TryGetComponent(out VidaPersonaje playerHealth))
        {
            playerHealth.TakeDamage(damage);
            Destroy(gameObject);
        }
            
    }
}
