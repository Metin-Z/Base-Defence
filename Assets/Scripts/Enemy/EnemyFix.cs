using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFix : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            Debug.Log("Düþman Ters Döndü");
            other.GetComponent<EnemyComponent>().look = false;
            other.transform.Rotate(transform.rotation.x, transform.rotation.y, 200, Space.Self);
        }
    }
}