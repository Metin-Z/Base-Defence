using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PistolComponent : MonoBehaviour
{
    public GameObject Bullet;
    public GameObject Barrel;
    private void Start()
    {
        StartCoroutine(Fire());
    }
    public IEnumerator Fire()
    {
        yield return new WaitForSeconds(0.4f);
        Debug.Log("Ateþ Edildi");
        Instantiate(Bullet, Barrel.transform.position, Quaternion.identity);
        StartCoroutine(Fire());
    }
}
