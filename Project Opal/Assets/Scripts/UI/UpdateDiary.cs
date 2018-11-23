using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdateDiary : MonoBehaviour
{
    #region Singleton
    public static UpdateDiary singleton;
    private void Awake()
    {
        singleton = this;
    }
    #endregion

    private Animator thisAnimator;

    private void Start()
    {
        thisAnimator = GetComponent<Animator>();
    }

    public void PlayAnimation()
    {
        thisAnimator.SetTrigger("Play");
        print("PLAY ANIMATION FOR DIARY");
    }

}
