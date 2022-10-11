using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class EnemySpawner : MonoBehaviour
{
    public static EnemySpawner Instance;
    public List<GameObject> Enemies;
    public List<GameObject> Active_Enemies;
    public GameObject blood;

    int spawnTime;
    int enemySkin;
    float spawnX;
    float spawnZ;
    public bool Spawn = true;
    private void Awake()
    {
        Instance = this;
        Spawn = true;
    }
    private void Start()
    {
        StartCoroutine(SpawnEnemy());
    }
    public IEnumerator SpawnEnemy()
    {
        spawnTime = Random.Range(1, 3);
        yield return new WaitForSeconds(spawnTime);
        if (Spawn == true)
        {
            enemySkin = Random.Range(0, 3);
            spawnX = Random.Range(6.444f, 12.14f);
            spawnZ = Random.Range(4.49f, 13.272f);
            GameObject NewEnemy = Instantiate(Enemies[enemySkin], new Vector3(spawnX, 0, spawnZ), Quaternion.identity);
            Active_Enemies.Add(NewEnemy);
            StartCoroutine(SpawnEnemy());
        }
       
    }
}
