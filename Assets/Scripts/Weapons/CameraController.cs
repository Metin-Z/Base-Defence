using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public static CameraController Instance;
    public float speedH = 1f;
    public float speedV = 1f;

    private float yaw = 0f;
    private float pitch = 0f;
    private void Awake()
    {
        Instance = this;
    }
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            pitch += speedH * Input.GetAxis("Mouse X");
            yaw -= speedV * Input.GetAxis("Mouse Y");

            yaw = Mathf.Clamp(yaw, 20f, 30f);
            //the rotation range
            pitch = Mathf.Clamp(pitch, -45f, -25f);
            //the rotation range

            transform.eulerAngles = new Vector3(yaw, pitch, 0.0f);
        }
        
    }
}
