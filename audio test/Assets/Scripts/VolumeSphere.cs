using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VolumeSphere : MonoBehaviour
{
    [SerializeField] private AudioSource micAudio;
    private bool running;

    float scaleModifier;
    public float maxVolume = 0.01f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(maxVolume);
        if (!running)
        {
            running = true;
            StartCoroutine(PollVolume());
        }
    }

   IEnumerator PollVolume()
    {
        yield return new WaitForSeconds(0.05f);
        transform.localScale = new Vector3(1, 1, 1);


        float[] samples = new float[micAudio.clip.samples * micAudio.clip.channels];
        micAudio.clip.GetData(samples, 0);


        float averageSample = 0;
        int inputCount = 0;

        /*foreach (float s in samples)
        {
            if (s > 0.000001f && s < 0.005f)
            {
                averageSample += s;
                inputCount++;
            }
        }
        */
        for (int i =0; i<samples.Length/10; i++)
        {
            if (samples[i] > 0.000001f)
            {
                averageSample += samples[i];
                inputCount++;
            }
        }
        averageSample /= (inputCount);

        averageSample = Mathf.Clamp(averageSample, 0, 0.05f);
        //Debug.Log(averageSample);
        transform.localScale *= (averageSample * 100 / maxVolume);
        //this.GetComponent<SphereCollider>().radius = transform.localScale.x / 4;

        running = false;
    } 

    public void Calibration()
    {
        float[] samples = new float[micAudio.clip.samples * micAudio.clip.channels];
        micAudio.clip.GetData(samples, 0);


        float peakSample =0;

        for (int i = 0; i < samples.Length / 10; i++)
        {
            if (samples[i] > peakSample)
            {
                peakSample = samples[i];
            }
        }

        if (maxVolume < peakSample)
        {
            maxVolume = peakSample;
        }
        Debug.Log(peakSample);
    }
}
