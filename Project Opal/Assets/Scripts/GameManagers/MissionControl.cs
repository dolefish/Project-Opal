using System.Collections;
using System.Collections.Generic;
using UnityEngine.Playables;
using UnityEngine;

public class MissionControl : MonoBehaviour
{
    [Header("Mission")]
    public MissionReference missionCondition;
    public MissionReference missionProgression;
    public bool progressMission = false;

    [Header("Output")]
    [SerializeField]protected bool loadDialog;
    [SerializeField]protected bool instantiateObj;
    [SerializeField]protected bool changeScene;
    [SerializeField]protected bool playCutScene;

   [Header("Variables")]
    public Dialog dialog;
    public GameObject gameObj;
    public string sceneName;
    public PlayableAsset cutScene;

    [Header("Debug")]
    public GameObject parentObj;
    public bool ignoreInteraction;
    public string missionName;
    public bool missionReady;
    private Interactable interactable;
    public bool active;

    private void Start()
    {
        if (gameObject.GetComponentInParent<Interactable>())
        {
            interactable = gameObject.GetComponentInParent<Interactable>();
        }
        else
        {
            print(transform.parent.name + " has no interactable object!");
        }

        parentObj = transform.parent.gameObject;
        Mission mission = MissionList.singleton.GetMission(missionCondition.missionID);
        missionName = mission.title;
    }

    private void FixedUpdate()
    {
        if ((interactable.interacting && MissionRequirementsMet()) && !active)
        { StartCoroutine(TakeAction()); }
        else if ((ignoreInteraction && MissionRequirementsMet()) && !active)
        { StartCoroutine(TakeAction()); }


        missionReady = MissionRequirementsMet();
    }

    private IEnumerator TakeAction()
    {
        active = true;
        #region Dialogue
        if (loadDialog && !DialogProcessor.singleton.inDialogue)
        {
            DialogProcessor.singleton.StartDialog(dialog, parentObj);
            print("Name:" + transform.name + "\nStarted:" + missionCondition.started + "\nFinished:" + missionCondition.finished + "\nBranch:" + missionCondition.branch + "\nStage:" + missionCondition.stage);
        }
        while (DialogProcessor.singleton.inDialogue)
        {
            yield return null;
        }
        #endregion
        #region CutScene
        if (cutScene != null)
        { Director.singleton.Play(cutScene); }
        #endregion

        if (progressMission)
        { ProgressMission(missionProgression); }

        if (!ignoreInteraction)
        { interactable.OnInteractStop(); }

        yield return new WaitForSecondsRealtime(1f);
        active = false;
    }

    public void ProgressMission(MissionReference missionRef)
    {
        MissionList.singleton.SetMissionInfo(missionRef);  
        UpdateDiary.singleton.PlayAnimation();
    }


    public bool MissionRequirementsMet()
    {
        Mission reference = MissionList.singleton.GetMission(missionCondition.missionID);

        #region Mission Checks
        if (missionCondition.requireStarted)
        {
            if (missionCondition.started != reference.started)
            {
                return false;
            }
        }
        if (missionCondition.requireFinished)
        {
            if (missionCondition.finished != reference.finished)
            {
                return false;
            }
        }
        if (missionCondition.requireBranch)
        {
            if (missionCondition.branch != reference.branch)
            {
                return false;
            }
        }
        if (missionCondition.requireStage)
        {
            if (missionCondition.stage != reference.stage)
            {
                return false;
            }
        }
        #endregion

        return true;
    }

}
