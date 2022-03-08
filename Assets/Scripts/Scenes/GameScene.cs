using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameScene : BaseScene
{
    protected override void Init()
    {
        base.Init();
        MainManager.Game.Spawn(Define.GameObjects.Background, "Background");
        MainManager.Game.Spawn(Define.GameObjects.Map, "Map");
        MainManager.Game.Spawn(Define.GameObjects.Player, "Player");
    }

    public override void Clear()
    {

    }
}