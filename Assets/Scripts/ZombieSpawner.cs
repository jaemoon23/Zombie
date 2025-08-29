using System.Collections.Generic;
using System.Net.Http.Headers;
using UnityEngine;

public class ZombieSpawner : MonoBehaviour
{
    public Uimanager uiManager;

    public Zombie prefab;

    public ZombieData[] zombieDatas;
    public Transform[] spwanPoints;

    private List<Zombie> zombies = new List<Zombie>();

    private int wave;

    public void Update()
    {
        if (zombies.Count == 0)
        {
            SpawnWave();
        }
    }
    private void SpawnWave()
    {
        wave++;
        int count = Mathf.RoundToInt(wave * 1.5f);
        for (int i = 0; i < count; i++)
        {
            CreateZombie();
        }
        uiManager.SetWaveInfo(wave, zombies.Count);
    }

    public void CreateZombie()
    {
        var point = spwanPoints[Random.Range(0, spwanPoints.Length)];

        var zombie = Instantiate(prefab, point.position, point.rotation);
        zombie.SetUp(zombieDatas[Random.Range(0, zombieDatas.Length)]);
        zombies.Add(zombie);

        zombie.OnDeath += () => zombies.Remove(zombie);
        zombie.OnDeath += () => uiManager.SetWaveInfo(wave, zombies.Count);
        zombie.OnDeath += () => Destroy(zombie.gameObject, 5f);
    }
}
