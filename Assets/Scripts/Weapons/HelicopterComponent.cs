using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class HelicopterComponent : MonoBehaviour
{
    RaycastHit collision;
    public GameObject mCamera;
    public GameObject RocketPrefab;
    private void Awake()
    {
        //mCamera = CanvasManager.Instance.MainCamera;
    }
    void Update()
    {
        CanvasManager.Instance.MainCamera.transform.position = EnemySpawner.Instance.Helicopter.transform.GetChild(0).position;
        Vector3 targetOrigin;
        Vector3 targetPos;
        targetOrigin = new Vector3(Screen.width / 2, Screen.height / 2, 0);
        Ray Point = Camera.main.ScreenPointToRay(targetOrigin);


        if (Physics.Raycast(Point, out collision, Mathf.Infinity))
        {
            if (CanvasManager.Instance.rocket_Count == 0)
            {
                CanvasManager.Instance.RocketDeactive();
            }
            Debug.Log("Ray Attý");
            mCamera.GetComponent<Camera>().DOFieldOfView(60, 0.3f);
            targetPos = collision.point;
            if (Input.GetMouseButtonUp(0))
            {
                if (CanvasManager.Instance.rocket_Count != 0)
                {
                    CanvasManager.Instance.rocket_Count--;
                    Debug.Log("Roket Oluþturuldu");
                    GameObject rocketObj = Instantiate(RocketPrefab, transform.position, Quaternion.identity);
                    rocketObj.GetComponent<RocketComponent>().CollisionPos = targetPos;
                }
            }
        }
    }
}
