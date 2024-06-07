using UnityEngine;

public class CloudManager : MonoBehaviour
{
    public GameObject cloudPrefab;
    public int cloudCount = 10;
    public float moveSpeed = 1f;
    public Vector3 gatherPoint = new Vector3(0, 5, 0);
    public float rainDuration = 10f;
    public Vector3 scatterArea = new Vector3(50, 10, 50);

    private GameObject[] clouds;
    private ParticleSystem[] rainSystems;
    private Vector3[] scatterTargets;
    private float timer;
    private bool rainStarted = false;

    void Start()
    {
        clouds = new GameObject[cloudCount];
        rainSystems = new ParticleSystem[cloudCount];
        scatterTargets = new Vector3[cloudCount];

        for (int i = 0; i < cloudCount; i++)
        {
            Vector3 position = new Vector3(Random.Range(-scatterArea.x, scatterArea.x),
                                           Random.Range(5, scatterArea.y),
                                           Random.Range(-scatterArea.z, scatterArea.z));
            clouds[i] = Instantiate(cloudPrefab, position, Quaternion.identity);
            rainSystems[i] = clouds[i].GetComponentInChildren<ParticleSystem>();
        }
    }

    void Update()
    {
        timer += Time.deltaTime;

        if (timer <= 10)
        {
            MoveClouds();
            StopRain();
        }
        else if (timer <= 20)
        {
            GatherClouds();
            if (!rainStarted && timer > 10)
            {
                StartRain();
                rainStarted = true;
            }
        }
        else if (timer <= 30)
        {
            if (rainStarted)
            {
                StopRain();
                rainStarted = false;
            }
            ScatterClouds();
        }
        else
        {
            ResetClouds();
        }
    }

    void MoveClouds()
    {
        foreach (GameObject cloud in clouds)
        {
            cloud.transform.Translate(Vector3.right * moveSpeed * Time.deltaTime);
        }
    }

    void GatherClouds()
    {
        foreach (GameObject cloud in clouds)
        {
            cloud.transform.position = Vector3.Lerp(cloud.transform.position, gatherPoint, moveSpeed * Time.deltaTime);
            cloud.transform.localScale = Vector3.Lerp(cloud.transform.localScale, new Vector3(2, 2, 2), Time.deltaTime);
        }
    }

    void StartRain()
    {
        foreach (ParticleSystem rain in rainSystems)
        {
            rain.Play();
        }
        Debug.Log("It's raining!");
    }

    void StopRain()
    {
        foreach (ParticleSystem rain in rainSystems)
        {
            rain.Stop();
        }
        Debug.Log("Rain stopped!");
    }

    void ScatterClouds()
    {
        for (int i = 0; i < clouds.Length; i++)
        {
            if (scatterTargets[i] == Vector3.zero)
            {
                scatterTargets[i] = new Vector3(Random.Range(-scatterArea.x, scatterArea.x),
                                                Random.Range(5, scatterArea.y),
                                                Random.Range(-scatterArea.z, scatterArea.z));
            }
            clouds[i].transform.position = Vector3.Lerp(clouds[i].transform.position, scatterTargets[i], moveSpeed * Time.deltaTime);
            clouds[i].transform.localScale = Vector3.Lerp(clouds[i].transform.localScale, new Vector3(1, 1, 1), Time.deltaTime);
        }
    }

    void ResetClouds()
    {
        timer = 0;
        rainStarted = false;
        scatterTargets = new Vector3[cloudCount];
        foreach (GameObject cloud in clouds)
        {
            cloud.transform.localScale = new Vector3(1, 1, 1);
            if (cloud.GetComponentInChildren<ParticleSystem>().isPlaying)
            {
                cloud.GetComponentInChildren<ParticleSystem>().Stop();
            }
        }
    }
}
