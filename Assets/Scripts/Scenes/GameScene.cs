using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameScene : BaseScene
{
    GameObject _player;
    Vector3 _fristPos;
    GameObject _map;
    protected override void Init()
    {
        base.Init();
        GameObject background = MainManager.Game.Spawn(Define.GameObjects.Background, "Background");
        _map = MainManager.Game.Spawn(Define.GameObjects.Map, "Map");
        MainManager.Game.Spawn(Define.GameObjects.MiniMap, "MiniMap");
        MainManager.Game.Spawn(Define.GameObjects.MiniMap, "MiniMap Camera");
        _player = MainManager.Game.Spawn(Define.GameObjects.Player, "Player");
        _fristPos = _player.transform.position;

        CameraController camera = Util.GetOrAddComponent<CameraController>(Camera.main.gameObject);
        camera.SetPlayer(_player);
        camera.SetCamera();

        GameObject spawning = new GameObject("@SpawningMonster");
        Util.GetOrAddComponent<SpawnMonster>(spawning);
    }

    void Update()
    {
        if (_player.transform.position.x > 76f)
        {
            _player.transform.position = _fristPos;
            MainManager.Game.Despawn(_map);
            _map = MainManager.Game.Spawn(Define.GameObjects.Map, "Map");
        }
    }
    public override void Clear()
    {

    }
}