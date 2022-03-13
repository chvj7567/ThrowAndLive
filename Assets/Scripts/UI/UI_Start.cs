using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_Start : UI_Base
{
    Image _start, _how, _setting, _exit;
    enum Images
    {
        Start,
        How,
        Setting,
        Exit,
    }

    public override void Init()
    {
        Bind<Image>(typeof(Images));
        _start = GetImage((int)Images.Start).gameObject.GetComponent<Image>();
        _how = GetImage((int)Images.How).gameObject.GetComponent<Image>();
        _setting = GetImage((int)Images.Setting).gameObject.GetComponent<Image>();
        _exit = GetImage((int)Images.Exit).gameObject.GetComponent<Image>();

        BindEvent(_start.gameObject, StartGame, Define.UIEvent.Click);
        BindEvent(_how.gameObject, HowGame, Define.UIEvent.Click);
        BindEvent(_setting.gameObject, SettingGame, Define.UIEvent.Click);
        BindEvent(_exit.gameObject, ExitGame, Define.UIEvent.Click);
    }

    void StartGame(PointerEventData eventData)
    {
        MainManager.UI.HideUI(gameObject, Define.UI.Start);
        MainManager.UI.ShowUI("MoveUI", Define.UI.Move);
        MainManager.Game.StartGame();
    }

    void HowGame(PointerEventData eventData)
    {
        MainManager.UI.HideUI(gameObject, Define.UI.Start);
        MainManager.UI.ShowUI("HowUI", Define.UI.How);
    }

    void SettingGame(PointerEventData eventData)
    {
        MainManager.UI.HideUI(gameObject, Define.UI.Start);
        MainManager.UI.ShowUI("SettingUI", Define.UI.Setting);
    }

    void ExitGame(PointerEventData eventData)
    {
        MainManager.Game.ExitGame();
    }
}