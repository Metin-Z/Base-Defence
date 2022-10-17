using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketComponent : MonoBehaviour
{
    public Vector3 CollisionPos;
    public float radius = 10;
    public float power = 1000.0F;
    public GameObject Explosion;
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, CollisionPos, 14 * Time.deltaTime);
        transform.LookAt(CollisionPos, new Vector3(transform.rotation.x, transform.rotation.y, transform.position.z));
    }
    private void OnCollisionEnter(Collision collision)
    {
        transform.GetComponent<BoxCollider>().enabled = false;
        transform.GetComponent<MeshRenderer>().enabled = false;
        Vector3 explosionPos = transform.position;
        Collider[] colliders = Physics.OverlapSphere(explosionPos, radius);
        Instantiate(Explosion, explosionPos, Quaternion.identity);
        foreach (Collider hit in colliders)
        {
            Rigidbody rb = hit.GetComponent<Rigidbody>();
            Debug.Log("Roket Çarptý");
            if (hit.gameObject.tag == "Enemy")
            {
                if (rb != null)
                {
                    rb.AddExplosionForce(power, explosionPos, radius, 3.0F);
                    hit.GetComponent<Animator>().enabled = false;
                    Destroy(hit.GetComponent<EnemyComponent>());
                    GameObject pistol = hit.transform.gameObject.GetComponent<EnemyComponent>().Pistol;
                    pistol.transform.parent = null;
                    Destroy(pistol.GetComponent<PistolComponent>());
                    pistol.GetComponent<Rigidbody>().isKinematic = false;
                    pistol.GetComponent<Animation>().enabled = false;
                    pistol.GetComponent<Rigidbody>().AddForce(pistol.transform.up * 120 + pistol.transform.forward * 60);
                    Destroy(pistol, 2.5f);
                    Destroy(hit.gameObject, 4.5f);
                    EnemySpawner.Instance.Active_Enemies.Remove(hit.gameObject);
                }
            }
            Destroy(transform.gameObject, 3);
        }
    }
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(transform.position, radius);
    }
}
