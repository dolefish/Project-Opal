using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class ChangeSceneTrigger : MonoBehaviour
{
    public string scene;
    public Vector2 newPos;
    public Direction requiredDirection;
    public AudioClip track;


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            PlayerController pC = other.GetComponent<PlayerController>();

            if (requiredDirection == pC.player_Dir)
            {
                if (track != null)
                {
                    ChangeScene.singleton.musicAudioSource.clip = track;
                    ChangeScene.singleton.musicAudioSource.Play();
                }

                StartCoroutine(ChangeScene.singleton.NextScene(scene, newPos));
            }
           
        }
    }

}
