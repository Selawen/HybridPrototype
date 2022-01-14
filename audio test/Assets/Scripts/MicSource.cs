using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MicSource : MonoBehaviour
{
    static private int micCount;
    //[SerializeField] int micNumber;

    [SerializeField] private string micName;
    private int micFreq;

    // Start is called before the first frame update
    void Start()
    {
        micCount = Microphone.devices.Length - 1;
        //micNumber = Mathf.Clamp(micNumber, 0, micCount);

        var audio = GetComponent<AudioSource>();
        if (micName == "" || micFreq == 0)
        {
            int minFreq = 0;
            int maxFreq = 0;
            Microphone.GetDeviceCaps(Microphone.devices[0], out minFreq, out maxFreq);
            audio.clip = Microphone.Start(Microphone.devices[0], true, 1, minFreq);
        }
        else audio.clip = Microphone.Start(micName, true, 1, micFreq);
        
        audio.loop = true;

        //int loopcount = 0;
        //while (!(Microphone.GetPosition(null) > 0 && loopcount <10)) { loopcount++; }
        audio.Play();

        /*
        foreach (var device in Microphone.devices)
        {
            Debug.Log("Name: " + device);
            int minFreq;
            int maxFreq;
            Microphone.GetDeviceCaps(device, out minFreq, out maxFreq);
            Debug.Log(minFreq + ", " + maxFreq);
        }
        */
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetHeadset(string name, int freq)
    {
        micName = name;
        micFreq = freq;

        var audio = GetComponent<AudioSource>();
        if (micName == "" || micFreq == 0) audio.clip = Microphone.Start("Microphone (Elite Atlas Aero)", true, 1, 48000);
        else audio.clip = Microphone.Start(micName, true, 1, micFreq);

        Debug.Log("Changed microphone to: " + name);
    }
}
