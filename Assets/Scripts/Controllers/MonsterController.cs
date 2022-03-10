using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MonsterController : BaseController
{
    Vector3 startPosition, endPosition;
    bool _frameCheck;
    GameObject _player;

    public override void Init()
    {
        _rb = Util.GetOrAddComponent<Rigidbody2D>(gameObject);
        _frameCheck = true;
        _maxSpeed = 0.1f;
        _player = GameObject.FindGameObjectWithTag("Player");
    }

    private void OnMouseDown()
    {
        startPosition = GetPosition(Input.mousePosition);
        _rb.velocity = Vector3.zero;
    }

    private void OnMouseDrag()
    {
        if (_frameCheck)
        {
            _frameCheck = false;
            startPosition = transform.position = GetPosition(Input.mousePosition);
        }
        else
        {
            _frameCheck = true;
            _rb.velocity = GetVelocity(Input.mousePosition) * _maxSpeed;
        }
    }

    private void OnMouseUp()
    {
        _rb.velocity = GetVelocity(Input.mousePosition) * _maxSpeed;
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
}