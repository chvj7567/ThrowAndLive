using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_Setting : UI_Base
{
    Slider _volume;
    Image _back;

    enum Sliders
    {
        VolumeSlider,
    }

    enum Images
    {
        Back,
    }
    public override void Init()
    {
        Bind<Slider>(typeof(Sliders));
        Bind<Image>(typeof(Images));

        _volume = Get<Slider>((int)Sliders.VolumeSlider);
        _back = GetImage((int)Images.Back);

        BindEvent(_volume.gameObject, SliderVolume);
        BindEvent(_back.gameObject, BackGame, Define.UIEvent.Click);
    }

    public void SliderVolume()
    {
        //MainManager.Audio.SetVolume(_volume.value);
    }

    public void BackGame(PointerEventData data)
    {
        MainManager.UI.HideUI(gameObject, Define.UI.Setting);
        MainManager.UI.ShowUI("StartUI", Define.UI.Start);
    }
}
