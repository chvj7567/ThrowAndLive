using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameScene : BaseScene
{
    protected override void Init()
    {
        base.Init();
        GameObject background = MainManager.Game.Spawn(Define.GameObjects.Background, "Background");
        MainManager.Game.Spawn(Define.GameObjects.Map, "Map");
        
        GameObject player = MainManager.Game.Spawn(Define.GameObjects.Player, "Player");
        CameraController camera = Util.GetOrAddComponent<CameraController>(Camera.main.gameObject);
        camera.SetPlayer(player);
        camera.SetCamera();
        camera.SetBackground(background);
    }

    public override void Clear()
    {

    }
}