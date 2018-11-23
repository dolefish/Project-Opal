using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Diary : MonoBehaviour {

    public GameObject diary;
    public GameObject goals;

    public Animator diaryAnimator;

    public AudioClip open;
    public AudioClip nextPage;

    private int displayID;

    private bool diaryActive = false;

    void Update ()
    {
        if ((Input.GetButtonDown("Select") && diaryActive == false) && diary.activeSelf == false)
        { 
        }
        else if (Input.GetButtonDown("Select") && diaryActive == true)
        {
        }
	}



}
