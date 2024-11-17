using UnityEngine;

public class Player : Entity
{

    [Header("Player Movement")]
    [SerializeField] private int movementSpeed;
    [SerializeField] private int jumpForce;
    [Space]
    [Header("Dash Info")]
    [SerializeField] private float dashSpeed;
    [SerializeField] private float dashDuration;
    [SerializeField] private float dashCooldown;
    private float dashTime;
    [Space]
    [Header("Attack info")]
    private bool isAttacking;
    [SerializeField] private int comboCounter;
    [SerializeField] private float comboTimeWindow;
    [SerializeField] private float comboTime = .3f;
    private float dashCooldownTimer;
    [Header("Attack Collider")]
    public  Transform AttackControler;
    [SerializeField] private float hitRadius;
    [SerializeField] private int hitDamage=2;
    [Space]
    private float xInput;

    protected override void Start()
    {
        base.Start();
    }


    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
        Movement();

        CheckInput();
        collisionChecks();

        dashTime -= Time.deltaTime;
        dashCooldownTimer -= Time.deltaTime;
        comboTimeWindow -= Time.deltaTime;


        FlipController();
        AnimatorControllers();
    }

    public void AttackOver()
    {
        isAttacking = false;
        comboCounter++;
        if (comboCounter > 2) { comboCounter = 0; }

    }
    private void CheckInput()
    {
        xInput = Input.GetAxisRaw("Horizontal");
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {

            StartAtackEvent();
            Golpe();
        }
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            Jump();
            isGrounded = false;
        }
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            DashAbility();
        }
    }

    private void StartAtackEvent()
    {
        if (!isGrounded)
        {
            return;
        }
        if (comboTimeWindow < 0) { comboCounter = 0; }
        isAttacking = true;
        comboTimeWindow = comboTime;
    }

    private void DashAbility()
    {
        if (dashCooldownTimer < 0 && !isAttacking)
        {
            dashCooldownTimer = dashCooldown;
            dashTime = dashDuration;
        }

    }
    private void Movement()
    {
        if (isAttacking)
        {
            rb.velocity = new Vector2(0, 0);
        }
        else if (dashTime > 0)
        {
            rb.velocity = new Vector2(playerDirection * dashSpeed, 0);
        }

        else { rb.velocity = new Vector2(xInput * movementSpeed, rb.velocity.y); }

    }
    private void Jump()
    {
        rb.velocity = new Vector2(rb.velocity.x, jumpForce);
    }
    private void AnimatorControllers()
    {
        bool isMoving = rb.velocity.x != 0;
        anim.SetFloat("yVelocity", rb.velocity.y);
        anim.SetBool("isMoving", isMoving);
        anim.SetBool("isGrounded", isGrounded);
        anim.SetBool("isDashing ", dashTime > 0);
        anim.SetBool("isAttacking", isAttacking);
        anim.SetInteger("comboCounter", comboCounter);
    }

    private void FlipController()
    {
        if (rb.velocity.x > 0 && !facingRight)
            Flip();
        else if (rb.velocity.x < 0 && facingRight) { Flip(); }
    }
    private void Golpe()
    {
        anim.SetTrigger("Hit");
        Collider2D[] objects = Physics2D.OverlapCircleAll(AttackControler.position, hitRadius);
        foreach(Collider2D collider in objects)
        {
            if (collider.CompareTag("Enemigo"))
            {
                collider.transform.GetComponentInChildren<Archer_Enemy>().TakeDamage(hitDamage);
            }
        }
    }
    protected override void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(AttackControler.position, hitRadius);
    }







}
