using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    private Animator _anim;
    public string[] _staticDirection = { "Idle SE", "Idle SW", "Idle NE", "Idle NW" };

    int lastDirection;

    private void Awake()
    {
        _anim = GetComponent<Animator>();
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
            lastDirection = DirectionToIndex(_direction);
        }

        _anim.Play(directionArray[lastDirection]);
    }

    private int DirectionToIndex(Vector2 _direction)
    {
        Vector2 norDir = _direction.normalized;

        float step = 360 / 8;

        float angle = Vector2.SignedAngle(Vector2.up, norDir);


        return 1;
    }
}
