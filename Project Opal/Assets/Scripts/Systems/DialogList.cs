using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class DialogList
{
    [Header("Dialog")]
    public Sprite sprite;
    public Placement placement;
    public enum Placement
    { Left, Center, Right }
    public string actorName;
    [TextArea(3, 10)]
    public string dialog;

    [Header("Choice")]
    public bool isChoice;
    public Choice[]answers;

    [Header("Extras")]
    public bool isThought;
    public bool shakeMessageBox;
    public Emote emote;
    public Transform emoteTarget;
    public ChangeDirection changeDirection;

    public DialogList(Sprite sprite, string actorName, string dialog, bool isThought, Emote emote, Transform emoteTarget)
    {
        this.sprite = sprite;
        this.actorName = actorName;
        this.dialog = dialog;
        this.isThought = isThought;
        this.emote = emote;
        this.emoteTarget = emoteTarget;
    }

    public DialogList(Sprite sprite, string actorName, string dialog, bool isChoice, string question, bool isThought, Emote emote, Transform emoteTarget, params Choice[] answers)
    {
        this.sprite = sprite;
        this.actorName = actorName;
        this.dialog = dialog;
        this.isChoice = isChoice;
        this.answers = answers;
        this.isThought = isThought;
        this.emote = emote;
        this.emoteTarget = emoteTarget;
    }

    public void SetDirection()
    {
        Animator animator = DialogProcessor.singleton.parentObj.GetComponent<Animator>();

        switch (changeDirection)
        {
            case ChangeDirection.NoChange:
                break;
            case ChangeDirection.FaceUp:
                ResetAnimatorBools(animator);
                animator.SetBool("faceUp", true);
                break;
            case ChangeDirection.FaceRight:
                ResetAnimatorBools(animator);
                animator.SetBool("faceRight", true);
                break;
            case ChangeDirection.FaceDown:
                ResetAnimatorBools(animator);
                animator.SetBool("faceDown", true);
                break;
            case ChangeDirection.FaceLeft:
                ResetAnimatorBools(animator);
                animator.SetBool("faceLeft", true);
                break;
            case ChangeDirection.FacePlayer:
                ResetAnimatorBools(animator);
                SetDirectionToPlayer();
                break;
                
        }
    }

    private void SetDirectionToPlayer()
    {
        Direction dir = PlayerController.singleton.player_Dir;
        Animator animator = DialogProcessor.singleton.parentObj.GetComponent<Animator>();


        if (dir == Direction.Down)
        { animator.SetBool("faceUp", true); }
        else if (dir == Direction.Up)
        { animator.SetBool("faceDown", true); }
        else if (dir == Direction.Right)
        { animator.SetBool("faceLeft", true); }
        else
        { animator.SetBool("faceRight", true); }

    }

    private void ResetAnimatorBools(Animator animator)
    {
        #region DisableAllTheBools
        animator.SetBool("faceUp", false);
        animator.SetBool("faceRight", false);
        animator.SetBool("faceDown", false);
        animator.SetBool("faceLeft", false);
        #endregion
    }

}

public enum Emote
{
    Empty,
    Thinking,
    Suprise,
    Angry,
    Awkward,
    Happy,
    Sad
}

public enum ChangeDirection
{
    NoChange,
    FaceUp,
    FaceRight,
    FaceDown,
    FaceLeft,
    FacePlayer
}