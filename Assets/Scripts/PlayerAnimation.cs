using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    public static PlayerAnimation THIS;

    public readonly string[] _staticDirection = { "Idle N", "Idle NW", "Idle W", "Idle SW", "Idle S", "Idle SE", "Idle E", "Idle NE" };
    public readonly string[] _runDirection = { "Run N", "Run NW", "Run W", "Run SW", "Run S", "Run SE", "Run E", "Run NE" };

    [HideInInspector] public  Animator _anim;
    int lastDirection;

    private void Awake()
    {
        _anim = GetComponent<Animator>();

        if (THIS == null)
        {
            THIS = this;
        }
    }

    public void SetDirection(Vector2 _direction)
    {
        string[] directionArray = null;
        if (_direction.magnitude < 0.01)
        {
            directionArray = _staticDirection;
        }
        else
        {
            directionArray = _runDirection;
            lastDirection = DirectionToIndex(_direction, 8);
        }
        _anim.Play(directionArray[lastDirection]);
        Debug.Log(directionArray[lastDirection]);
    }

    private int DirectionToIndex(Vector2 _direction, int _sliceCount)
    {
        Vector2 norDir = _direction.normalized;

        float step = 360 / _sliceCount;

        float halfstep = step / 2;

        float angle = Vector2.SignedAngle(Vector2.up, norDir);

        angle += halfstep;

        if (angle < 0)
        {
            angle += 360;
        }

        float stepCount = angle / step;

        return Mathf.FloorToInt(stepCount);
    }

    public static int[] AnimatorStringArrayToHashArray(string[] animationArray)
    {
        int[] hashArray = new int[animationArray.Length];
        for (int i = 0; i < animationArray.Length; i++)
        {
            hashArray[i] = Animator.StringToHash(animationArray[i]);
        }
        return hashArray;
    }


    //Funciones para llamar como animation events
    public void FinalizarGolpe()
    {
        AttackController.THIS.attacking = false;
        AttackController.THIS.canAttack = true;
    }   
}
