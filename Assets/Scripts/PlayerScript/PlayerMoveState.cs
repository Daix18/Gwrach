using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveState : PlayerGroundedState
{
    public PlayerMoveState(Player_ST _player, Player_ST_StateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
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
        
        player.SetVelocity(xInput*player.playerSpeed, rb.velocity.y);
        if (xInput == 0|| player.isWallDetected())
            stateMachine.ChangeState(player.idleState);
    }
}
