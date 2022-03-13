using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UI_Move : UI_Base
{
    Button _left, _right, _jump;
    public bool IsLeft { get; private set; }
    public bool IsRight { get; private set; }
    public float Horizontal { get; private set; }
    public bool IsJump { get; set; }
    enum Buttons
    {
        Left,
        Right,
        Jump,
    }

    public override void Init()
    {
        Bind<Button>(typeof(Buttons));
        _left = GetButton((int)Buttons.Left).gameObject.GetComponent<Button>();
        _right = GetButton((int)Buttons.Right).gameObject.GetComponent<Button>();
        _jump = GetButton((int)Buttons.Jump).gameObject.GetComponent<Button>();

        BindEvent(gameObject, Move);

        BindEvent(_left.gameObject, LeftDownCheck, Define.UIEvent.Down);
        BindEvent(_left.gameObject, LeftUpCheck, Define.UIEvent.Up);
        BindEvent(_right.gameObject, RightDownCheck, Define.UIEvent.Down);
        BindEvent(_right.gameObject, RightUpCheck, Define.UIEvent.Up);

        BindEvent(_jump.gameObject, Jump, Define.UIEvent.Click);
    }
    
    void LeftDownCheck(PointerEventData eventData) { IsLeft = true; }
    void LeftUpCheck(PointerEventData eventData) { IsLeft = false; }
    void RightDownCheck(PointerEventData eventData) { IsRight = true; }
    void RightUpCheck(PointerEventData eventData) { IsRight = false; }

    void Move()
    {
        if (IsLeft)
            Horizontal = -1f;
        else if (IsRight)
            Horizontal = 1f;
        else
            Horizontal = 0f;
    }

    void Jump(PointerEventData eventData)
    {
        IsJump = true;
    }
}