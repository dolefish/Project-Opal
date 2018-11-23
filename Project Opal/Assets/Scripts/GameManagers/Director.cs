using System.Collections;
using System.Collections.Generic;
using UnityEngine.Playables;
using UnityEngine;

public class Director : MonoBehaviour {

    #region Singleton
    public static Director singleton;
    private void Awake()
    {
        singleton = this;
    }
    #endregion

    public PlayableDirector director;

    public void Play(PlayableAsset playable)
    {
        StartCoroutine(PlayScene(playable, 0));
    }

    public void Play(PlayableAsset playable, float delay)
    {
        StartCoroutine(PlayScene(playable, delay));
    }

    private IEnumerator PlayScene(PlayableAsset playable, float delay)
    {
 
        director.playableAsset = playable;

        if (delay > 0)
        { yield return new WaitForSecondsRealtime(delay); }
                
        yield return null;
    }

}
