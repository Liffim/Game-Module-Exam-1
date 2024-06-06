using System.Collections;
using UnityEngine;

public class cube20 : MonoBehaviour
{
    private Vector3 initialScale;

    void Start()
    {
        initialScale = transform.localScale;
    }

    public void Initialize(float height)
    {
        transform.localScale = new Vector3(initialScale.x, height, initialScale.z);
    }

    public void RandomizeHeight()
    {
        float newHeight = Random.Range(0f, 5f); // Adjust range as needed
        StartCoroutine(SmoothHeightChange(newHeight));
    }

    IEnumerator SmoothHeightChange(float targetHeight)
    {
        float elapsedTime = 0f;
        float startHeight = transform.localScale.y;

        while (elapsedTime < 1f) // Change duration as needed
        {
            float height = Mathf.Lerp(startHeight, targetHeight, elapsedTime);
            transform.localScale = new Vector3(initialScale.x, height, initialScale.z);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.localScale = new Vector3(initialScale.x, targetHeight, initialScale.z);
    }

}