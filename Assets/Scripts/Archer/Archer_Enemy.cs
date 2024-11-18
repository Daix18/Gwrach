using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Archer_Enemy : MonoBehaviour
{
    public Transform shootingControler;
    public float playerDistance;
    public LayerMask whereIsPlayer;
    public bool isPlayerLocated;
    public GameObject archerArrow;
    public float timeBtwShoots;
    public float lastTimeShot;
    public float waitTimeBtwShoots;
    [SerializeField] private float health = 3;
    
    public Animator anim;
    private void Start()
    {
        anim=GetComponent<Animator>();
    }
    private void Update()
    {
        isPlayerLocated = Physics2D.Raycast(shootingControler.position, transform.right, playerDistance, whereIsPlayer);
        if (isPlayerLocated)
        {
            if (Time.time > timeBtwShoots + lastTimeShot)
            {
                lastTimeShot = Time.time;
                anim.SetTrigger("Disparar");
                Invoke(nameof(ShootArrow), waitTimeBtwShoots);
            }

        }
    }
    private void ShootArrow()
    {
        Instantiate(archerArrow, shootingControler.position, shootingControler.rotation);
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(shootingControler.position, shootingControler.position + transform.right * playerDistance);
    }
    public void TakeDamage(float damage)
    {
        health-=damage;
        if (health < 0)
        {
            Die();
        }
    }
    private void Die()
    {
        anim.SetTrigger("Muerte");
    }

}
