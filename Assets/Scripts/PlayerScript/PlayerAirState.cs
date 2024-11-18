using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerAirState : Player_ST_State
{
    public PlayerAirState(Player_ST _player, Player_ST_StateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
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
        if (player.isWallDetected())
            stateMachine.ChangeState(player.wallSlideState);
        if (player.isGroundDetected())
        {
            stateMachine.ChangeState(player.idleState);
        }
        if (xInput != 0)
        {
            player.SetVelocity(player.playerSpeed*.8f*xInput, rb.velocity.y);
        }
    }
    





}
