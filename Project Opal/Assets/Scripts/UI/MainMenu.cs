using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour {

    public GameObject splashScreen;
    public GameObject cautionWindow;
    public GameObject infoWindow;

    public Text loveText;
    public Text intellectText;
    public Text charismaText;

    public Button loadButton;

    [SerializeField]
    public List<Button> buttons = new List<Button>(); 


    void Start()
    {
        StartCoroutine(SetUp());

    }



    public void CheckCautionWindow()
    {
        if (cautionWindow.activeSelf == true)
        {
            cautionWindow.SetActive(false);

            for (int i = 0; i < buttons.Count; i++)
            {
                if (buttons[i].GetComponent<Button>().interactable == false)
                {
                    buttons[i].GetComponent<Button>().interactable = true;
                }
            }
        }
        else
        {
            cautionWindow.SetActive(true);

            for (int i = 0; i < buttons.Count; i++)
            {
                if (buttons[i].GetComponent<Button>().interactable == true)
                {
                    buttons[i].GetComponent<Button>().interactable = false;
                }
            }
        }


    }

    public void Exit()
    {
        Application.Quit();
    }

    IEnumerator SetUp()
    {
        ChangeScene.singleton.musicAudioSource.clip = ChangeScene.singleton.lovePlus;
        ChangeScene.singleton.musicAudioSource.PlayDelayed(6.25f);
        yield return new WaitForSeconds(23f);
        Destroy(splashScreen);
    }


    public void RemoveCautionWindow()
    {
        for (int i = 0; i < buttons.Count; i++)
        {
            if (buttons[i].GetComponent<Button>().interactable == false)
            {
                buttons[i].GetComponent<Button>().interactable = true;
            }
        }

        cautionWindow.SetActive(false);
    }

    IEnumerator ChangeTheScene()
    {
        AudioSource audioSource = this.gameObject.GetComponent<AudioSource>();

        UIObjects.singleton.gameObject.SetActive(true);

        StartCoroutine(UIObjects.singleton.FadeScreen(0f, 1f, 3f));

        while (audioSource.volume > 0.1)
        {
            audioSource.volume -= Time.deltaTime * 3;
        }

        yield return new WaitForSecondsRealtime(3f);

        SceneManager.LoadScene("JennApartment");
    }

}
