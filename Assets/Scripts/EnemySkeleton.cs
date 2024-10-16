using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySkeleton : Entity
{
    [Header("Enemy Movement")]
    [SerializeField] private float movementSpeed;
   
    protected override void Start()
    {
        base.Start();

    }
    protected override void Update()
    {
        base.Update();
        if (!isGrounded)
            Flip();

            rb.velocity = new Vector2(movementSpeed * facingDirection, rb.velocity.y);
    }
}
