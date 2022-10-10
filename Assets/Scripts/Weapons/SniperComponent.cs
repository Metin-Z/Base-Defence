using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class SniperComponent : MonoBehaviour
{
    RaycastHit collision;
    void Update()
    {
        Vector3 targetOrigin;
        Vector3 ShotPos;
        targetOrigin = new Vector3(Screen.width / 2, Screen.height / 2, 0);
        Ray Point = Camera.main.ScreenPointToRay(targetOrigin);
        int layer = 7;
        //int layer2 = 8;
        if (Physics.Raycast(Point, out collision, Mathf.Infinity, 1 << layer))
        {
            ShotPos = new Vector3(collision.transform.position.x, collision.transform.position.y+1.55f, collision.transform.position.z);
            CanvasManager.Instance.miniUse--;
            CanvasManager.Instance.MainCamera.GetComponent<Camera>().DOFieldOfView(30, 0.1f);
            CanvasManager.Instance.MainCamera.GetComponent<Camera>().DOFieldOfView(10, 0.1f);
            collision.transform.gameObject.GetComponent<EnemyComponent>().enabled = false;
            collision.transform.gameObject.GetComponent<CapsuleCollider>().enabled = false;
            collision.transform.GetChild(1).GetComponent<SkinnedMeshRenderer>().material.DOFade(0.1f, 3);
            Destroy(collision.transform.gameObject, 4.5f);
            collision.transform.gameObject.GetComponent<Animator>().SetBool("Death", true);
            GameObject blood = Instantiate(EnemySpawner.Instance.blood,ShotPos,Quaternion.identity);
            Destroy(blood, 1.75f);
        }
    }
}