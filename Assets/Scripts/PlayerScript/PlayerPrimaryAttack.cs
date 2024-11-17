using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPrimaryAttack : Player_ST_State
{
    private int comboCounter;
    private float lastTimeAttacked;
    private float comboWindow=2;
    public PlayerPrimaryAttack(Player_ST _player, Player_ST_StateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
            if(comboCounter>2||Time.time>= lastTimeAttacked+ comboWindow)
            comboCounter=0;

        player.anim.SetInteger("ComboCounter",comboCounter);
        player.SetVelocity(player.attackMovements[comboCounter].x * player.facingDir, player.attackMovements[comboCounter].y);
        stateTimer = .1f;
    }

    public override void Exit()
    {
        base.Exit();
        player.StartCoroutine("BusyFor", .15f);
        comboCounter ++;
        lastTimeAttacked = Time.time;

    }

    public override void Update()
    {
        base.Update();
        if (stateTimer < 0)
        {
            rb.velocity= new Vector2 (0,0);
        }
        if (triggerClalled)
        {
            stateMachine.ChangeState(player.idleState);

        }
    }
    
}
