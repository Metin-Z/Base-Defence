using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankComponent : MonoBehaviour
{
    Vector3 target;
    public GameObject TankBullet;
    public GameObject explosion;
    int shoot=0;
    public int health=30;
    Rigidbody rb;
    void Start()
    {
        target = new Vector3(3, 0, 1.77f);
        shoot++;
        transform.LookAt(target, new Vector3(transform.rotation.x, transform.rotation.y, 0));
        rb = GetComponent<Rigidbody>();
    }
    void Update()
    {
        if (health <= 0)
        {
            Destroy(transform.gameObject, 0.3f);
            Instantiate(explosion,transform.position,Quaternion.identity);
            shoot--;
        }
        transform.position = Vector3.MoveTowards(transform.position, target, 1 * Time.deltaTime);
        if (transform.position == target)
        {
            if (shoot ==1)
            {
                shoot--;
                GameObject barrel = transform.GetChild(0).gameObject;
                Instantiate(TankBullet, barrel.transform.position, Quaternion.identity);
                HealthScript.Instance.health_Value -= 250;
            }           
        }
    }
}