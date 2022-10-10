using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class MinigunComponent : MonoBehaviour
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
            ShotPos = new Vector3(collision.transform.position.x, collision.transform.position.y + 1.55f, collision.transform.position.z);

            CanvasManager.Instance.MainCamera.GetComponent<Camera>().DOFieldOfView(80, 0.3f).OnComplete(() =>
                CanvasManager.Instance.MainCamera.GetComponent<Camera>().DOFieldOfView(65, 0.2f));
            GameObject Obj = collision.transform.gameObject;
            Obj.GetComponent<EnemyComponent>().enabled = false;
            Obj.GetComponent<CapsuleCollider>().enabled = false;
            Obj.transform.GetChild(1).GetComponent<SkinnedMeshRenderer>().material.DOFade(0f, 3);
            Destroy(collision.transform.gameObject, 4.5f);
            Obj.transform.gameObject.GetComponent<Animator>().SetBool("Death", true);
            GameObject blood = Instantiate(EnemySpawner.Instance.blood, ShotPos, Quaternion.identity);
            GameObject pistol = collision.transform.gameObject.GetComponent<EnemyComponent>().Pistol;
            pistol.transform.parent = null;
            pistol.GetComponent<Rigidbody>().isKinematic = false;
            Destroy(pistol, 2.5f);
            Destroy(blood, 1.75f);
        }
    }
}
