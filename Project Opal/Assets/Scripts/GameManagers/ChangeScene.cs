using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour {

    public static ChangeScene singleton;

    public AudioSource openAudioSource;
    public AudioSource musicAudioSource;

    [Header("AudioClips")]
    public AudioClip lovePlus;
    public AudioClip rainyHeart;
    public AudioClip explorationUnknown;

    [HideInInspector] public Vector2 newPos;

    private void Awake()
    {
        singleton = this;
    }

    public IEnumerator NextScene(string scene, Vector2 newPos)
    {
        PlayerController.singleton.canMove = false;
        yield return UIObjects.singleton.FadeScreen(0f, 1f, 0.25f);
        openAudioSource.Play();

        SceneManager.LoadScene(scene);

        PlayerController.singleton.transform.position = newPos;
        RestoreScreen();
        PlayerController.singleton.canMove = true;
    }

    private void RestoreScreen()
    {
        StartCoroutine(UIObjects.singleton.FadeScreen(1f, 0f, 1f));

        if (PlayerGridController.singleton != null)
        { PlayerGridController.singleton.EnablePlayerMovement(true); }     
    }

}
