using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    public GameObject panel;

    public TextMeshProUGUI timerText;
    [Range(1, 900)] public float timer;
    private float maxTime;

    public TextMeshProUGUI targetText;
    [Range(1, 10)] public int totalTargets;
    private int targetsHit = 0;

    [SerializeField] private AudioSource lastPew;

    // Start is called before the first frame update
    void Start()
    {
        maxTime = timer;
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.timeScale == 0) return;

        timer -= Time.deltaTime; 
        
        int seconds = Mathf.CeilToInt(timer % 60);
        int minutes = Mathf.FloorToInt(timer / 60);
        if (seconds <10) timerText.text = minutes + ":0" + seconds;
        else timerText.text = minutes + ":" + seconds;
        
        if (timer <= 0)
        {
            timerText.text = "0:00";
            GameOver();
            return;
        }


        if (targetsHit == totalTargets)
        {
            targetText.text = "Hit: " + targetsHit + "/" + totalTargets;
            Win();
            return;
        }

        if (Input.GetKeyDown(KeyCode.Minus))
        {
            targetsHit--;
        } else if (Input.GetKeyDown(KeyCode.Equals))
        {
            targetsHit++;
            GetComponent<AudioSource>().Play();
        }

        targetText.text = "Hit: " + targetsHit + "/" + totalTargets;

    }

    public void GameOver()
    {
        EndGame();
        Debug.Log("GameOver");
    } 

    public void Win()
    {
        lastPew.Play();
        EndGame();
        Debug.Log("Win");
    }

    private void EndGame()
    {
        Time.timeScale = 0;
        panel.SetActive(true);
        timer = maxTime;
        targetsHit = 0;
    }
}
