using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

[RequireComponent(typeof(TMP_Dropdown))]
public class HeadsetPicker : MonoBehaviour
{
    [SerializeField] private MicSource micSource;

    private List<string> availableMics;
    private TMP_Dropdown dropdown;

    // Start is called before the first frame update
    void Start()
    {
        availableMics = new List<string>();
        dropdown = GetComponent<TMP_Dropdown>();

        foreach (string mic in Microphone.devices)
        {
            availableMics.Add(mic);
        }

        dropdown.ClearOptions();
        dropdown.AddOptions(availableMics);
        //Add listener for when the value of the Dropdown changes, to take action
        dropdown.onValueChanged.AddListener(delegate {
            DropdownValueChanged(dropdown);
        });
    }

    public void DropdownValueChanged(TMP_Dropdown change)
    {
        //Debug.Log(dropdown.options[change.value].text);
        int minFreq = 0;
        int maxFreq = 0;
        Microphone.GetDeviceCaps(dropdown.options[change.value].text, out minFreq, out maxFreq);
        micSource.SetHeadset(dropdown.options[change.value].text, minFreq);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
