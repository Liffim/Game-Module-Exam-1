using UnityEngine;

public class DayNightCycle : MonoBehaviour
{
    [Header("Day Time Settings")]
    [Range(0, 1)]
    [SerializeField]
    private float initialDayTimeProgress;

    [Min(0f)]
    [SerializeField]
    private float dayLengthInSeconds = 120f;

    [SerializeField]
    private float sunSpeed = 1f;

    [Header("Rain Settings")]
    [SerializeField]
    private ParticleSystem rainParticleSystem;

    [SerializeField]
    private float rainIntensityMultiplier = 1f;

    [Header("Fire Settings")]
    [SerializeField]
    private ParticleSystem fireParticleSystem;

    [SerializeField]
    private float fireIntensityMultiplier = 1f;

    [SerializeField]
    private Light sun;

    private void Update()
    {
        UpdateDayTimeProgress();
        UpdateSunRotation();
        UpdateRainIntensity();
        UpdateFireIntensity();
    }

    private void UpdateDayTimeProgress()
    {
        initialDayTimeProgress += Time.deltaTime / dayLengthInSeconds;
        if (initialDayTimeProgress > 1f)
        {
            initialDayTimeProgress = 0f;
        }
    }

    private void UpdateSunRotation()
    {
        float angle = Mathf.Lerp(0f, 360f, initialDayTimeProgress);
        Quaternion rotation = Quaternion.Euler(angle, 0, 0);
        sun.transform.rotation = rotation;
    }

    private void UpdateRainIntensity()
    {
        float rainIntensity = Mathf.Clamp01(1 - initialDayTimeProgress) * rainIntensityMultiplier;
        var rainEmission = rainParticleSystem.emission;
        rainEmission.rateOverTime = rainIntensity;
    }

    private void UpdateFireIntensity()
    {
        float fireIntensity = Mathf.Clamp01(initialDayTimeProgress) * fireIntensityMultiplier;
        var fireEmission = fireParticleSystem.emission;
        fireEmission.rateOverTime = fireIntensity;
    }
}
