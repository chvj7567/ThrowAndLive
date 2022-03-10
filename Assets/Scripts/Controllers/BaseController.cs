using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseController : MonoBehaviour
{
    protected float _maxSpeed;
    protected Rigidbody2D _rb;
    protected Define.State _state;
    protected SpriteRenderer _spriteRenderer;

    public Define.GameObjects GameObjectType { get; protected set; } = Define.GameObjects.Unknown;

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

    void Start()
    {
        Init();
    }

    void Update()
    {
        switch (State)
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

    void FixedUpdate()
    {
        Run();
        Jump();
    }

    public abstract void Init();
    protected virtual void UpdateIdle() { }
    protected virtual void UpdateRun() { }
    protected virtual void Run() { }
    protected virtual void UpdateJump() { }
    protected virtual void Jump() { }
    protected virtual void UpdateDie() { }
}
