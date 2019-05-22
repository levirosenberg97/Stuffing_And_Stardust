using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorBoardCreak : MonoBehaviour {

    public AudioSource FloorCreaks;
   
    // Use this for initialization
    void Start()
    {
        StartCoroutine(Timer());
    }

    // Update is called once per frame
   IEnumerator Timer()
    {
        int Timing = Random.Range(5, 15);
        yield return new WaitForSeconds(Timing);
        FloorCreaks.Play();
        Debug.Log ("yes");
    }

    void TimerReset()
    {

    }
}
