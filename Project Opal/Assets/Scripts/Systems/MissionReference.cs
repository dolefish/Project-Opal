using UnityEngine;

[System.Serializable]
public class MissionReference
{
    public int missionID;

    public bool started;
    public bool finished;
    public int branch;
    public int stage;
    public GameTime timeStamp;

    [Header("Requirements")]
    public bool requireStarted;
    public bool requireFinished;
    public bool requireBranch;
    public bool requireStage;
    public bool requireTimeStamp;
}
