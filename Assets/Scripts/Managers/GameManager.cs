using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GameManager
{
    public GameObject Background { get; private set; }
    public GameObject Map { get; private set; }
    public GameObject Player { get; private set; }
    public GameObject MiniMap { get; private set; }
    public GameObject MiniMapCamera { get; private set; }

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
                Background = go;
                go.GetComponent<SpriteRenderer>().sortingOrder = _sortOrder++;
                break;
            case Define.GameObjects.Map:
                Map = go;
                go.GetComponentInChildren<TilemapRenderer>().sortingOrder = _sortOrder;
                break;
            case Define.GameObjects.Monster:
                _monsters.Add(go);
                Util.FindChild(go, "Square").GetComponent<SpriteRenderer>().sortingOrder = go.GetComponent<SpriteRenderer>().sortingOrder = _sortOrder++;
                break;
            case Define.GameObjects.Player:
                Player = go;
                Util.FindChild(go, "Square").GetComponent<SpriteRenderer>().sortingOrder = go.GetComponent<SpriteRenderer>().sortingOrder = _sortOrder++;
                break;
            case Define.GameObjects.MiniMap:
                MiniMap = go;
                break;
            case Define.GameObjects.MiniMapCamera:
                MiniMapCamera = go;
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

    public void Despawn(GameObject go)
    {
        Define.GameObjects type = GetWorldObjectType(go);

        switch (type)
        {
            case Define.GameObjects.Background:
                Background = null;
                break;
            case Define.GameObjects.Player:
                Player = null;
                break;
            case Define.GameObjects.Map:
                Map = null;
                break;
            case Define.GameObjects.MiniMap:
                MiniMap = null;
                break;
            case Define.GameObjects.MiniMapCamera:
                MiniMapCamera = null;
                break;
        }

        MainManager.Resource.Destroy(go);
    }
}
