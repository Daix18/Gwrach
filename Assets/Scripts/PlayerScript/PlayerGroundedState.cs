using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGroundedState : Player_ST_State
{
    public PlayerGroundedState(Player_ST _player, Player_ST_StateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            stateMachine.ChangeState(player.primaryAttack);
        }
        if (!player.isGroundDetected())
            stateMachine.ChangeState(player.airState);
        if (Input.GetKeyDown(KeyCode.Space)&&player.isGroundDetected())
        {
            stateMachine.ChangeState(player.jumpState);
        }
        if (player.isGroundDetected() && xInput == 0)
        {
            player.SetVelocity(player.playerSpeed * xInput, rb.velocity.y);
        }
    }

  
}
