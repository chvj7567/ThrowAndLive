using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : BaseController
{
    GameObject _player;
    Vector3 cameraPos;
    float _aspectRatio;
    float _verticalRatio;

    public override void Init()
    {
        transform.position = new Vector3(0, 0, -10f);
        _aspectRatio = 19f;
        _verticalRatio = 9f;

        Rect rect = Camera.main.rect;
        float scaleHeight = ((float)Screen.width / Screen.height) / (_aspectRatio / _verticalRatio);
        float scaleWidth = 1f / scaleHeight;

        if (scaleHeight < 1)
        {
            rect.height = scaleHeight;
            rect.y = (1f - scaleHeight) / 2f;
        }
        else
        {
            rect.width = scaleWidth;
            rect.x = (1f - scaleWidth) / 2f;
        }

        Camera.main.rect = rect;
    }

    void LateUpdate()
    {
        Vector3 playerPos;

        if (_player == null)
            playerPos = Vector3.zero;
        else
            playerPos = _player.transform.position;

        transform.position = new Vector3(playerPos.x + cameraPos.x, transform.position.y, transform.position.z);
    }

    public void SetPlayer(GameObject player)
    {
        _player = player;
    }

    public void SetCamera()
    {
        if (_player != null)
            cameraPos = transform.position - _player.transform.position;
        else
            Debug.Log("Player is not Setting");
    }

    public void SetSightCamera(float sight)
    {
        Camera cam = Util.GetOrAddComponent<Camera>(gameObject);
        cam.orthographicSize += sight;
    }
}