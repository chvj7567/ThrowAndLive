using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Score : UI_Base
{
    enum Texts
    {
        Score,
    }

    Text _score;
    float _startTime;
    public float AvoidTime { get; private set; }

    float _record;

    public override void Init()
    {
        _record = -1;
        _startTime = Time.time;

        Bind<Text>(typeof(Texts));
        _score = GetText((int)Texts.Score);

        BindEvent(gameObject, AvoidRecord);
    }

    public void AvoidRecord()
    {
        AvoidTime = float.Parse((Time.time - _startTime).ToString("F0"));
        _score.text = $"Score : {AvoidTime * 10}";

        if (!MainManager.Game.IsGamimg)
        {
            if (_record == -1 || _record > AvoidTime)
            {
                _record = AvoidTime;

                Text record = Util.FindChild(MainManager.UI.Root, "Record", true).GetComponent<Text>();
                record.text = $"Score : {_record * 10}";
            }
        }
    }
}
