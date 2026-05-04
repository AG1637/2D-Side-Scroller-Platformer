using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CameraZoom : MonoBehaviour
{
    public static CameraZoom instance;
    public Camera mainCamera;
    public float scrollSpeed = 10f;
    public bool panelOpen = false;

    public int minZoom = 25;
    public int maxZoom = 75;

    private void Start()
    {
        instance = this;
    }

    private void Update()
    {
        mainCamera.fieldOfView -= Input.GetAxis("Mouse ScrollWheel") * scrollSpeed;

        if (mainCamera.fieldOfView < minZoom)
        {
            mainCamera.fieldOfView = minZoom;
        }
        else if (mainCamera.fieldOfView > maxZoom)
        {
            mainCamera.fieldOfView = maxZoom;
        }

        if (panelOpen == true)
        {
            scrollSpeed = 0f;
        }
        else
        {
            scrollSpeed = 10f;
        }
    }
}
