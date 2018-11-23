using UnityEngine;
using System.Collections;

public class PlayerGridController : MonoBehaviour

{
    public static PlayerGridController singleton;

    bool canmove = true;
    Vector2 input;
    Vector2 startPosition;
    Vector2 endPosition;

    [HideInInspector]public Animator animator;

    [Header("Variables")]
    public float speed = 1;
    public float gridSize = 0.32f;

    [Header("Raycast Detection")]
    public float ray_Distance = 1;
    public Transform raycastPoint;

    void Awake()
    {
        singleton = this;
    }

    void Start()
    {
        animator = this.gameObject.GetComponent<Animator>();
        startPosition = this.transform.position;
        endPosition = this.transform.position;
    }

    void Update()
    {
        if (canmove && Input.GetAxis("Horizontal") != 0)
        {
            input = Vector2.zero;

            input.x = Input.GetAxis("Horizontal");

            if (input.x > 0)
            {
                ShootRaycast(Vector2.right);
            }
            else if (input.x < 0)
            {
                ShootRaycast(Vector2.left);
            }

        }

        if (canmove && Input.GetAxis("Vertical") != 0)
        {
            input = Vector2.zero;

            input.y = Input.GetAxis("Vertical");

            if (input.y > 0)
            {
                ShootRaycast(Vector2.up);
            }
            else if (input.y < 0)
            {
                ShootRaycast(Vector2.down);
            }

        }

        if (Input.GetButtonDown("Sprint"))
        {
            speed = speed * 2;
        }
        else if (Input.GetButtonUp("Sprint"))
        {
            speed = speed / 2;
        }
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.transform.name != "Player")
        {
            StopCoroutine("Move");
            canmove = true;
            input = Vector2.zero;
            this.transform.position = startPosition;
        }

    }

    void ShootRaycast(Vector2 direction)
    {
        // Shoot ray based on player pos + offset
        RaycastHit2D hit = Physics2D.Raycast(raycastPoint.position, direction, ray_Distance);


        if (hit && hit.transform.tag == "Interactable")
        {
            #region Face Direction
            if (direction == Vector2.up)
            {
                animator.Play("face_up");
            }
            else if (direction == Vector2.down)
            {
                animator.Play("face_down");
            }
            else if (direction == Vector2.right)
            {
                animator.Play("face_right");
            }
            else if (direction == Vector2.left)
            {
                animator.Play("face_left");
            }
            else { print("Can't find Dir!"); }
            #endregion
        }
        else if (hit && hit.transform.tag != "Player")
        {
            #region Face Direction
            if (direction == Vector2.up)
            {
                animator.Play("face_up");
            }
            else if (direction == Vector2.down)
            {
                animator.Play("face_down");
            }
            else if (direction == Vector2.right)
            {
                animator.Play("face_right");
            }
            else if (direction == Vector2.left)
            {
                animator.Play("face_left");
            }
            else { print("Can't find Dir!"); }
            #endregion
        }
        else
        {
            StartCoroutine(Move());
        }

       
    }

    IEnumerator Move()
    {
        canmove = false;
        startPosition = transform.position;


        if (input.x > 0 || input.x < 0)
        {
            if (input.x > 0)
            {
                endPosition = new Vector2(startPosition.x + gridSize, startPosition.y);
                animator.Play("walk_right");
            }
            else
            {
                endPosition = new Vector2(startPosition.x - gridSize, startPosition.y);
                animator.Play("walk_left");
            }
        }

        else if (input.y > 0 || input.y < 0)
        {
            if (input.y > 0)
            {
                endPosition = new Vector2(startPosition.x, startPosition.y + gridSize);
                animator.Play("walk_up");
            }
            else
            {
                endPosition = new Vector2(startPosition.x, startPosition.y - gridSize);
                animator.Play("walk_down");
            }
        }

        while (Vector2.Distance(transform.position, endPosition) > 0.01)
        {
            transform.position = Vector2.MoveTowards(transform.position, endPosition, speed * Time.deltaTime);
            yield return null ;
        }

        animator.StopPlayback();

        transform.position = endPosition;
        ValuesScript.singleton.playerPosition = transform.position;

        input = Vector2.zero;

        canmove = true;

        yield return null;
    }

    public void EnablePlayerMovement(bool state)
    {
        StopAllCoroutines();
        canmove = state;
        transform.position = endPosition;
        input = Vector2.zero;
    }

    public bool CanMove()
    {
        return canmove;
    }
}