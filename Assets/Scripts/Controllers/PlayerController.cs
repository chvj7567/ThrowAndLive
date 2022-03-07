using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    float _maxSpeed;
    [SerializeField]
    float _jumpPower;

    Rigidbody2D _rb;
    Define.State _state;
    SpriteRenderer _spriteRenderer;

    public bool IsJumping { get; private set; }

    public Define.State State
    {
        get { return _state; }
        set
        {
            _state = value;

            Animator anim = GetComponent<Animator>();
            switch (_state)
            {
                case Define.State.Idle:
                    anim.Play("IDLE");
                    break;
                case Define.State.Run:
                    anim.Play("RUN");
                    break;
                case Define.State.Jump:
                    anim.Play("JUMP");
                    break;
                case Define.State.Die:
                    break;
            }
        }
    }
    private void Awake()
    {
        _maxSpeed = 5f;
        _jumpPower = 5f;
        _rb = GetComponent<Rigidbody2D>();
        _rb.freezeRotation = true;
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _state = Define.State.Idle;
        IsJumping = false;
    }

    private void Update()
    {
        switch (_state)
        {
            case Define.State.Idle:
                UpdateIdle();
                break;
            case Define.State.Run:
                UpdateRun();
                break;
            case Define.State.Jump:
                UpdateJump();
                break;
            case Define.State.Die:
                UpdateDie();
                break;
        }
    }

    private void UpdateDie()
    {

    }

    private void UpdateJump()
    {
        if (!IsJumping)
        {
            _rb.AddForce(Vector2.up * _jumpPower, ForceMode2D.Impulse);
            IsJumping = true;
        }
        else
        {
            Debug.DrawRay(_rb.position, Vector2.down, new Color(0, 1, 0));
            RaycastHit2D hit = Physics2D.Raycast(_rb.position, Vector2.down, 0.6f, LayerMask.GetMask("Road"));
            
            // hit.distance 조건 없애면 2단 점프 가능
            if (hit.collider != null && hit.distance < 0.5f)
            {
                IsJumping = false;
                State = Define.State.Idle;
            }
        }
    }

    private void UpdateRun()
    {
        if (!IsJumping && Input.GetButtonDown("Jump"))
        {
            State = Define.State.Jump;
        }
        else if (Input.GetButton("Horizontal") || Input.GetButtonDown("Horizontal"))
        {
            State = Define.State.Run;
        }
        else if (Input.GetButtonUp("Horizontal"))
        {
            State = Define.State.Idle;
        }
    }

    private void UpdateIdle()
    {
        if (!IsJumping && Input.GetButtonDown("Jump"))
        {
            State = Define.State.Jump;
        }
        else if (Input.GetButton("Horizontal") || Input.GetButtonDown("Horizontal"))
        {
            State = Define.State.Run;
        }
        else
        {
            State = Define.State.Idle;
        }
    }

    private void FixedUpdate()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        _rb.AddForce(Vector2.right * horizontal, ForceMode2D.Impulse);

        if (_rb.velocity.x > 0)
        {
            _spriteRenderer.flipX = false;
        }
        else if (_rb.velocity.x < 0)
        {
            _spriteRenderer.flipX = true;
        }

        if (_rb.velocity.x > _maxSpeed)
        {
            _rb.velocity = new Vector2(_maxSpeed, _rb.velocity.y);
        }
        else if (_rb.velocity.x < _maxSpeed * -1)
        {
            _rb.velocity = new Vector2(_maxSpeed * -1, _rb.velocity.y);
        }
    }
}
