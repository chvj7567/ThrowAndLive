using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : BaseController
{
    float _jumpPower;
    bool _isJumping;
    
    public override void Init()
    {
        _maxSpeed = 5f;
        _jumpPower = 5f;
        _rb = Util.GetOrAddComponent<Rigidbody2D>(gameObject);
        _rb.freezeRotation = true;
        _spriteRenderer = Util.GetOrAddComponent<SpriteRenderer>(gameObject);
        State = Define.State.Idle;
        GameObjectType = Define.GameObjects.Player;
    }

    protected override void UpdateRun()
    {
        if (Input.GetButtonDown("Jump"))
        {
            Debug.Log("Run -> Jump");
            State = Define.State.Jump;
        }
        else if (Input.GetButtonUp("Horizontal"))
        {
            Debug.Log("Run -> Idle");
            State = Define.State.Idle;
        }
    }

    protected override void UpdateIdle()
    {
        if (Input.GetButtonDown("Jump"))
        {
            Debug.Log("Idle -> Jump");
            State = Define.State.Jump;
        }
        else if (Input.GetButton("Horizontal"))
        {
            Debug.Log("Idle -> Run");
            State = Define.State.Run;
        }
    }

    protected override void Run()
    {
        if (State != Define.State.Run)
            return;

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

    protected override void Jump()
    {
        if (State != Define.State.Jump)
            return;

        if (!_isJumping)
        {
            _rb.AddForce(Vector2.up * _jumpPower, ForceMode2D.Impulse);
            _isJumping = true;
        }

        RaycastHit2D hit = Physics2D.Raycast(_rb.position, Vector2.down, 1, ~LayerMask.GetMask("Player"));

        if (hit.collider != null && hit.distance < 0.5f)
        {
            Debug.Log("Run -> Jump");
            State = Define.State.Idle;
            _isJumping = false;
        }
    }
}
