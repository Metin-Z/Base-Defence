using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.AI;

public class SniperComponent : MonoBehaviour
{
    RaycastHit collision;
    RaycastHit tank;
    GameObject mCamera;
    public GameObject bulletCol;
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
        int layer2 = 8;
        if (Input.GetMouseButtonUp(0))
        {
            if (Physics.Raycast(Point, out collision, Mathf.Infinity, 1 << layer))
            {
                if (collision.transform.gameObject.CompareTag("Enemy"))
                {
                    ShotPos = new Vector3(collision.transform.position.x, collision.transform.position.y + 1.55f, collision.transform.position.z);
                    CanvasManager.Instance.miniUse--;
                    mCamera.GetComponent<Camera>().DOFieldOfView(20, 0.3f).OnComplete(() =>
                       mCamera.GetComponent<Camera>().DOFieldOfView(10, 0.2f));
                    GameObject Obj = collision.transform.gameObject;
                    if (Obj.GetComponent<EnemyComponent>())
                        Obj.GetComponent<EnemyComponent>().enabled = false;
                    if (Obj.GetComponent<NavMeshAgent>())
                        Obj.GetComponent<NavMeshAgent>().enabled = false;
                    //Obj.GetComponent<CapsuleCollider>().enabled = false;
                    Obj.transform.GetChild(1).GetComponent<SkinnedMeshRenderer>().materials[0].DOFade(0.0f, 3);
                    EnemySpawner.Instance.Active_Enemies.Remove(Obj);
                    Destroy(Obj, 4.5f);
                    Obj.gameObject.GetComponent<Animator>().enabled = false;

                    foreach (Rigidbody item in Obj.GetComponentsInChildren<Rigidbody>())
                    {
                        item.isKinematic = false;
                        item.velocity = Vector3.zero;
                        item.angularVelocity = Vector3.zero;
                    }
                    GameObject blood = Instantiate(EnemySpawner.Instance.blood, collision.point, Quaternion.identity);
                    GameObject pistol = collision.transform.gameObject.GetComponent<EnemyComponent>().Pistol;
                    pistol.transform.parent = null;
                    pistol.GetComponent<Rigidbody>().isKinematic = false;
                    pistol.GetComponent<Animation>().enabled = false;
                    Destroy(pistol.GetComponent<PistolComponent>());
                    pistol.GetComponent<Rigidbody>().AddForce(pistol.transform.up * 120 + pistol.transform.forward * 60);
                    Destroy(pistol, 4.5f);
                    Destroy(blood, 1.75f);
                }
            }
            if (Physics.Raycast(Point, out tank, Mathf.Infinity, 1 << layer2))
            {
                if (tank.transform.gameObject.CompareTag("Tank"))
                {
                    mCamera.GetComponent<Camera>().DOFieldOfView(20, 0.3f).OnComplete(() =>
                       mCamera.GetComponent<Camera>().DOFieldOfView(10, 0.2f));
                    tank.transform.GetComponent<TankComponent>().health -= 2;
                    Instantiate(bulletCol, tank.point, Quaternion.identity);
                }
            }
        }
    }
}