using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController singleton;

    private Animator animator;
    private Rigidbody2D player_rb;
    public Direction player_Dir;

    public Sprite up;
    public Sprite left;
    public Sprite down;
    public Sprite right;

    public float walkSpeed;
    public bool canMove = true;
    
    void Awake()
    {
        singleton = this;
        animator = GetComponent<Animator>();
        player_rb = this.GetComponent<Rigidbody2D>();
        if (animator == null || player_rb == null)
        {
            print("Can't find component! Animator: " + animator + "RigidBody2D: " + player_rb);
        }
    }

    void Update()
    {
        if(canMove)
        {
            if (Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0)
            {
                float horizontal = Input.GetAxisRaw("Horizontal");
                float vertical = Input.GetAxisRaw("Vertical");
                Vector2 dir = new Vector2(horizontal, vertical);
                Move(dir.normalized);
            }
            else
            {
                Move(Vector2.zero);
            }
        }

        SetDirection();
        PlayAnim();
    }

    void Move(Vector2 dir)
    {
        player_rb.velocity = dir * walkSpeed;
    }

    void SetDirection()
    {
        if (player_rb.velocity.x > 0 && player_rb.velocity.y == 0)
        {
            player_Dir = Direction.Right;
        }
        else if (player_rb.velocity.x > 0 && player_rb.velocity.y > 0)
        {
            player_Dir = Direction.UpRight;
        }
        else if (player_rb.velocity.x > 0 && player_rb.velocity.y < 0)
        {
            player_Dir = Direction.DownRight;
        }
        else if (player_rb.velocity.x < 0 && player_rb.velocity.y == 0)
        {
            player_Dir = Direction.Left;
        }
        else if (player_rb.velocity.x < 0 && player_rb.velocity.y > 0)
        {
            player_Dir = Direction.UpLeft;
        }
        else if (player_rb.velocity.x < 0 && player_rb.velocity.y < 0)
        {
            player_Dir = Direction.DownLeft;
        }
        else if (player_rb.velocity.x == 0 && player_rb.velocity.y > 0)        
        {
            player_Dir = Direction.Up;
        }
        else if (player_rb.velocity.x == 0 && player_rb.velocity.y < 0)
        {
            player_Dir = Direction.Down;
        }
    }

    void PlayAnim()
    {
        if (player_rb.velocity != Vector2.zero)
        {
            switch (player_Dir)
            {
                case Direction.Up:
                    animator.Play("walk_up");
                    break;
                case Direction.Down:
                    animator.Play("walk_down");
                    break;
                case Direction.Left:
                    animator.Play("walk_left");
                    break;
                case Direction.Right:
                    animator.Play("walk_right");
                    break;
                case Direction.UpRight:
                    animator.Play("walk_right");
                    break;
                case Direction.UpLeft:
                    animator.Play("walk_left");
                    break;
                case Direction.DownRight:
                    animator.Play("walk_right");
                    break;
                case Direction.DownLeft:
                    animator.Play("walk_left");
                    break;
            }
        }
        else
        {
            switch (player_Dir)
            {
                case Direction.Up:
                    animator.Play("face_up");
                    break;
                case Direction.Down:
                    animator.Play("face_down");
                    break;
                case Direction.Left:
                    animator.Play("face_left");
                    break;
                case Direction.Right:
                    animator.Play("face_right");
                    break;
                case Direction.UpRight:
                    animator.Play("face_right");
                    break;
                case Direction.UpLeft:
                    animator.Play("face_left");
                    break;
                case Direction.DownRight:
                    animator.Play("face_right");
                    break;
                case Direction.DownLeft:
                    animator.Play("face_left");
                    break;
            }
        }
    }
}


public enum Direction
{
    Up,
    Down,
    Left,
    Right,
    UpLeft,
    UpRight,
    DownLeft,
    DownRight
}
