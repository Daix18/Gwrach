using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_ST_StateMachine 
{
    public Player_ST_State currentState {  get; private set; }// Es publico si quieres obtenerlo, pero es privado si quieres cambiarlo, basicamente puedes verlo pero no afectarlo. (Como una opcion de Read-Only).
    
    public void Initialize(Player_ST_State _startState)
    {
        currentState = _startState;
        currentState.Enter();
    }
    public void ChangeState(Player_ST_State _newState)
    {
        currentState.Exit();
        currentState= _newState;
        currentState.Enter();
    }
}
