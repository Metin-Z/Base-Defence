using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyComponent : MonoBehaviour
{
    public float randomSpeed;
    public float shootRangeZ;
    public float shootRangeX;
    public GameObject Pistol;
    Vector3 target;
    public Animator anim;
    bool look;
    void Start()
    {
        look = true;
        randomSpeed = Random.Range(2, 6);
        shootRangeZ = Random.Range(3.5f, 7);
        shootRangeX = Random.Range(3.5f, 7);
        target = new Vector3(BaseComponent.Instance.transform.position.x + shootRangeX, 0, BaseComponent.Instance.transform.position.z + shootRangeZ);
    }

    void Update()
    {
        if (look== true)
        {
            transform.LookAt(BaseComponent.Instance.transform, new Vector3(transform.rotation.x, transform.rotation.y, 0));
        }
        
        transform.position = Vector3.MoveTowards(transform.position, target, randomSpeed * Time.deltaTime);
       
        if (transform.position == target)
        {
            anim.SetBool("Stop", true);
            look = false;
        }
    }
}
