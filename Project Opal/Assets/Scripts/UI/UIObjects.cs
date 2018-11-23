using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;
using System;

public class UIObjects : MonoBehaviour {

    public static UIObjects singleton;

    public float moveSpeed;

    public Image fade;
    public GameObject emotePrefab;

    bool moving;

    public ActionBoxObjects actionBoxObjects = new ActionBoxObjects();

    public MoneyObjects moneyObjects = new MoneyObjects();
    public MessageBoxStuff MessageBox = new MessageBoxStuff();
    public TimeObjects timeObjects = new TimeObjects();

    void Awake()
    {
        singleton = this;
    }

    public void SetObjectLocation(Transform objTransform, Transform newPosition)
    {
        objTransform.position = newPosition.position;
    }

    public void Move(GameObject obj, Vector3 startPos, Vector3 endPos, float speed)
    {
        StopCoroutine("MoveCo");
        StartCoroutine(MoveCo(obj, startPos, endPos, speed));
    }

    public IEnumerator FadeScreen(float startAlpha, float endAlpha, float speed)
    {
        Color fadeAlpha = new Color();
        fadeAlpha.a = startAlpha;
        fadeAlpha = fade.color;

        while (Mathf.Abs(fade.color.a - endAlpha) > 0.05)
        {
            if (speed == 0)
            { break;}
            if (startAlpha > endAlpha)
            {
                fadeAlpha.a -= Time.deltaTime / speed;
                fade.color = fadeAlpha;
            }
            else
            {
                fadeAlpha.a += Time.deltaTime / speed;
                fade.color = fadeAlpha;
            }

            yield return null;
        }
        fadeAlpha.a = endAlpha;
        fade.color = fadeAlpha;

        yield return null;
    }

    public IEnumerator MoveCo(GameObject obj, Vector3 startPos, Vector3 endPos)
    {
        obj.transform.position = startPos;

        while (Vector3.Distance(obj.transform.position, endPos) > 0.05)
        {
            obj.transform.position = Vector3.MoveTowards(obj.transform.position, endPos, moveSpeed * Time.deltaTime * 250);
            yield return null;
        }

        obj.transform.position = endPos;
    }

    public IEnumerator MoveCo(GameObject obj, Vector3 startPos, Vector3 endPos, float speed)
    {
        obj.transform.position = startPos;

        while (Vector3.Distance(obj.transform.position, endPos) > 0.05)
        { 
            obj.transform.position = Vector3.MoveTowards(obj.transform.position, endPos, moveSpeed * Time.deltaTime * 125 * speed);
            yield return null;
        }

        obj.transform.position = endPos;
    }

    public IEnumerator ShakeMessageBox()
    {
        Vector3 startPos = MessageBox.box.transform.position;
        Vector3 posOne = new Vector3(MessageBox.box.transform.position.x - 25f, MessageBox.box.transform.position.y, MessageBox.box.transform.position.z);
        Vector3 posTwo = new Vector3(MessageBox.box.transform.position.x + 25f, MessageBox.box.transform.position.y, MessageBox.box.transform.position.z);
        yield return new WaitForSecondsRealtime(0.25f);

        int cycles = 16;
        while (cycles > 0)
        {
            if (MessageBox.box.transform.position == posOne)
            { MessageBox.box.transform.position = posTwo; }
            else
            { MessageBox.box.transform.position = posOne; }

            cycles--;
            yield return new WaitForSecondsRealtime(0.05f);
        }

        MessageBox.box.transform.position = startPos;
        yield return null;
    }

    public IEnumerator PlayEmote(Emote emote, Transform target)
    {
        Vector2 newTargetPos = new Vector3(target.position.x, target.position.y + 1);

        GameObject emoteBubble = Instantiate(emotePrefab);
        emoteBubble.transform.position = newTargetPos;

        switch (emote)
        {
            case Emote.Angry:
                emoteBubble.GetComponent<Animator>().SetTrigger("isAngry");
                break;
            case Emote.Awkward:
                emoteBubble.GetComponent<Animator>().SetTrigger("isAwkward");
                break;
            case Emote.Happy:
                emoteBubble.GetComponent<Animator>().SetTrigger("isHappy");
                break;
            case Emote.Sad:
                emoteBubble.GetComponent<Animator>().SetTrigger("isSad");
                break;
            case Emote.Suprise:
                emoteBubble.GetComponent<Animator>().SetTrigger("isSuprised");
                break;
            case Emote.Thinking:
                emoteBubble.GetComponent<Animator>().SetTrigger("isThinking");
                break;
        }

        yield return new WaitForSeconds(3f);

        Destroy(emoteBubble);

        yield return null;
    }


/// <summary>
/// ////////////////////// UIClasses Below ////////////////////////////////////////////////
/// </summary>

