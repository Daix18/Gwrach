using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIdleState : PlayerGroundedState
{
    public PlayerIdleState(Player_ST _player, Player_ST_StateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
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
        if (xInput == player.facingDir && player.isWallDetected())
            return;
        if (xInput != 0&& !player.isBusy)
            stateMachine.ChangeState(player.moveState);
    }
}
