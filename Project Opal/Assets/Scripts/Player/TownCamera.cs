using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TownCamera : MonoBehaviour
{
    [Header("Offset")]
    public Vector2 minOffSet;
    public Vector2 maxOffSet;

    [Header("Player")]
    public Transform player;

    [Header("Debug")]
    public float moveX;
    public float moveY;

    private void Start()
    {
        this.transform.position = PlayerController.singleton.transform.position;
        player = PlayerController.singleton.transform;
        moveX = player.transform.position.x;
        moveY = player.transform.position.y;
    }

    private void FixedUpdate()
    {
        if (player.position.x < maxOffSet.x && player.position.x > minOffSet.x)
        { moveX = player.position.x; }

        if (player.position.y < maxOffSet.y && player.position.y > minOffSet.y)
        { moveY = player.position.y; }

        MoveCamera();
    }

    private void MoveCamera()
    {
        transform.position = new Vector3(moveX, moveY, -10);
    }

}
