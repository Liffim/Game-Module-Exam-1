using UnityEngine;

public class TimeController : MonoBehaviour
{
    public DayNightCycle dayNightCycle;
    public float timeScale = 1f;

    void Update()
    {
        // Update time scale based on user input
        if (Input.GetKeyDown(KeyCode.Space))
        {
            // Toggle pause/resume
            if (Time.timeScale == 0f)
                Time.timeScale = timeScale;
            else
                Time.timeScale = 0f;
        }

        // Speed up time
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            Time.timeScale *= 2f;
        }

        // Slow down time
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            Time.timeScale /= 2f;
        }

        // Reset time scale
        if (Input.GetKeyDown(KeyCode.R))
        {
            Time.timeScale = 1f;
        }
    }
}
