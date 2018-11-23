using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class DialogProcessor : MonoBehaviour
{
    #region Singleton 
    public static DialogProcessor singleton;
    private void Awake()
    {
        singleton = this;
    }
    #endregion
    [Header("Dialogue Queue")]
    public Queue<DialogList> dialogues = new Queue<DialogList>();

    [Header("TextSpeed")]
    public float textSpeed = 0.20f;

    [Header("Debug")]
    public GameObject parentObj;
    public bool inDialogue = false;
    private bool nextSentenceReady = false;
    private UIObjects uiObjects;


    private void Start()
    {
        uiObjects = UIObjects.singleton;
    }

    private void Update()
    {
        if (Input.GetButtonUp("Submit") && (inDialogue && nextSentenceReady))
        {
            StartCoroutine(DisplayNextSequence());
        }
    }

    public void StartDialog(Dialog dialog, GameObject parentObj)
    {
        this.parentObj = parentObj;
        dialogues.Clear();
        inDialogue = true;
        PlayerController.singleton.canMove = false;

        if (uiObjects.MessageBox.box.transform.position != uiObjects.MessageBox.endPos.position)
        { UIObjects.singleton.Move(uiObjects.MessageBox.box, uiObjects.MessageBox.startPos.position, uiObjects.MessageBox.endPos.position, 1f); }
        else
        { uiObjects.MessageBox.box.transform.position = uiObjects.MessageBox.endPos.position; }

        print("MessageBox = " + uiObjects.MessageBox.box.transform.position + " // TargetPosition = " + uiObjects.MessageBox.endPos.position);

        foreach (DialogList dL in dialog.dialogs)
        {
            dialogues.Enqueue(dL);
        }

        StartCoroutine(DisplayNextSequence());
    }

    private IEnumerator DisplayNextSequence()
    {
        NextDialogReady(false);
        if (dialogues.Count == 0)
        {
            EndDialog();
        }
        else
        {
            DialogList currentDialogue = dialogues.Dequeue();

            if (currentDialogue.isChoice)
            {
               StartCoroutine(StartChoice(currentDialogue));
            }
            else
            {
                SetUIElements(currentDialogue);
                foreach (char letter in currentDialogue.dialog)
                {
                    uiObjects.MessageBox.body.text += letter;
                    yield return new WaitForSeconds(textSpeed);
                }
                NextDialogReady(true);
            }

        }

        yield return null;
    }

    private void NextDialogReady(bool active)
    {
        uiObjects.MessageBox.heart.SetActive(active);
        nextSentenceReady = active;
    }

    private IEnumerator StartChoice(DialogList dialog)
    {
        SetUIElements(dialog);
        uiObjects.MessageBox.choiceBox.SetActive(true);

        foreach (char letter in dialog.dialog)
        {
            uiObjects.MessageBox.body.text += letter;
            yield return new WaitForSeconds(textSpeed);
        }

        ChoiceSelection.singleton.StartChoice(dialog);

    }

    public void SetUIElements(DialogList dialogList)
    {
        uiObjects.MessageBox.talkSprite.sprite = dialogList.sprite;
        uiObjects.MessageBox.name.text = dialogList.actorName;
        uiObjects.MessageBox.body.text = null;

        switch (dialogList.placement)
        {
            case DialogList.Placement.Left:
                uiObjects.SetObjectLocation(uiObjects.MessageBox.talkSprite.transform, uiObjects.MessageBox.talkSpriteLeft);
                break;
            case DialogList.Placement.Center:
                uiObjects.SetObjectLocation(uiObjects.MessageBox.talkSprite.transform, uiObjects.MessageBox.talkSpriteMiddle);
                break;
            case DialogList.Placement.Right:
                uiObjects.SetObjectLocation(uiObjects.MessageBox.talkSprite.transform, uiObjects.MessageBox.talkSpriteRight);
                break;
        }

        #region isThought?
        if (dialogList.isThought)
        { DialogIsThought(true); }
        else
        { DialogIsThought(false); }
        #endregion
        #region ShakeMessageBox?
        if (dialogList.shakeMessageBox)
        { ShakeMessageBox(); }
        #endregion
        #region Emote?
        if (dialogList.emote != Emote.Empty)
        {
            Transform target = this.transform;
            if (dialogList.emoteTarget != null)
            { target = dialogList.emoteTarget; }
            else
            { print("No target for emote, transform = null"); }

            PlayEmote(dialogList.emote, target);
        }
        #endregion
        #region ChangeDirection?
        dialogList.SetDirection();
        #endregion
    }

    private void DialogIsThought(bool active)
    {
        if (active)
        {
            uiObjects.MessageBox.ChangeFont(2);
            StartCoroutine(uiObjects.FadeScreen(0f, 0.75f, 1f));
        }
        else
        {
            uiObjects.MessageBox.ChangeFont(0);
            StartCoroutine(uiObjects.FadeScreen(0f, 0f, 0f));
        }

    }

    private void ShakeMessageBox()
    {
       StartCoroutine(uiObjects.ShakeMessageBox());
    }

    private void PlayEmote(Emote emote, Transform emoteTarget)
    {
        StartCoroutine(uiObjects.PlayEmote(emote, emoteTarget));
    }

    public void EndDialog()
    {
        NextDialogReady(false);
        inDialogue = false;

        uiObjects.MessageBox.box.transform.position = uiObjects.MessageBox.startPos.position;

        PlayerController.singleton.canMove = true;
    }

 

}
