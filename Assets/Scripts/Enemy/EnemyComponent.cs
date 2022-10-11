using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyComponent : MonoBehaviour
{
    public float randomSpeed;
    public float shootRangeZ;
    public float shootRangeX;
    public GameObject Pistol;
    public Vector3 target;
    public Animator anim;
    bool look;
    bool targetOn = true;
    bool lookBase = true;
    void Start()
    {
        look = true;
        randomSpeed = Random.Range(3.5f, 5);
        shootRangeZ = Random.Range(0.3f, 6f);
        shootRangeX = Random.Range(-4, 10);
        target = new Vector3(shootRangeX,0,shootRangeZ);
        
        if (look == true)
        {
            transform.LookAt(target, new Vector3(transform.rotation.x, transform.rotation.y, 0));
        }
    }

    void Update()
    {
        NavMeshAgent nMesh = GetComponent<NavMeshAgent>();
        nMesh.speed = randomSpeed;
        if (targetOn == true)
        {
            nMesh.destination = target;
        }    
        if (Mathf.Abs( target.x-transform.position.x) < 0.55f && Mathf.Abs(target.z - transform.position.z) < 0.55f)
        {
            targetOn = false;
            nMesh.enabled = false;
            anim.SetBool("Stop", true);           
            look = false;
            LookBase();
        }
    }
    public void LookBase()
    {
        if (lookBase == true)
        {
            Pistol.GetComponent<Animation>().enabled = true;
            Pistol.GetComponent<PistolComponent>().enabled = true;
            lookBase = false;
            Debug.Log("Baseye Bakýldý");         
            transform.LookAt(BaseComponent.Instance.transform, new Vector3(transform.rotation.x, transform.rotation.y, 0));
        }
    }
}
