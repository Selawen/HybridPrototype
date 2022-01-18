using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CaughtButton : MonoBehaviour
{
    [SerializeField] private GameObject panel;

    public void Caught()
    {
        GetComponent<AudioSource>().Play();
        FindObjectOfType<GameManager>().EndGame();
    }

    public void NotCaught()
    {
        panel.SetActive(false);
    }
}
