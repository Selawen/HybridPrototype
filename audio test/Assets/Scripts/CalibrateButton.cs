using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CalibrateButton : MonoBehaviour
{
    [SerializeField] VolumeSphere sphere;
    private bool calibrating = false;

    [SerializeField] private float calibrationTime = 10;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Calibrate()
    {        
        if (!calibrating)
        {
            calibrating = true;
            sphere.maxVolume = 0;
            for (float timer = calibrationTime; timer>0; timer -= Time.unscaledDeltaTime)
            { 
                sphere.Calibration();
            }
            Debug.Log(sphere.maxVolume);
            calibrating = false;
        } else
        {
            calibrating = false;
        }
    }
}
