using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GameManager
{
    GameObject _background;
    GameObject _map;
    GameObject _player;
    HashSet<GameObject> _monsters = new HashSet<GameObject>();

    int _sortOrder = 0;

    public GameObject Spawn(Define.GameObjects type, string path, Transform parent = null)
    {
        GameObject go = MainManager.Resource.Instantiate(path, parent);

        if (go == null)
            return null;

        switch (type)
        {
            case Define.GameObjects.Background:
                _background = go;
                go.GetComponent<SpriteRenderer>().sortingOrder = _sortOrder++;
                break;
            case Define.GameObjects.Map:
                _map = go;
                go.GetComponentInChildren<TilemapRenderer>().sortingOrder = _sortOrder;
                break;
            case Define.GameObjects.Monster:
                _monsters.Add(go);
                go.GetComponent<SpriteRenderer>().sortingOrder = _sortOrder++;
                break;
            case Define.GameObjects.Player:
                _player = go;
                go.GetComponent<SpriteRenderer>().sortingOrder = _sortOrder++;
                break;
        }

        return go;
    }

    public Define.GameObjects GetWorldObjectType(GameObject go)
    {
        BaseController bc = go.GetComponent<BaseController>();
        if (bc == null)
            return Define.GameObjects.Unknown;

        return bc.GameObjectType;
    }
}
