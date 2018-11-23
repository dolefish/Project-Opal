using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CivilianNode : MonoBehaviour
{
    [Header("Direction")]
    public bool goUp = false;
    public bool goRight = false;
    public bool goDown = false;
    public bool goLeft = false;


    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.transform.gameObject.layer == 10)
        {
            float waitTime = CalculateWaitTime();
            StartCoroutine(ChangeDirection(waitTime, other));
            print("BUMP");
        }
    }

    public float CalculateWaitTime()
    {
        int rng = Random.Range(0, 6);
        float waitTime = 0;

        if (rng == 1)
        {
            waitTime = Random.Range(1, 4);
        }
        else
        {
            waitTime = 0f;
        }

        return waitTime;
    }

    IEnumerator ChangeDirection(float waitTime, Collider2D other)
    {
        bool decided = false;
        int rng = 0;
        Civilian civilian = other.GetComponent<Civilian>();
        Rigidbody2D thisRigidBody2D = other.GetComponent<Rigidbody2D>();

        thisRigidBody2D.velocity = Vector2.zero;

        for (int i = 0; decided == false; i++)
        {
            rng = Random.Range(0, 4);

            if (rng == 0 && goUp)
            {
                decided = true;
            }
            else if (rng == 1 && goRight)
            {
                decided = true;
            }
            else if (rng == 2 && goDown)
            {
                decided = true;
            }
            else if (rng == 3 && goLeft)
            {
                decided = true;
            }

        }

        yield return new WaitForSecondsRealtime(waitTime);
        

        if (rng == 0)  // Go Up
        {
            thisRigidBody2D.velocity = Vector2.up * civilian.moveSpeed;
        }
        else if (rng == 1)  // Go Right
        {
            thisRigidBody2D.velocity = Vector2.right * civilian.moveSpeed;
        }
        else if (rng == 2)  // Go Down
        {
            thisRigidBody2D.velocity = Vector2.down * civilian.moveSpeed;
        }
        else if (rng == 3)  // Go Left
        {
            thisRigidBody2D.velocity = Vector2.left * civilian.moveSpeed;
        }

        civilian.dir = rng;

    }
}
