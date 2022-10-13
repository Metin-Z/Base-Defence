using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class HelicopterComponent : MonoBehaviour
{
    RaycastHit collision;
    public GameObject mCamera;
    public GameObject RocketPrefab;
    public static bool rocketShoot;
    private void Awake()
    {
        //mCamera = CanvasManager.Instance.MainCamera;
    }
    private void Start()
    {
        rocketShoot = false;
        StartCoroutine(RocketTrue());
    }
    void Update()
    {
        transform.SetParent(EnemySpawner.Instance.Helicopter.transform.GetChild(0));
        transform.DOLocalMove(new Vector3(0,0,0),3);
        Vector3 targetOrigin;
        Vector3 targetPos;
        targetOrigin = new Vector3(Screen.width / 2, Screen.height / 2, 0);
        Ray Point = Camera.main.ScreenPointToRay(targetOrigin);

        if (rocketShoot == true)
        {
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
    public IEnumerator RocketTrue()
    {
        yield return new WaitForSeconds(3);
        rocketShoot = true;
    }
}
