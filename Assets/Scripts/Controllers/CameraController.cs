using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    GameObject _player;
    Vector3 cameraPos;

    void Awake()
    {
        transform.position = new Vector3(0, 0, -10f);
    }
    void LateUpdate()
    {
        Vector3 playerPos = _player.transform.position;
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