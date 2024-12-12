using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    public readonly string[] _isometricstaticDirection = { "Idle N", "Idle NW", "Idle W", "Idle SW", "Idle S", "Idle SE", "Idle E", "Idle NE" };
    public readonly string[] _isometricrunDirection = { "Run N", "Run NW", "Run W", "Run SW", "Run S", "Run SE", "Run E", "Run NE" };
    public readonly string[] _topdownrunDirection = { "Run_N", "Run_NW", "Run_W", "Run_SW", "Run_S", "Run_SE", "Run_E", "Run_NE" };

    public Animator _isometricAnimator;
    public Animator _topDownAnimator;

    public static bool _inCombat;

    [HideInInspector]public Animator _anim;
    int lastDirection;

    private void Awake()
    {
        _anim = GetComponent<Animator>();
        _isometricAnimator.enabled = false;
        _topDownAnimator.enabled = false;

        SetAnimator(_isometricAnimator);
    }

    private void Update()
    {
        if (_anim.runtimeAnimatorController == _topDownAnimator.runtimeAnimatorController)
        {
            _inCombat = true;
        }
        else
        {
            _inCombat = false;
        }
    }

    public void SetAnimator(Animator animator)
    {
        animator.enabled = true;

        _anim.runtimeAnimatorController = animator.runtimeAnimatorController;
    }

    public void SetDirection(Vector2 _direction)
    {
        string[] directionArray = null;
        if (_direction.magnitude < 0.01)
        {
            directionArray = _isometricstaticDirection;
        }
        else
        {
            directionArray = _inCombat ? _topdownrunDirection : _isometricrunDirection; ;
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
