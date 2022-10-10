using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public static CameraController Instance;
    public Vector3 spawnPos;

    public float speedH = 1f;
    public float speedV = 1f;

    private float yaw = 0f;
    private float pitch = 0f;

    public float yawL = 20f;
    public float yawR = 30f;

    public float pitchL = -45f;
    public float pitchR = -25f;
    
    private void Awake()
    {
        Instance = this;
        spawnPos = transform.position;
    }
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            pitch += speedH * Input.GetAxis("Mouse X");
            yaw -= speedV * Input.GetAxis("Mouse Y");

            yaw = Mathf.Clamp(yaw, yawL, yawR);
            //the rotation range
            pitch = Mathf.Clamp(pitch, pitchL, pitchR);
            //the rotation range

            transform.eulerAngles = new Vector3(yaw, pitch, 0.0f);
        }
        
    }
}
