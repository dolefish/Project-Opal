using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ChoiceSelection : MonoBehaviour
{
    public static ChoiceSelection singleton;

    public bool makingChoice = false;

    public bool stopInput = false;

    public DialogList choices;

    private int currentChoice = 0;

    public Dialog setDialog;

    private void Awake()
    {
        singleton = this;
    }

    private void Start()
    {
        gameObject.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetAxis("Vertical") > 0 && !stopInput)
        {
            stopInput = true;
            currentChoice++;
            if (currentChoice > choices.answers.Length - 1)
            {
                currentChoice = 0;
                StartCoroutine(ChangeChoice(choices.answers[currentChoice].choice));
            }
            else
            {
                StartCoroutine(ChangeChoice(choices.answers[currentChoice].choice));
            }
        }
        else if (Input.GetAxis("Vertical") < 0 && !stopInput)
        {
            stopInput = true;
            currentChoice--;
            if (currentChoice < 0)
            {
                currentChoice = choices.answers.Length - 1;
                StartCoroutine(ChangeChoice(choices.answers[currentChoice].choice));
            }
            else
            {
                StartCoroutine(ChangeChoice(choices.answers[currentChoice].choice));
            }
        }

        if (Input.GetButtonDown("Submit") && stopInput == false)
        {
            MakeChoice();
        }
    }

    public void StartChoice(DialogList choices)
    {
        StartCoroutine(StartChoiceCo(choices));
    }

    private IEnumerator StartChoiceCo(DialogList choices)
    {
        makingChoice = true;
        currentChoice = 0;

        UIObjects.singleton.Move(UIObjects.singleton.MessageBox.choiceBox, UIObjects.singleton.MessageBox.choiceStartPos.position, UIObjects.singleton.MessageBox.choiceEndPos.position, 4f);
        this.choices = choices;
        string text = choices.answers[0].choice;
        UIObjects.singleton.MessageBox.choiceText.text = text;
        choices.answers[currentChoice].SetSkillImage();

        yield return null;
    }

    private IEnumerator CloseChoice()
    {
        makingChoice = false;
        currentChoice = 0;

        UIObjects.singleton.Move(UIObjects.singleton.MessageBox.choiceBox, UIObjects.singleton.MessageBox.choiceEndPos.position, UIObjects.singleton.MessageBox.choiceStartPos.position, 4f);
        choices = null;
        string text = null;
        UIObjects.singleton.MessageBox.choiceText.text = text;

        gameObject.SetActive(false);
        yield return null;
    }

    public IEnumerator ChangeChoice(string choice)
    {
        UIObjects.singleton.MessageBox.choiceText.text = null;
        string text = choices.answers[currentChoice].choice;
        choices.answers[currentChoice].SetSkillImage();
        foreach (char letter in text.ToCharArray())
        {
            UIObjects.singleton.MessageBox.choiceText.text += letter;
            UIObjects.singleton.MessageBox.audioSource.Play();
            yield return null;
        }

        yield return new WaitForSecondsRealtime(0.50f);

        stopInput = false;
    }

    public void MakeChoice()
    {
        makingChoice = false;

        if (choices.answers[currentChoice].progressMission)
        {
            MissionList.singleton.SetMissionInfo(choices.answers[currentChoice].missionProgression);
        }

        DialogProcessor.singleton.EndDialog();
 
        StartCoroutine(CloseChoice());

    }
}
