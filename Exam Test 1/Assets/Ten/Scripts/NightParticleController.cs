using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NightParticleController : MonoBehaviour
{
    [Min(0f)]
    [SerializeField]
    private float intensitytransitionSpeed = 3f;

    [SerializeField]
    private bool smoothIntensity;

    private DayTimeController dayTimeController;
    public ParticleSystem particleFire;
    public ParticleSystem ParticlesRain;

    private float initialIntensityRain;
    private float targetIntensityRain;

    private float initialIntensityFire;
    private float targetIntensityFire;
    private void Awake()
    {
        dayTimeController = FindObjectOfType<DayTimeController>();
        initialIntensityRain = ParticlesRain.emission.rateOverTime.constant;
        initialIntensityFire = particleFire.emission.rateOverTime.constant;
    }
    private void Start()
    {
        UpdateTargetIntensity();
        UpdateParticleIntensity();
    }
    private void Update()
    {
        UpdateTargetIntensity();
        if(smoothIntensity)
        {
            UpdateParticleIntensitySmooth();
        }
        else
        {
            UpdateParticleIntensity();
        }
    }
    private void UpdateTargetIntensity()
    {
        targetIntensityRain = dayTimeController.IsDay ? 0 : initialIntensityRain;
        targetIntensityFire = dayTimeController.IsDay ? 0 : initialIntensityFire;
    }
    private void UpdateParticleIntensitySmooth()
    {
        //ParticlesRain.emission.rateOverTime = Mathf.Lerp(ParticlesRain.emission.rateOverTime.constant, targetIntensityRain, intensitytransitionSpeed * Time.deltaTime);
        //particleFire.emission.rateOverTime.constant = Mathf.Lerp(particleFire.emission.rateOverTime.constant, targetIntensityFire, intensitytransitionSpeed * Time.deltaTime);
    }
    private void UpdateParticleIntensity()
    {
        //ParticlesRain.emission.rateOverTime = targetIntensityRain;
        //particleFire.emission.rateOverTime = targetIntensityFire;
    }

}
