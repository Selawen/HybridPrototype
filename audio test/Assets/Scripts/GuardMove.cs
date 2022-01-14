using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuardMove : MonoBehaviour
{
    //public Vector3 patrolPoint0, patrolPoint1;
    public List<Vector3> patrolPoints;
    [SerializeField] private Vector3 targetPoint;
    [SerializeField] public int patrolPointCount;
    [SerializeField] private int patrolTarget = 0;

    [SerializeField] private bool waiting;

    private AudioSource footstep;

    // Start is called before the first frame update
    void Start()
    {
        patrolPointCount = patrolPoints.Count;
        //footstep.Play();
        //ToDo: implement removing doubles
        /*
        for (int i = 1; i<=patrolPointCount; i++)
        {
            if (patrolPoints[i-1] == patrolPoints[i]) { }
        }
        */

        targetPoint = patrolPoints[0];

        footstep = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (transform.position == targetPoint)
        {
            waiting = true;
            if(patrolTarget < patrolPointCount)
            {
                
                targetPoint = patrolPoints[patrolTarget];
                patrolTarget++;
            } else if (patrolTarget >= patrolPointCount)
            {
                patrolTarget = 0;
                targetPoint = patrolPoints[patrolTarget];
            }
            StartCoroutine(LookAround());
        } 
        if (!waiting)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPoint, Time.deltaTime);
            if (!footstep.isPlaying)
            {
                footstep.mute = !footstep.mute;
                footstep.Play();
            }
        }
    }

    IEnumerator LookAround()
    {
        yield return new WaitForSeconds(1);
        waiting = false;
    }
}
