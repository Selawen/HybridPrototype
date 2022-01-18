using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuardEars : MonoBehaviour
{
    [SerializeField] private GameObject soundSphere;
    [SerializeField] private AudioSource alerted;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {   
        if (other.gameObject == soundSphere)
        {
            alerted.Play();
            Debug.Log("sound heard");
            GameObject.FindObjectOfType<GameManager>().GameOver();
        }
    }
}
