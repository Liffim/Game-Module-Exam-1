using UnityEngine;

public class CubeGenerator : MonoBehaviour
{
    public GameObject cubePrefab;
    public int numberOfCubes = 10;
    public float spacing = 2f;

    void Start()
    {
        for (int i = 0; i < numberOfCubes; i++)
        {
            Vector3 position = new Vector3(i * spacing, 0, 0);
            GameObject cube = Instantiate(cubePrefab, position, Quaternion.identity);
            float randomScale = Random.Range(1f, 3f);
            cube.transform.localScale = new Vector3(1, randomScale, 1);
        }
    }
}
