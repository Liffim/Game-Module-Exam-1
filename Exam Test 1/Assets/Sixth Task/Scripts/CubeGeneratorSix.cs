using UnityEngine;

public class CubeGeneratorSix : MonoBehaviour
{
    public GameObject cubePrefab;
    public int gridSize = 20;
    public float minHeight = 1f;
    public float maxHeight = 5f;
    public float spacing = 1.1f;

    private GameObject[,] grid;

    void Start()
    {
        GenerateGrid();
    }

    void GenerateGrid()
    {
        grid = new GameObject[gridSize, gridSize];

        for (int x = 0; x < gridSize; x++)
        {
            for (int z = 0; z < gridSize; z++)
            {
                // Randomly skip some cubes to create holes
                if (Random.Range(0, 10) < 6) continue;

                float height = Random.Range(minHeight, maxHeight);
                Vector3 position = new Vector3(x * spacing, height / 2, z * spacing);
                GameObject cube = Instantiate(cubePrefab, position, Quaternion.identity);
                cube.transform.localScale = new Vector3(1, height, 1);
                grid[x, z] = cube;
            }
        }
    }

    public void RandomizeHeights()
    {
        foreach (GameObject cube in grid)
        {
            if (cube != null)
            {
                StartCoroutine(ChangeHeightSmoothly(cube, Random.Range(minHeight, maxHeight)));
            }
        }
    }

    private System.Collections.IEnumerator ChangeHeightSmoothly(GameObject cube, float targetHeight)
    {
        float initialHeight = cube.transform.localScale.y;
        float elapsedTime = 0f;
        float duration = 1f;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float newHeight = Mathf.Lerp(initialHeight, targetHeight, elapsedTime / duration);
            cube.transform.localScale = new Vector3(1, newHeight, 1);
            cube.transform.position = new Vector3(cube.transform.position.x, newHeight / 2, cube.transform.position.z);
            yield return null;
        }
    }
}