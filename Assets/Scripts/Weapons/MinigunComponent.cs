using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.AI;

public class MinigunComponent : MonoBehaviour
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

        if (Physics.Raycast(Point, out collision, Mathf.Infinity, 1 << layer))
        {
            ShotPos = new Vector3(collision.transform.position.x, collision.transform.position.y + 1.55f, collision.transform.position.z);
            Sequence Shake = DOTween.Sequence();
            Shake.Append(mCamera.GetComponent<Camera>().DOFieldOfView(80, 0.3f).OnComplete(() =>
            mCamera.GetComponent<Camera>().DOFieldOfView(65, 0.2f)));
            GameObject Obj = collision.transform.gameObject;
            Obj.GetComponent<EnemyComponent>().enabled = false;
            Obj.GetComponent<NavMeshAgent>().enabled = false;
            Obj.GetComponent<CapsuleCollider>().enabled = false;
            Obj.transform.GetChild(1).GetComponent<SkinnedMeshRenderer>().materials[0].DOFade(0f, 3);
            Destroy(Obj, 4.5f);
            Obj.transform.gameObject.GetComponent<Animator>().SetBool("Death", true);
            GameObject blood = Instantiate(EnemySpawner.Instance.blood, ShotPos, Quaternion.identity);
            GameObject pistol = collision.transform.gameObject.GetComponent<EnemyComponent>().Pistol;
            pistol.transform.parent = null;
            pistol.GetComponent<Rigidbody>().isKinematic = false;
            pistol.GetComponent<Animation>().enabled = false;
            Destroy(pistol.GetComponent<PistolComponent>());
            pistol.GetComponent<Rigidbody>().AddForce(transform.up * 15);
            Destroy(pistol, 4.5f);
            Destroy(blood, 1.75f);
        }
    }
}
