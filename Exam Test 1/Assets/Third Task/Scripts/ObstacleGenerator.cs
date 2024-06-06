using UnityEngine;

public class ObstacleGenerator : MonoBehaviour
{
    public GameObject obstaclePrefab;
    public int numberOfObstacles = 20;
    public float corridorWidth = 10f;
    public float corridorLength = 50f;
    public float minObstacleHeight = 0.5f;
    public float maxObstacleHeight = 3f;

    void Start()
    {
        GenerateObstacles();
    }

    void GenerateObstacles()
    {
        for (int i = 0; i < numberOfObstacles; i++)
        {
            float xPosition = Random.Range(-corridorWidth / 2, corridorWidth / 2);
            float zPosition = Random.Range(-corridorLength / 2, corridorLength / 2);
            Vector3 obstaclePosition = new Vector3(xPosition, 0.5f, zPosition);
            GameObject newObstacle = Instantiate(obstaclePrefab, obstaclePosition, Quaternion.identity);

            float obstacleHeight = Random.Range(minObstacleHeight, maxObstacleHeight);
            newObstacle.transform.localScale = new Vector3(1, obstacleHeight, 1);
        }
    }
}
