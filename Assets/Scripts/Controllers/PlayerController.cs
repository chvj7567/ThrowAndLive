using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : BaseController
{
    public float _jumpPower;
    bool _isJumping;
    int _jumpCount;
    int _JumpMaxCount;
    bool _check;
    int _bulletDelay;

    public override void Init()
    {
        _check = false;
        _maxSpeed = 5f;
        _jumpPower = 5f;
        _jumpCount = 1;
        _JumpMaxCount = 1;
        _bulletDelay = 1;
        _rb = Util.GetOrAddComponent<Rigidbody2D>(gameObject);
        _rb.freezeRotation = true;
        _spriteRenderer = Util.GetOrAddComponent<SpriteRenderer>(gameObject);
        State = Define.State.Idle;
        GameObjectType = Define.GameObjects.Player;
    }

    protected override void UpdateJump()
    {
        if (Input.GetButtonDown("Jump") && _jumpCount < _JumpMaxCount)
        {
            _rb.AddForce(Vector2.up * _jumpPower, ForceMode2D.Impulse);
            _jumpCount++;
        }

        if (!_isJumping)
        {
            _rb.AddForce(Vector2.up * _jumpPower, ForceMode2D.Impulse);
            _isJumping = true;
        }

        if (Input.GetButton("Horizontal"))
        {
            State = Define.State.Run;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.gameObject.layer == LayerMask.NameToLayer("Road"))
        {
            _isJumping = false;
            _jumpCount = 1;
            if (Input.GetButton("Horizontal"))
            {
                //Debug.Log("Jump -> Run");
                State = Define.State.Run;
            }
            else
            {
                //Debug.Log("Jump -> Idle");
                State = Define.State.Idle;
            }
        }
    }

    protected override void UpdateRun()
    {
        if (Input.GetButtonDown("Jump"))
        {
            //Debug.Log("Run -> Jump");
            State = Define.State.Jump;
        }
        else if (Input.GetButtonUp("Horizontal"))
        {
            //Debug.Log("Run -> Idle");
            State = Define.State.Idle;
        }
    }

    protected override void UpdateIdle()
    {
        if (Input.GetButtonDown("Jump"))
        {
            //Debug.Log("Idle -> Jump");
            State = Define.State.Jump;
        }
        else if (Input.GetButton("Horizontal"))
        {
            //Debug.Log("Idle -> Run");
            State = Define.State.Run;
        }
    }

    protected override void Run()
    {
        if (State != Define.State.Run)
            return;

        float horizontal = Input.GetAxisRaw("Horizontal");

        _rb.AddForce(Vector2.right * horizontal, ForceMode2D.Impulse);

        StartCoroutine(CreateBullet(_check));

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

    IEnumerator CreateBullet(bool check)
    {
        if (!check)
        {
            _check = true;
            GameObject bullet = MainManager.Resource.Instantiate("Bullet");
            BulletController bulletController = Util.GetOrAddComponent<BulletController>(bullet);

            if (_rb.velocity.x > 0)
            {
                _spriteRenderer.flipX = false;
                bullet.transform.position = transform.position + Vector3.right;
                bulletController.Shoot(Vector3.right);
            }
            else if (_rb.velocity.x < 0)
            {
                _spriteRenderer.flipX = true;
                bullet.transform.position = transform.position - Vector3.right;
                bulletController.Shoot(-Vector3.right);
            }

            yield return new WaitForSeconds(_bulletDelay);

            _check = false;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.name == "Monster")
            MainManager.Game.Despawn(gameObject);
    }
}
