using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public static PlayerMovement THIS;

    public float _playerSpeed;
    private Rigidbody2D _rb;
    private float _moveH, _moveY;
    [Header("Player Settings")]
    public PlayerState _playerState;
    [SerializeField] private float _jumpForce;
    PlayerAnimation _playerAnimation;
    public enum PlayerState
    {
        OutOfCombat,
        InCombat
    }

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _playerAnimation = GetComponentInChildren<PlayerAnimation>();

        if (THIS == null)
        {
            THIS = this;
        }
    }

    private void Update()
    {
        switch (_playerState)
        {
            case PlayerState.OutOfCombat:
                OutOfCombat();
                break;
            case PlayerState.InCombat:
                InCombat();
                break;
            default:
                break;
        }
    }

    public void InCombat()
    {
        _playerAnimation.SetAnimator(_playerAnimation._topDownAnimator);

        _moveH = Input.GetAxis("Horizontal") * _playerSpeed;
        _moveY = Input.GetAxis("Vertical") * _playerSpeed;
        _rb.velocity = new Vector2(_moveH, _moveY);
        Vector2 _direction = new Vector2(_moveH, _moveY);
        _direction = Vector2.ClampMagnitude(_direction, 1);
        Vector2 _movement = _direction * _playerSpeed;
        _playerAnimation.SetDirection(_movement);


        //if(Input.GetMouseButtonDown(0))
        //{
        //    AttackController.THIS.Golpe();
        //}
    }

    public void OutOfCombat()
    {
        _playerAnimation.SetAnimator(_playerAnimation._isometricAnimator);

        _moveH = Input.GetAxis("Horizontal") * _playerSpeed;
        _moveY = Input.GetAxis("Vertical") * _playerSpeed;
        _rb.velocity = new Vector2(_moveH, _moveY);
        Vector2 _direction = new Vector2(_moveH, _moveY);
        _direction = Vector2.ClampMagnitude(_direction, 1);
        Vector2 _movement = _direction * _playerSpeed;
        _playerAnimation.SetDirection(_movement);
    }
}