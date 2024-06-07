using System.Collections;
using UnityEngine;

public class cube20 : MonoBehaviour
{
    private CubeGeneratorSix gridManager;

    void Start()
    {
        gridManager = FindObjectOfType<CubeGeneratorSix>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            gridManager.RandomizeHeights();
        }
    }

}