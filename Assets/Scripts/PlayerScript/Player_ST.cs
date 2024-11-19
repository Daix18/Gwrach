using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_ST : Entity_ST
{
    [Header("Attack details")]
    public Vector2[] attackMovements;
    public bool isBusy {  get; private set; }
    [Header("Player Move")]
    [SerializeField] private float dashCooldown;
    private float dashUsageTimer;
    public float playerSpeed= 12;
    public float jumpForce;
    public float damage = 15;

    [Header("Dash")]
    public float dashSpeed;
    public float dashDuration;
    public float dashDir {  get; private set; }

    #region States
    public Player_ST_StateMachine stateMachine {  get; private set; }
    public PlayerIdleState idleState { get; private set; }
    public PlayerMoveState moveState { get; private set; }
    public PlayerJumpState jumpState { get; private set; }
    public PlayerAirState airState { get; private set; }    
    public PlayerDashState dashState { get; private set; }  
    public PlayerWallSlideState wallSlideState { get; private set; }
    public PlayerWallJumpState wallJumpState { get; private set; }
    public PlayerPrimaryAttack primaryAttack { get; private set; }

    #endregion
    protected override void Awake()
    {
        base.Awake();
        stateMachine = new Player_ST_StateMachine();

        idleState      = new PlayerIdleState(this, stateMachine, "Idle");
        moveState      = new PlayerMoveState(this, stateMachine, "Move");
        jumpState      = new PlayerJumpState(this, stateMachine, "Jump");
        airState       = new PlayerAirState(this, stateMachine, "Jump");
        dashState      = new PlayerDashState(this, stateMachine, "Dash");
        wallSlideState = new PlayerWallSlideState(this, stateMachine, "WallSlide");
        wallJumpState  = new PlayerWallJumpState(this, stateMachine, "WallJump");
        primaryAttack = new PlayerPrimaryAttack(this, stateMachine, "Attack");
    }
    protected override void Start()
    {
        base.Start();
        stateMachine.Initialize(idleState);
    }
    protected override void Update()
    {
        base.Update();
        stateMachine.currentState.Update();
        CheckForDashInput();
        
    }
    public IEnumerator BusyFor(float _seconds)
    {
        isBusy = true;
        yield return new WaitForSeconds(_seconds);
        isBusy = false;
    }
    public void AnimationTrigger()=>stateMachine.currentState.AnimationFinishTrigger();
    
    private void CheckForDashInput()
    {
        if (isWallDetected())
        {
            return;
        }
        dashUsageTimer -= Time.deltaTime;
        if (Input.GetKeyDown(KeyCode.W)&& dashUsageTimer<0) 
        {
            dashUsageTimer = dashCooldown;
            dashDir = Input.GetAxisRaw("Horizontal");
            if (dashDir==0)
            {
                dashDir = facingDir;
            }
            stateMachine.ChangeState(dashState);
        }
            
    }
}
