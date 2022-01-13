using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartButton : MonoBehaviour
{
    [SerializeField] private GameObject panel;
    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 0;
    }

    public void Play()
    {
        panel.SetActive(false);
        Time.timeScale = 1;
    }
}
