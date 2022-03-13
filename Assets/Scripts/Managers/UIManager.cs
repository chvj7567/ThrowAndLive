using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager
{
    public GameObject Start { get; private set; }
    public GameObject Setting { get; private set; }
    public GameObject How { get; private set; }
    public GameObject Move { get; private set; }
    public GameObject TimeScore { get; private set; }
    public GameObject End { get; private set; }

    int _order = 1;
    public GameObject Root
    {
        get
        {
            GameObject root = GameObject.Find("@UI_Root");

            if (root == null)
            {
                root = new GameObject { name = "@UI_Root" };

            }
            return root;
        }
    }

    public void SetCanvas(GameObject go, bool sort = true)
    {
        Canvas canvas = Util.GetOrAddComponent<Canvas>(go);
        canvas.renderMode = RenderMode.ScreenSpaceOverlay;

        canvas.worldCamera = Camera.main;
        canvas.overrideSorting = true;

        if (sort)
        {
            canvas.sortingOrder = _order;
            _order++;
        }
        else
        {
            canvas.sortingOrder = 0;

        }
    }

    public GameObject ShowUI(string name, Define.UI type)
    {
        GameObject go = MainManager.Resource.Instantiate($"UI/{name}");

        switch (type)
        {
            case Define.UI.Start:
                if (Start != null)
                    return Start;
                Start = go;
                break;
            case Define.UI.Setting:
                if (Setting != null)
                    return Setting;
                Setting = go;
                break;
            case Define.UI.How:
                if (How != null)
                    return How;
                How = go;
                break;
            case Define.UI.Move:
                if (Move != null)
                    return Move;
                Move = go;
                break;
            case Define.UI.TimeScore:
                if (TimeScore != null)
                    return TimeScore;
                TimeScore = go;
                break;
            case Define.UI.End:
                if (End != null)
                    return End;
                End = go;
                break;
        }

        go.transform.SetParent(Root.transform);
        SetCanvas(go);

        return go;
    }

    public void HideUI(GameObject ui, Define.UI type)
    {
        switch (type)
        {
            case Define.UI.Start:
                Start = null;
                break;
            case Define.UI.Setting:
                Setting = null;
                break;
            case Define.UI.How:
                How = null;
                break;
            case Define.UI.Move:
                Move = null;
                break;
            case Define.UI.TimeScore:
                TimeScore = null;
                break;
            case Define.UI.End:
                End = null;
                break;
        }

        ui.SetActive(false);
    }
}