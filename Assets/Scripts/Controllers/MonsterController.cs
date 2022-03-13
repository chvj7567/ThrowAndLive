 using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MonsterController : BaseController
{
    Vector3 startPosition, endPosition;
    bool _frameCheck, _dragCheck;
    GameObject _player;
    float horizontal;
    float _throwSpeed;

    public override void Init()
    {
        _rb = Util.GetOrAddComponent<Rigidbody2D>(gameObject);
        _rb.freezeRotation = true;
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _frameCheck = false;
        _dragCheck = false;
        _maxSpeed = 1f;
        _throwSpeed = 0.1f;
        _player = GameObject.FindGameObjectWithTag("Player");
        GameObjectType = Define.GameObjects.Monster;
        _state = Define.State.Idle;
    }

    private void OnEnable()
    {
        Init();
    }

    protected override void UpdateIdle()
    {
        State = Define.State.Run;
    }

    protected override void UpdateDie()
    {
        MainManager.Game.Despawn(gameObject);
    }

    protected override void Run()
    {
        if (transform.position.x < -25f || transform.position.x > 25f)
        {
            State = Define.State.Die;
            return;
        }
        if (transform.position.y < -5f || transform.position.y > 30f)
        {
            State = Define.State.Die;
            return;
        }

        if (_dragCheck)
            return;

        if (State != Define.State.Run)
            return;
        
        if (_player == null)
        {
            if (Vector3.Dot(transform.right, Vector3.zero - transform.position) > 0)
            {
                horizontal = 1;
            }
            else
            {
                horizontal = -1;
            }
        }
        else
        {
            if (Vector3.Dot(transform.right, _player.transform.position - transform.position) > 0)
            {
                horizontal = 1;
            }
            else
            {
                horizontal = -1;
            }
        }

        _rb.AddForce(Vector2.right * horizontal, ForceMode2D.Impulse);

        if (_rb.velocity.x > 0)
        {
            _spriteRenderer.flipX = true;
        }
        else if (_rb.velocity.x < 0)
        {
            _spriteRenderer.flipX = false;
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

    private void OnMouseDown()
    {
        _dragCheck = true;
        startPosition = GetPosition(Input.mousePosition);
        _rb.velocity = Vector3.zero;
    }

    private void OnMouseDrag()
    {
        if (!_frameCheck)
        {
            _frameCheck = true;
            startPosition = transform.position = GetPosition(Input.mousePosition);
        }
        else
        {
            _frameCheck = false;
            _rb.velocity = GetVelocity(Input.mousePosition) * _throwSpeed;
        }
    }

    private void OnMouseUp()
    {
        _rb.velocity = GetVelocity(Input.mousePosition) * _throwSpeed;
    }

    Vector3 GetPosition(Vector3 mousePosition)
    {
        Vector3 pos = Camera.main.ScreenToWorldPoint(mousePosition);

        return new Vector3(pos.x, pos.y, 0);
    }

    Vector3 GetVelocity(Vector3 mousePosition)
    {
        endPosition = transform.position = GetPosition(mousePosition);

        Vector3 dir = (endPosition - startPosition).normalized;
        float dis = (endPosition - startPosition).magnitude;
        float speed = dis / Time.deltaTime;

        return dir * speed;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        _dragCheck = false;

        if (collision.gameObject.name == "Bullet")
        {
            MainManager.Game.Despawn(gameObject);
        }
    }
}