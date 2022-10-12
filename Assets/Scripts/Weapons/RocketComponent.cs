using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketComponent : MonoBehaviour
{
    public Vector3 CollisionPos;
    public float radius = 45.0F;
    public float power = 1000.0F;
    public GameObject Explosion;
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, CollisionPos, 14 * Time.deltaTime);
        transform.LookAt(CollisionPos, new Vector3(transform.rotation.x, transform.rotation.y, transform.position.z));
    }
    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("sdsd");
        transform.GetComponent<BoxCollider>().enabled = false;
        transform.GetComponent<MeshRenderer>().enabled = false;
        Vector3 explosionPos = transform.position;
        Collider[] colliders = Physics.OverlapSphere(explosionPos, radius);
        Debug.Log(colliders);
        foreach (Collider hit in colliders)
        {
            Rigidbody rb = hit.GetComponent<Rigidbody>();
            Instantiate(Explosion, explosionPos, Quaternion.identity);
            Debug.Log("Roket Çarptý");

            if (rb != null)
            {
                rb.AddExplosionForce(power, explosionPos, radius, 3.0F);
                hit.GetComponent<Animator>().enabled = false;

                Destroy(hit, 4.5f);
            }
            Destroy(transform.gameObject, 5);
        }
    }
}
