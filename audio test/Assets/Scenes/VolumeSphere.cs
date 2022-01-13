using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VolumeSphere : MonoBehaviour
{
    [SerializeField] private AudioSource micAudio;
    private bool running;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
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
        transform.localScale *= (averageSample * 1000);
        this.GetComponent<SphereCollider>().radius = transform.localScale.x / 2;

        running = false;
    } 
}