    [Serializable]
    public class ActionBoxObjects
    {
        public GameObject actionBox;
        public TextMeshProUGUI actionText;

        public void ShowActionBox(bool active)
        {
            actionBox.SetActive(active);
            actionText.text = null;     
        }
        public void ShowActionBox(bool active, string interactionMessage)
        {
            actionBox.SetActive(active);
            actionText.text = interactionMessage;
        }
    }

    [Serializable]
    public class DiaryObjects
    {
        public GameObject diary;
        public TextMeshProUGUI title;
        public TextMeshProUGUI body;
        public List<TextMeshProUGUI> goals = new List<TextMeshProUGUI>();

        public void UpdateDiary(Mission mission)
        {
            title.text = mission.title;
            body.text = mission.body;
            for (int i = 0; i < mission.goals.Length; i++)
            {
                goals[i].gameObject.SetActive(true);
                goals[i].text = mission.goals[i];
            }
        }

        public void ClearDiary()
        {
            title.text = "";
            body.text = "";

            for (int i = 0; i < goals.Count; i++)
            {
                goals[i].text = "";
                goals[i].gameObject.SetActive(false);
            }
        }
    }

    [Serializable]
    public class MoneyObjects
    {
        public GameObject moneyPanel;
        public TextMeshProUGUI money_text;

        public void UpdateMoneyPanel()
        {
            float cash = ValuesScript.singleton.cash;
            money_text.text = cash.ToString("0.00");

            string temp = money_text.text;

            int changeSize = 75;

            foreach (char letter in temp.ToCharArray())
            {
                changeSize += 12;
            }

            RectTransform rt = moneyPanel.GetComponent(typeof(RectTransform)) as RectTransform;
            rt.sizeDelta = new Vector2(changeSize, 60);

            moneyPanel.transform.position = new Vector2(60 + (changeSize / 4), moneyPanel.transform.position.y);
        }
    }

    [Serializable]
    public class TimeObjects
    {
        public GameObject sun;
        public TextMeshProUGUI timeText;
        public TextMeshProUGUI dayText;
        public Animator sunMoon;

        public void UpdateTime(GameTime gameTime)
        {
            #region Updating Time Text
            //This will make the clock show a 12 and not a 0 at midnight.
            if (gameTime.hours == 0)   
            { timeText.text = "12:" + gameTime.minutes.ToString("00"); }
            else
            { timeText.text = gameTime.hours.ToString("00") + ":" + gameTime.minutes.ToString("00"); }

            //Is it AM or PM?
            if (gameTime.isAM)      
            {
                timeText.text += "AM";
            }
            else
            {
                timeText.text += "PM";
            }
            #endregion
            #region Updating Day Text
            dayText.text = gameTime.day.ToString();
            #endregion
        }

        public void UpdateSunMoon(bool day)
        {
            sunMoon.SetBool("isDay", day); 
        }

    }

    [Serializable]
    public class SkillBarObjects {
        public GameObject box;
        public Transform startPos;
        public Transform endPos;
    }

    [Serializable]
    public class MessageBoxStuff
    {
        public GameObject box;
        public GameObject choiceBox;
        public GameObject heart;
        public Transform startPos;
        public Transform endPos;
        public Transform choiceStartPos;
        public Transform choiceEndPos;
        public Transform talkSpriteLeft;
        public Transform talkSpriteMiddle;
        public Transform talkSpriteRight;
        public Image talkSprite;
        public Image skillSprite;
        public TextMeshProUGUI name;
        public TextMeshProUGUI body;
        public TextMeshProUGUI choiceText;
        public AudioSource audioSource;
        public Animator animator;

        public void ChangeFont(int style)
        {
            switch (style)
            {
                case 0:
                    UIObjects.singleton.MessageBox.body.fontStyle = FontStyles.Normal;
                    break;
                case 1:
                    UIObjects.singleton.MessageBox.body.fontStyle = FontStyles.Bold;
                    break;
                case 2:
                    UIObjects.singleton.MessageBox.body.fontStyle = FontStyles.Italic;
                    break;
            }
        }
    }

}
