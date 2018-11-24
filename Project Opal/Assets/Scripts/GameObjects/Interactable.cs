using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour {

    [Header("Interaction Box")]
    public string interaction_message;
    public float interact_distance = 0.3f;

    [Header("Conditions")]
    public bool onDistance = false;
    public float targetDistance = 0f;
    public bool onKeyDown = true;
    public RequiredDirection requiredDirection;

    [Header("Interact Dialog")]
    public Dialog dialog;

    [Header("Interact SceneTrigger")]
    public ChangeSceneTrigger sceneTrigger;

    [Header("Debug")]
    public bool interacting;
    public MissionControl missionControlCheck;
    private PlayerController player;
    private UIObjects ui;
    public Direction playerDirection;
    public ChangeDirection startingDirection;

    public enum RequiredDirection
    {
        Up,
        Down,
        Left,
        Right,
        None
    }

    public void Start()
    {
        ui = UIObjects.singleton;
        player = PlayerController.singleton;
        Animator animator = GetComponent<Animator>();

        if (animator != null)
        { SetDirection(animator); }
        
    }

    public void Update()
    {
        if ((TargetDistance() < targetDistance) && (!interacting && onDistance))
        { OnInteractStart(); }

        if((onKeyDown && PlayerInRange()) && !interacting)
        {
            print("[>>]Waiting for user input ...");
            if (Input.GetButtonDown("Submit"))
            { OnInteractStart(); }
        }
    }

    public float TargetDistance()
    {
        float distance = Vector2.Distance(player.transform.position, transform.position);
        playerDirection = player.player_Dir;
        return distance;
    }

    public bool PlayerInRange()
    {
        if (TargetDistance() <= interact_distance)
        {
            ui.actionBoxObjects.ShowActionBox(true, interaction_message);
            return true;
        }

        ui.actionBoxObjects.ShowActionBox(false);
        return false;
    }

    public void OnInteractStart()
    {
        interacting = true;

        if (requiredDirection == RequiredDirection.None)
        { }
        else if (!PlayerFacingRightDirection())
        { OnInteractStop();}

        if (dialog != null && interacting)
        {
            DialogProcessor.singleton.StartDialog(dialog, gameObject);
        }

        if (sceneTrigger != null)
        {
            sceneTrigger.CallChangeScene();
        }
    }

    private bool PlayerFacingRightDirection()
    {
        if (playerDirection.ToString() == requiredDirection.ToString())
        { return true; }
        else
        { return false; }

    }

    public void SetDirection(Animator animator)
    {
        #region DisableAllTheBools
        animator.SetBool("faceUp", false);
        animator.SetBool("faceRight", false);
        animator.SetBool("faceDown", false);
        animator.SetBool("faceLeft", false);
        #endregion

        print("Changing Direction: " + animator + " / " + startingDirection.ToString());

        switch (startingDirection)
        {
            case ChangeDirection.NoChange:
                break;
            case ChangeDirection.FaceUp:
                animator.SetBool("faceUp", true);
                break;
            case ChangeDirection.FaceRight:
                animator.SetBool("faceRight", true);
                break;
            case ChangeDirection.FaceDown:
                animator.SetBool("faceDown", true);
                break;
            case ChangeDirection.FaceLeft:
                animator.SetBool("faceLeft", true);
                break;
            case ChangeDirection.FacePlayer:
                SetDirectionToPlayer();
                break;
        }
    }

    private void SetDirectionToPlayer()
    {
        Animator animator = GetComponent<Animator>();

        if (playerDirection == Direction.Down)
        { animator.SetBool("faceUp", true); }
        else if (playerDirection == Direction.Up)
        { animator.SetBool("faceDown", true); }
        else if (playerDirection == Direction.Right)
        { animator.SetBool("faceRight", true); }
        else
        { animator.SetBool("faceLeft", true); }

    }

    public void OnInteractStop()
    {
        if (interacting)
        {
            print("Stopping Interaction");
            player.canMove = true;
            interacting = false;
        }

        ui.ResetChecks();
    }

}
