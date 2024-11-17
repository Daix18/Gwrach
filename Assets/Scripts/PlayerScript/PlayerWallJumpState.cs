using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWallJumpState : Player_ST_State
{
    public PlayerWallJumpState(Player_ST _player, Player_ST_StateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        stateTimer = .4f; player.SetVelocity(5 * -player.facingDir, player.jumpForce);
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();
        if (stateTimer<0)
        {
            stateMachine.ChangeState(player.airState);
        }
    }
}
