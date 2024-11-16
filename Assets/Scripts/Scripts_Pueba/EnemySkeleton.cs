using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySkeleton : Entity
{
    bool isAttacking;
    [Header("Move Enemy")]
    [SerializeField] private float moveSpeed;
    [Header("Player Detection")]
    [SerializeField] private float playerCheckDistance;
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
            if (isPlayerDetected.distance > 1)
            {
                rb.velocity = new Vector2(moveSpeed * playerDirection, rb.velocity.y);
                Debug.Log("Veo al player");
                isAttacking = false;
            }
            else
            {
                Debug.Log("Atacando" + isPlayerDetected.collider.gameObject.name);
                isAttacking = true;
            }
        }
        if (!isGrounded || isWallDetected)
        {
            Flip();
        }
        Movement();
    }

    private void Movement()
    {
        if(!isAttacking)
        rb.velocity = new Vector2(moveSpeed * playerDirection, rb.velocity.y);
    }

    protected override void collisionChecks()
    {
        base.collisionChecks();
        isPlayerDetected = Physics2D.Raycast(transform.position, Vector2.right, playerCheckDistance * playerDirection, whatIsPlayer);
    }
    protected override void OnDrawGizmos()
    {
        base.OnDrawGizmos();
        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(transform.position, new Vector3(transform.position.x + playerCheckDistance * playerDirection, transform.position.y));
    }
}
