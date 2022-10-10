using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CanvasManager : MonoBehaviour
{
    public GameObject MainCamera;
    public GameObject ButtonGroup;

    public GameObject SniperCross;

    public void SniperActive()
    {
        MainCamera.GetComponent<SniperComponent>().enabled = true;
        MainCamera.GetComponent<Camera>().DOFieldOfView(10, 0.5f);

        ButtonGroup.SetActive(false);
        SniperCross.SetActive(true);
        CameraController.Instance.speedH = 0.4f;
        CameraController.Instance.speedV = 0.4f;
    }
    public void SniperDeactive()
    {
        MainCamera.GetComponent<SniperComponent>().enabled = false;
        MainCamera.GetComponent<Camera>().DOFieldOfView(27, 0.5f);

        ButtonGroup.SetActive(true);
        SniperCross.SetActive(false);
        CameraController.Instance.speedH = 2f;
        CameraController.Instance.speedV = 2f;
    }
}
