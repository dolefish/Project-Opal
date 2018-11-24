using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissionList : MonoBehaviour
{

    #region Singleton
    public static MissionList singleton;
    private void Awake()
    {
        singleton = this;
    }
    #endregion

    [Header("MissionList")]
    public List<Mission> missionList = new List<Mission>();


    public Mission GetMission(int id)
    {
        for (int i = 0; i < missionList.Count; i++)
        {
            if (id == missionList[i].id)
            {
                return missionList[i];
            }
        }
        return null;
    }

    public void SetMissionInfo(MissionReference missionRef)
    {
        for (int i = 0; i < missionList.Count; i++)
        {
            if (missionRef.missionID == missionList[i].id)
            {
                if (SpecificProgress(missionRef))
                {
                    int missID = i;
                    ProgressMission(missionRef, missID);
                    break;
                }
                else
                {
                    missionList[i].started = missionRef.started;
                    missionList[i].finished = missionRef.finished;
                    missionList[i].branch = missionRef.branch;
                    missionList[i].stage = missionRef.stage;
                    print("MissionUpdate: ID:" + missionList[i].id + " Started:" + missionList[i].started + " Finished:" + missionList[i].finished + " Branch:" + missionList[i].branch + " Stage:" + missionList[i].stage);
                    break;
                }
                
            }
        }      
    }

    private bool SpecificProgress(MissionReference missionRef)
    {
        if (missionRef.requireStarted)
        { return true; }
        else if (missionRef.requireFinished)
        { return true; }
        else if (missionRef.requireBranch)
        { return true; }
        else if (missionRef.requireStage)
        { return true; }

        return false;
    }

    private void ProgressMission(MissionReference missionRef, int missID)
    {
        if (missionRef.requireStarted)
        { missionList[missID].started = missionRef.started; }
        if (missionRef.requireFinished)
        { missionList[missID].finished = missionRef.finished; }
        if (missionRef.requireBranch)
        { missionList[missID].branch = missionRef.branch; }
        if (missionRef.requireStage)
        { missionList[missID].stage = missionRef.stage; }
    }

}
