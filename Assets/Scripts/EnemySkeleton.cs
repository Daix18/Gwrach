using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySkeleton : Entity
{
    bool isAtaquing;
    [Header("Enemy Movement")]
    [SerializeField] private float movementSpeed;

    [Header("Player Detection")]
    [SerializeField] private float playerCheckDistance
        ;
    [SerializeField] private LayerMask whatIsPlayer;

    private RaycastHit2D isPlayerDetected;
   
    protected override void Start()
    {
        base.Start();

    }
    protected override void Update()
    {
        base.Update();
        if (isPlayerDetected)
        {
            if (isPlayerDetected.distance > 2)
            {
                rb.velocity = new Vector2(movementSpeed *6f * facingDirection, rb.velocity.y);
                Debug.Log("Veo al Jugador");
                
            }
            else
            {
                Debug.Log("Attack" + isPlayerDetected);
                
            }
        }
        if (!isGrounded || isWallDetected )
            Flip();

        movement();

        Debug.Log(isPlayerDetected.collider.name);
           
    }
    private void movement()
    {
        if(!isAtaquing)
            rb.velocity = new Vector2(movementSpeed * facingDirection, rb.velocity.y);
    }
    protected override void CollisionChecks()
    {
        base.CollisionChecks();
        isPlayerDetected = Physics2D.Raycast(transform.position, Vector2.right, playerCheckDistance * facingDirection, whatIsPlayer);
    }
    

    protected override void OnDrawGizmos()
    {
        base.OnDrawGizmos();

        Vector3 rayStart = new Vector3(transform.position.x, transform.position.y); // Y-offset in gizmo

        Debug.DrawRay(rayStart, Vector2.right * playerCheckDistance * facingDirection, Color.blue);
    }
}
