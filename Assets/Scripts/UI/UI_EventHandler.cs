using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UI_EventHandler : MonoBehaviour, IPointerClickHandler, IPointerDownHandler
{
    public Action<PointerEventData> OnClickHandler = null;
    public Action<PointerEventData> OnDownHandler = null;

    public Action OnUpdateHandler = null;

    public void OnPointerClick(PointerEventData eventData)
    {
        if (OnClickHandler != null)
        {
            OnClickHandler.Invoke(eventData);
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (OnDownHandler != null)
        {
            OnDownHandler.Invoke(eventData);
        }
    }

    void Update()
    {
        if (OnUpdateHandler != null)
            OnUpdateHandler.Invoke();
    }
}
