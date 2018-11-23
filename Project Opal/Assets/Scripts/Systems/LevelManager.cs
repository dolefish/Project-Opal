using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{


    public void LoadJennApartment()
    {
        StartCoroutine(UIObjects.singleton.FadeScreen(0f, 1f, 3f));

        StartCoroutine(JennApartment());
    }

    public void LoadMainMenu()
    {
        GameObject.Find("_UI").SetActive(false);

        SceneManager.LoadScene("MainMenu");
        print("BUMP - Loaded Scene");
    }

    IEnumerator JennApartment()
    {
        yield return new WaitForSecondsRealtime(3f);
        ChangeScene.singleton.musicAudioSource.clip = ChangeScene.singleton.rainyHeart;
        ChangeScene.singleton.musicAudioSource.Play();
        SceneManager.LoadScene("JennApartment");
    }

    public void LoadTown()
    {
        ChangeScene.singleton.musicAudioSource.clip = ChangeScene.singleton.explorationUnknown;
        ChangeScene.singleton.musicAudioSource.Play();
        SceneManager.LoadScene("Town");
    }
}
