using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour
{
    protected Rigidbody2D rb;
    protected Animator anim;
    protected int playerDirection = 1;
    protected bool facingRight = true;

    [Header("Collision Info")]
    [SerializeField] protected Transform groundcheck;
    [SerializeField] protected float grounCheckDistance;
    [Space]
    [SerializeField] protected Transform wallCheck;
    [SerializeField] protected float wallCheckDistance;
    [SerializeField] protected LayerMask whatIsGround;

    protected bool isGrounded;
    protected bool isWallDetected;
    protected virtual void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim= GetComponentInChildren<Animator>();
        if (wallCheck == null)
        {
            wallCheck = transform;
        }
    }

  
    protected virtual void Update()
    {
        collisionChecks();
    }
    protected virtual void collisionChecks()
    {

        isGrounded = Physics2D.Raycast(groundcheck.position, Vector2.down, grounCheckDistance, whatIsGround);
        isWallDetected = Physics2D.Raycast(wallCheck.position, Vector2.right, wallCheckDistance * playerDirection, whatIsGround);
    }
    protected virtual void Flip()
    {
        playerDirection = playerDirection * -1;
        facingRight = !facingRight;
        transform.Rotate(0, 180, 0);
    }
    protected virtual void OnDrawGizmos()
    {
        Gizmos.DrawLine(groundcheck.position, new Vector3(groundcheck.position.x, groundcheck.position.y - grounCheckDistance));
        Gizmos.DrawLine(wallCheck.position, new Vector2(wallCheck.position.x + wallCheckDistance*playerDirection, wallCheck.position.y));
    }
}
