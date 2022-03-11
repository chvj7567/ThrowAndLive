using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    Vector3 _direction;
    float _bulletSpeed = 5f;

    void Update()
    {
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