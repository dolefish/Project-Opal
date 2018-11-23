using System.Collections;
using UnityEngine;
using System;
using TMPro;

public class Console : MonoBehaviour {

    public GameObject console;

    public TextMeshProUGUI inputText;
    public TextMeshProUGUI resultText;

    private bool inProcess;

    void Update ()
    {
        if ((Input.GetKeyDown(KeyCode.Return) && !inProcess) && console.activeSelf == true)
        {
            ProcessRequest();
        }

        if (Input.GetKeyDown(KeyCode.Home))
        {
            if (console.activeSelf == true)
            { console.SetActive(false); PlayerController.singleton.canMove = true; }
            else
            { console.SetActive(true); PlayerController.singleton.canMove = false; }
        }
	}

    void ProcessRequest()
    {
        inProcess = true;

        string input = inputText.text;

        if (input.Contains("/help"))
        {
            PrintHelp();
        }
        else if (input.Contains("/clear"))
        {
            ClearResult();
        }
        else if (input.Contains("/mission"))
        {
        //   StartCoroutine(SetMission());
        }
        else if (input.Contains("/teleport"))
        {
            StartCoroutine(SetTeleport());
        }
        else if (input.Contains("/timescale"))
        {
            StartCoroutine(SetTimeScale());
        }
        else
        { Error(); }

        inputText.text = null;
    }

    void PrintHelp()
    {
        resultText.text += "\n /help : /item : /mission : /time : /timescale : /teleport : /info : /save : /load : /clear";
        inProcess = false;
    }

    public void ClearResult()
    {
        resultText.text = null;
        inputText.text = null; 
        inProcess = false;
    }

    void Error()
    {
        resultText.text += "\n Error: Invalid input. Use /help for list of commands.";
        inProcess = false;
    }

    IEnumerator SetTimeScale()
    {
        resultText.text = "/n Input TimeScale (numers only) \n 0 = Fronzen : 1 = Normal Time";

        yield return StartCoroutine(WaitForKeyDown(KeyCode.Return));

        string temp = inputText.text;
        Time.timeScale = Int32.Parse(temp);

        inProcess = false;
    }

    IEnumerator SetTeleport()
    {
        resultText.text = "\n Location Name =";

        yield return StartCoroutine(WaitForKeyDown(KeyCode.Return));

        string tempString = inputText.text;

        Vector2 tmp = new Vector2(0, 0);
        StartCoroutine(ChangeScene.singleton.NextScene(tempString, tmp)); 

        inProcess = false;

        print("SceneLoad");
        yield return null;
    }

    IEnumerator WaitForKeyDown(KeyCode keyCode)
    {
        yield return new WaitForSecondsRealtime(0.5f);

        while (!Input.GetKeyDown(keyCode))
        { yield return null; }

        yield return null;
    }
}
