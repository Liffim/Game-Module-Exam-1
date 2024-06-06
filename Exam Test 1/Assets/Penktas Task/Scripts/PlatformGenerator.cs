using UnityEngine;

public class PlatformGenerator : MonoBehaviour
{
    public GameObject platformPrefab;
    public float spawnInterval = 2f;
    public float platformSpeed = 5f;

    private float timer = 0f;

    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= spawnInterval)
        {
            SpawnPlatform();
            timer = 0f;
        }
    }

    void SpawnPlatform()
    {
        Vector3 spawnPos = new Vector3(Random.Range(-5f, 5f), 0f, transform.position.z);
        GameObject newPlatform = Instantiate(platformPrefab, spawnPos, Quaternion.identity);
        Rigidbody platformRb = newPlatform.GetComponent<Rigidbody>();
        if (platformRb != null)
        {
            platformRb.velocity = Vector3.back * platformSpeed;
        }
    }
}

