using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : BaseController
{
    Vector3 _direction;
    float _bulletSpeed;

    public override void Init()
    {
        _bulletSpeed = 5f;
    }

    void Update()
    {
        if (transform.position.x < -25f || transform.position.x > 25f)
        {
            MainManager.Game.Despawn(gameObject);
        }

        transform.Translate(_direction * Time.deltaTime * _bulletSpeed);
    }

    public void Shoot(Vector3 direction)
    {
        _direction = direction;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
            return;
        MainManager.Game.Despawn(gameObject);
    }
}