using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D _rb;
    private float _moveH, _moveY;
    [SerializeField] private float _moveSpeed;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        _moveH = Input.GetAxis("Horizontal") * _moveSpeed;
        _moveY = Input.GetAxis("Vertical") * _moveSpeed;
        _rb.velocity = new Vector2(_moveH, _moveY);

        Vector2 direction = new Vector2(_moveH,_moveY);
    }
}
