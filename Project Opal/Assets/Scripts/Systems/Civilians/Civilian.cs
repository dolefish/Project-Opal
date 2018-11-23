using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Civilian : MonoBehaviour
{
    private Rigidbody2D rb2d;

    public float moveSpeed;

    public int dir;

    private float kill;


    private void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    private void OnCollisionEnter2D(Collision2D coll)
    {
        StopAllCoroutines();
        StartCoroutine(WaitForRandomTime());
        rb2d.velocity = Vector2.zero;
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        kill += 0.1f;

        if (kill >= 20f)
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        StopAllCoroutines();
        StartMoving();
    }

    void StartMoving()
    {
        if (dir == 0)  // Go Up
        {
            rb2d.velocity = Vector2.up * moveSpeed;
        }
        else if (dir == 1)  // Go Right
        {
            rb2d.velocity = Vector2.right * moveSpeed;
        }
        else if (dir == 2)  // Go Down
        {
            rb2d.velocity = Vector2.down * moveSpeed;
        }
        else if (dir == 3)  // Go Left
        {
            rb2d.velocity = Vector2.left * moveSpeed;
        }
    }

    IEnumerator WaitForRandomTime()
    {
        yield return new WaitForSeconds(Random.Range(0, 4));
        StartMoving();
    }

}
