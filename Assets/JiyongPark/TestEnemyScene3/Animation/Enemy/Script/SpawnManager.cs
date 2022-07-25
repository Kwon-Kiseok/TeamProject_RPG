using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HOGUS.Scripts.Manager;

public class SpawnManager : MonoBehaviour, IUpdatableObject
{
    public int maxCount;
    public int enemyCount;
    public float spawnTime = 3f;
    public float curTime;
    public Transform[] spawnPoints;
    public bool[] isSpawn;
    public GameObject[] enemy;

    // 퀘스트를 다꺠면 enemy 배열 증가해서 또 스폰. 퀘스트 다꺠면 isSpawn false로..
    // 그다음 스폰을 위해 배열 우측으로 한개 이동 
    // 마지막은 보스 소환.

    public static SpawnManager instance;
    
    void Start()
    {
        isSpawn = new bool[spawnPoints.Length];
        for (int i =0; i < spawnPoints.Length; i++)
        {
            isSpawn[i] = false;
        }
        instance = this;
    }
    public void OnEnable()
    {
        UpdateManager.Instance.RegisterUpdatableObject(this);
    }

    public void OnDisable()
    {
        if (UpdateManager.Instance != null)
        {
            UpdateManager.Instance.DeregisterUpdatableObject(this);
        }
    }

    public void OnFixedUpdate(float deltaTime)
    {

    }

    [System.Obsolete]
    public void OnUpdate(float deltaTime)
    {
        if(curTime >= spawnTime && enemyCount < maxCount)
        {
            int x = Random.RandomRange(0,spawnPoints.Length);
            if (!isSpawn[x])
            {
                SpawnEnemy(x);
            }
        }
        curTime += Time.deltaTime;
    }

    public void SpawnEnemy(int ranNum)
    {
        curTime = 0;
        enemyCount++;
        Instantiate(enemy[0], spawnPoints[ranNum]);
        isSpawn[ranNum] = true;
    }
}