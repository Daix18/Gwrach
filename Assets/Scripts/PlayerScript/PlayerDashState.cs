using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDashState : Player_ST_State
{
    public PlayerDashState(Player_ST _player, Player_ST_StateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        stateTimer = player.dashDuration;
    }

    public override void Exit()
    {
        base.Exit();
        player.SetVelocity(0, rb.velocity.y);
    }

    public override void Update()
    {
        base.Update();
        if (!player.isGroundDetected() && player.isWallDetected())
        {
            stateMachine.ChangeState(player.wallSlideState);
        }
        player.SetVelocity(player.dashSpeed * player.dashDir, 0);
        if (stateTimer<0)
        {
            stateMachine.ChangeState(player.idleState);
        }
    }
}
