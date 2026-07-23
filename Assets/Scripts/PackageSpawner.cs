using UnityEngine;

public class PackageSpawner : MonoBehaviour
{
    [Header("Bản mẫu Gói hàng")]
    public GameObject packagePrefab; 

    [Header("Thời gian sinh đồ")]
    public float spawnInterval = 3f; 

    void Start()
    {
        InvokeRepeating("SpawnPackage", 1f, spawnInterval);
    }

    void SpawnPackage()
    {
        float randomX = Random.Range(-23f, 23f);
        float randomZ = Random.Range(-23f, 23f);

        Vector3 spawnPosition = new Vector3(randomX, 0.5f, randomZ);

        Instantiate(packagePrefab, spawnPosition, Quaternion.identity);
    }
}