using UnityEngine;

public class CubeGeneratorSix : MonoBehaviour
{
    public GameObject cubePrefab;
    public int gridSize = 20;
    public float maxCubeHeight = 5f;
    public float minCubeHeight = 0f;

    private cube20[,] grid;

    void Start()
    {
        GenerateGrid();
    }

    void GenerateGrid()
    {
        grid = new cube20[gridSize, gridSize];

        for (int x = 0; x < gridSize; x++)
        {
            for (int z = 0; z < gridSize; z++)
            {
                if (Random.Range(0f, 1f) < 0.8f) // 80% chance to generate cube
                {
                    GameObject cubeObj = Instantiate(cubePrefab, new Vector3(x, 0, z), Quaternion.identity);
                    cube20 cube = cubeObj.GetComponent<cube20>();
                    if (cube != null)
                    {
                        float randomHeight = Random.Range(minCubeHeight, maxCubeHeight);
                        cube.Initialize(randomHeight);
                        grid[x, z] = cube;
                    }
                }
            }
        }
    }

    public void RandomizeAllCubeHeights()
    {
        foreach (cube20 cube in grid)
        {
            if (cube != null)
            {
                cube.RandomizeHeight();
            }
        }
    }
}