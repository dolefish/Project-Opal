using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class PauseGame : MonoBehaviour {

    public GameObject pause;
    private AudioSource audioSource;

    public AudioClip openPause;
    public AudioClip closePause;

    public GameObject cautionWindow;
    public GameObject settingWindow;
    public GameObject mapWindow;

    private GameObject diaryMenu;


    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        diaryMenu = GameObject.Find("DiaryMenu");
    }

    void Update ()
    {                                           
        if (Input.GetButtonDown("Start") && pause.activeSelf == false)
        {
            //Play Sound
            audioSource.clip = openPause;
            audioSource.Play();

            //Pause Time
            PausetheGame();
            pause.SetActive(true);
        }
        else if (Input.GetButtonDown("Start") && pause.activeSelf == true)
        {
            //TODO: How does timescale change effect skill usage scripts? 
            ResumeGame();
        }
    }


    public void ExitGame()
    {
        if (cautionWindow.activeSelf == true)
        {
                print("Exit Application");
                Application.Quit();
        }
        else
        {
            cautionWindow.SetActive(true);
        }   
    }

    public void SettingsWindow()
    {
        if (settingWindow.activeSelf == false)
        {
            settingWindow.SetActive(true);
            if (settingWindow.activeSelf == true && mapWindow.activeSelf == true)
            {
                mapWindow.SetActive(false);
            }
        }
        else
        {
            settingWindow.SetActive(false);
        }

       
    }

    public void Map()
    {
        if (mapWindow.activeSelf == false)
        {
            mapWindow.SetActive(true);
            if (settingWindow.activeSelf == true && mapWindow.activeSelf == true)
            {
                settingWindow.SetActive(false);
            }
        }
        else
        {
            mapWindow.SetActive(false);
        }
    }

    public void PausetheGame()
    {
        Time.timeScale = 0f;
       // player.GetComponent<PlayerGridController>().enabled = !enabled;
        diaryMenu.GetComponent<Diary>().enabled = !enabled;
    }

    public void SaveGame()
    {
        XMLManager.singleton.SaveMissions();

        print("Saved Game");
    }

    public void LoadGame()
    {
        XMLManager.singleton.LoadMissions();
        print("Loaded Game");
    }

    public void ResumeGame()
    {
        //Play Sound
        audioSource.clip = closePause;
        audioSource.Play();

        //Resume time / Disable pause screen
        Time.timeScale = 1;
       // player.GetComponent<PlayerGridController>().enabled = enabled;
        diaryMenu.GetComponent<Diary>().enabled = enabled;
        settingWindow.SetActive(false);
        mapWindow.SetActive(false);
        pause.SetActive(false);
    }

    public void CloseCautionWindow()
    {
        cautionWindow.SetActive(false);
    }
}
