using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(CapsuleCollider2D))]

public class TanukiController : MonoBehaviour
{
    public enum FacedDirection { Right, Left, Up, Down }
    [SerializeField] private Rigidbody2D rb;

    [SerializeField] private int _moveSpeed;
    [SerializeField] private int _jumpForce;

    [SerializeField] private FireBall _fireBall;

    [SerializeField] private AudioClip _damagedSound;

    private Animator _animator;
    private FacedDirection _facedDirection;
    private bool _isGrounded = false;
    void Start()
    {   
        _facedDirection = FacedDirection.Right;
        _fireBall.Initialize();
        _animator = GetComponent<Animator>();
    }
    private void OnDestroy() {
        _fireBall.OnDestroy();
    }
    void Update()
    {
        if ((Input.GetKey(KeyCode.J)) && _isGrounded && !(rb.velocity.y < -0.5f))
        {
            Jump();
        }

        if ((Input.GetKeyDown(KeyCode.K)))
        {
            Fire();
        }

        /* 移動 */
        float inputValue = Input.GetAxis("Horizontal");
        if (inputValue != 0){
            _animator.SetBool("IsWalk", true);
            rb.velocity = new Vector2(inputValue * _moveSpeed, rb.velocity.y);
        }
        else
        {
            _animator.SetBool("IsWalk", false);
        }

        /* 向きの変更 */
        if (Input.GetKey("right") || Input.GetKey(KeyCode.D))
        {
            if (_fireBall.GetStatus() != FireBall.FireBallStatus.Firing){
                this.transform.eulerAngles = new Vector3(0, 0, 0);
                _facedDirection = FacedDirection.Right;
            }
        }
        else if (Input.GetKey("left") || Input.GetKey(KeyCode.A))
        {
            if (_fireBall.GetStatus() != FireBall.FireBallStatus.Firing){
                this.transform.eulerAngles = new Vector3(0, 180, 0);
                _facedDirection = FacedDirection.Left;
            }
        }
        else if (Input.GetKey("up") || Input.GetKey(KeyCode.W))
        {
            _facedDirection = FacedDirection.Up;
        }
        else if (Input.GetKey("down") || Input.GetKey(KeyCode.S))
        {
            _facedDirection = FacedDirection.Down;
        } else {
            if (this.transform.eulerAngles.y == 0) {
                _facedDirection = FacedDirection.Right;
            } else {
                _facedDirection = FacedDirection.Left;
            }
        }
    }

    public FacedDirection GetFacedDirection() 
    {
        return _facedDirection;
    }

    void Jump() 
    {
        _isGrounded = false;
        rb.AddForce(Vector2.up * _jumpForce, ForceMode2D.Impulse);
    }

    void Fire()
    {
        if (_fireBall.GetStatus() == FireBall.FireBallStatus.Idle)
        {
            _animator.SetTrigger("AttackTrigger");
            _animator.SetBool("IsWalk", false);
            _fireBall.Fire();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Stage"))
        {
            _isGrounded = true;
        }
        if (collision.gameObject.CompareTag("Enemy"))
        {
            BGMManager._bgmInstance.PlaySE(_damagedSound);
            _animator.SetTrigger("DamageTrigger");
            GameManager.Instance.DecreaseHP();
        }
    }
}
