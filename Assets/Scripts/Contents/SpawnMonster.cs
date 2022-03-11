using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnMonster : MonoBehaviour
{
    float _spawnTime;
    List<SpawnPosition> _pos;
    class SpawnPosition
    {
        public float X { get; private set; }
        public float Y { get; private set; }
        public SpawnPosition(float x, float y)
        {
            X = x; Y = y;
        }
    }

    void Awake()
    {
        _spawnTime = 2f;
        _pos = new List<SpawnPosition>();
        _pos.Add(new SpawnPosition(21f, -2.4f));
        _pos.Add(new SpawnPosition(-21f, -2.4f));
        _pos.Add(new SpawnPosition(21f, 10));
        _pos.Add(new SpawnPosition(-21f, 10f));
        Debug.Log(_pos.Count);
    }

    void Start()
    {
        StartCoroutine(CreateMonster());
    }

    IEnumerator CreateMonster()
    {
        while (true)
        {
            foreach (SpawnPosition pos in _pos)
            {
                GameObject monster = MainManager.Resource.Instantiate("Monster");
                monster.transform.position = new Vector3(pos.X, pos.Y, 0f);
            }

            yield return new WaitForSeconds(_spawnTime);
        }
    }
}
