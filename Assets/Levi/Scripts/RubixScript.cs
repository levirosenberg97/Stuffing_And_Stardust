using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RubixScript : MonoBehaviour
{
    public ParticleSystem chargingBeam;
    public ParticleSystem laser;

    private void Start()
    {
        chargingBeam.Stop();
    }

    public void StartCharging()
    {
        chargingBeam.Play();
    }
    public void StopCharging()
    {
        chargingBeam.Stop();
    }

    public void FireLaser()
    {
        laser.Play();
    }

    public void StopLaser()
    {
        laser.Stop();
    }
    
}