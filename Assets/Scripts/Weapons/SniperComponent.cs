using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.AI;

public class SniperComponent : MonoBehaviour
{
    RaycastHit collision;
    GameObject mCamera;
    private void Awake()
    {
        mCamera = CanvasManager.Instance.MainCamera;
    }
    void Update()
    {
        Vector3 targetOrigin;
        Vector3 ShotPos;
        targetOrigin = new Vector3(Screen.width / 2, Screen.height / 2, 0);
        Ray Point = Camera.main.ScreenPointToRay(targetOrigin);
        int layer = 7;
        //int layer2 = 8;
        if (Input.GetMouseButtonUp(0))
        {

        

        if (Physics.Raycast(Point, out collision, Mathf.Infinity, 1 << layer))
        {
            ShotPos = new Vector3(collision.transform.position.x, collision.transform.position.y + 1.55f, collision.transform.position.z);
            CanvasManager.Instance.miniUse--;
            mCamera.GetComponent<Camera>().DOFieldOfView(20, 0.3f).OnComplete(() =>
               mCamera.GetComponent<Camera>().DOFieldOfView(10, 0.2f));
            GameObject Obj = collision.transform.gameObject;
            Obj.GetComponent<EnemyComponent>().enabled = false;
            Obj.GetComponent<NavMeshAgent>().enabled = false;
            Obj.GetComponent<CapsuleCollider>().enabled = false;
            Obj.transform.GetChild(1).GetComponent<SkinnedMeshRenderer>().materials[0].DOFade(0.0f, 3);
            EnemySpawner.Instance.Active_Enemies.Remove(Obj);
            Destroy(Obj, 4.5f);
            Obj.gameObject.GetComponent<Animator>().enabled = false;
            GameObject blood = Instantiate(EnemySpawner.Instance.blood, ShotPos, Quaternion.identity);
            GameObject pistol = collision.transform.gameObject.GetComponent<EnemyComponent>().Pistol;
            pistol.transform.parent = null;
            pistol.GetComponent<Rigidbody>().isKinematic = false;
            pistol.GetComponent<Animation>().enabled = false;
            Destroy(pistol.GetComponent<PistolComponent>());
            pistol.GetComponent<Rigidbody>().AddForce(pistol.transform.up * 120);
            pistol.GetComponent<Rigidbody>().AddForce(pistol.transform.forward * 60);
            Destroy(pistol, 4.5f);
            Destroy(blood, 1.75f);
        }
        }
    }
}