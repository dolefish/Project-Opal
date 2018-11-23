using UnityEngine;

[System.Serializable]
public class Choice
{
    [Header("SetUp")]
    public string choice;
    public ChoiceSkill choiceSkill;

    [Header("Output Mission")]
    public MissionReference missionProgression;
    public bool progressMission;


    public void SetSkillImage()
    {
        switch (choiceSkill)
        {
            case ChoiceSkill.Intelligence:
                UIObjects.singleton.MessageBox.skillSprite.sprite = PlayerStats.singleton.intellect.statIcon;
            break;
            case ChoiceSkill.Charisma:
                UIObjects.singleton.MessageBox.skillSprite.sprite = PlayerStats.singleton.charisma.statIcon;
                break;
            case ChoiceSkill.Love:
                UIObjects.singleton.MessageBox.skillSprite.sprite = PlayerStats.singleton.love.statIcon;
                break;
            case ChoiceSkill.Generic:
                UIObjects.singleton.MessageBox.skillSprite.sprite = null;
                break;
        }

    }

}

public enum ChoiceSkill
{
    Intelligence,
    Charisma,
    Love,
    Generic
}