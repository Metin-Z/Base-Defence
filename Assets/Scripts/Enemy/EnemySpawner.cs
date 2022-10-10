using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class EnemySpawner : MonoBehaviour
{
    public List<GameObject> Enemies;

    int spawnTime;
    int enemySkin;
    float spawnX;
    float spawnZ;
    private void Start()
    {
        StartCoroutine(SpawnEnemy());
    }
    public IEnumerator SpawnEnemy()
    {
        spawnTime = Random.Range(2, 5);
        yield return new WaitForSeconds(spawnTime);
        enemySkin = Random.Range(0, 3);
        spawnX = Random.Range(6.444f, 12.14f);
        spawnZ = Random.Range(4.49f, 13.272f);
        Instantiate(Enemies[enemySkin],new Vector3(spawnX,0,spawnZ),Quaternion.identity);
        StartCoroutine(SpawnEnemy());
    }
}
