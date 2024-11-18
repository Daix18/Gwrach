using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_ST_State 
{
    protected Player_ST_StateMachine stateMachine;
    protected Player_ST player;

    protected Rigidbody2D rb;

    protected float xInput;
    protected float yInput;
    private string animBoolName;

    protected float stateTimer;
    protected bool triggerClalled;
    public Player_ST_State (Player_ST _player, Player_ST_StateMachine _stateMachine, string _animBoolName)
    {
        this.player = _player;
        this.stateMachine = _stateMachine;
        this.animBoolName = _animBoolName;
    }
    public virtual void Enter()
    {
        player.anim.SetBool(animBoolName, true);
        rb= player.rb;
        triggerClalled= false;
    }
    public virtual void Update()
    {
        stateTimer-= Time.deltaTime;
        xInput = Input.GetAxisRaw("Horizontal");
        yInput = Input.GetAxisRaw("Vertical");
        player.anim.SetFloat("yVelocity", rb.velocity.y);
    }
    public virtual void Exit()
    {
        player.anim.SetBool(animBoolName, false);
    }
    public virtual void AnimationFinishTrigger()
    {
        triggerClalled=true;
    }
}
