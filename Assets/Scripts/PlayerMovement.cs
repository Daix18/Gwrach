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
    [SerializeField] private bool _isGrounded;
    [SerializeField] private PlayerState _playerState;
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
        _moveH = Input.GetAxis("Horizontal") * _playerSpeed;
        _rb.velocity = new Vector2(_moveH, _rb.velocity.y);

        if (Input.GetKeyDown(KeyCode.W) && _isGrounded)
        {
            _rb.AddForce(Vector2.up * _jumpForce, ForceMode2D.Impulse);
            Debug.Log("Saltooo");
        }

        if(Input.GetMouseButtonDown(0))
        {
            AttackController.THIS.Golpe();
        }

        if (_moveH < 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }

        else if (_moveH > 0)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
    }

    public void OutOfCombat()
    {

        _moveH = Input.GetAxis("Horizontal") * _playerSpeed;
        _moveY = Input.GetAxis("Vertical") * _playerSpeed;
        _rb.velocity = new Vector2(_moveH, _moveY);
        Vector2 _direction = new Vector2(_moveH, _moveY);
        _direction = Vector2.ClampMagnitude(_direction, 1);
        Vector2 _movement = _direction * _playerSpeed;
        _playerAnimation.SetDirection(_movement);

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Suelo"))
        {
            _isGrounded = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Suelo"))
        {
            _isGrounded = false;
        }
    }
}