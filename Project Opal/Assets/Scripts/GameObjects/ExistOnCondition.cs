using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExistOnCondition : MonoBehaviour {

    [Header("Condition")]
    public MissionReference condition;

    [Header("Components")]
    public SpriteRenderer spriteRenderer;
    public BoxCollider2D boxCollider2D;


    private void FixedUpdate()
    {
        if (ConditionMet())
        { Exist(true);}
        else
        { Exist(false); }
            
    }

    private void Exist(bool active)
    {
        if (spriteRenderer != null)
        { spriteRenderer.enabled = active; }
        if (boxCollider2D != null)
        { boxCollider2D.enabled = active; }
    }

    public bool ConditionMet()
    {
        Mission reference = MissionList.singleton.GetMission(condition.missionID);

        #region Mission Checks
        if (condition.requireStarted)
        {
            if (condition.started != reference.started)
            {
                return false;
            }
        }
        if (condition.requireFinished)
        {
            if (condition.finished != reference.finished)
            {
                return false;
            }
        }
        if (condition.requireBranch)
        {
            if (condition.branch != reference.branch)
            {
                return false;
            }
        }
        if (condition.requireStage)
        {
            if (condition.stage != reference.stage)
            {
                return false;
            }
        }
        #endregion

        return true;
    }
}
