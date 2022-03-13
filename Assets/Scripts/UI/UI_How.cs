using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_How : UI_Base
{
    Image _back;

    enum Images
    {
        Back,
    }

    public override void Init()
    {
        Bind<Image>(typeof(Images));
        _back = GetImage((int)Images.Back).gameObject.GetComponent<Image>();
        BindEvent(_back.gameObject, BackGame, Define.UIEvent.Click);
    }

    void BackGame(PointerEventData eventData)
    {
        MainManager.UI.HideUI(gameObject, Define.UI.How);
        MainManager.UI.ShowUI("StartUI", Define.UI.Start);
    }
}