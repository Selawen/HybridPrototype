using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VolumeSphere : MonoBehaviour
{
    [SerializeField] private AudioSource micAudio;
    private bool running;

    float averageSample;
    [SerializeField] float scaleModifier;
    public float maxVolume = 0.01f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(maxVolume);
        if (!running)
        {
            running = true;
            StartCoroutine(PollVolume());
        }
    }

   IEnumerator PollVolume()
    {
        yield return new WaitForSeconds(0.005f);
        transform.localScale = new Vector3(1, 1, 1);


        float[] samples = new float[micAudio.clip.samples * micAudio.clip.channels];
        micAudio.clip.GetData(samples, 0);


        averageSample = 0;
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
        for (int i =0; i<samples.Length/50; i++)
        {
            if (samples[i] > 0.0005f)
            {
                averageSample += samples[i];
                inputCount++;
            }
        }
        if (inputCount != 0) averageSample /= (inputCount);

        averageSample = Mathf.Clamp(averageSample, 0, 0.05f);
        //Debug.Log(averageSample);

        if (averageSample == 0) averageSample = 0.00001f;
        float scaleMultiplier = averageSample * scaleModifier / maxVolume;
        scaleMultiplier = Mathf.Clamp(scaleMultiplier, 0, 7f);
        //Debug.Log(scaleMultiplier);
        transform.localScale *= scaleMultiplier;
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
        //Debug.Log(peakSample);
    }
}
