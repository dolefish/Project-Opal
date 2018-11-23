using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour {

    public float horizontal_Offset;
    public float vertical_Offset;
    public float move_speed;
    public bool snapToPosition;
    Camera m_cam;
    public Transform player;

    float left_bound = 0;
    float right_bound = 0;
    float up_bound = 0;
    float down_bound = 0;

    bool is_moving;

    void Awake()
    {
        m_cam = GetComponent<Camera>();
    }

    void Start()
    {
        CalculateBounds(m_cam.transform.position);
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void FixedUpdate()
    {
        int temp = PlayerOutOfBounds();

        if (temp > 0 && is_moving == false)
        {
            switch (temp)
            {
                case 1:
                    CalculateBounds(m_cam.transform.position + new Vector3(-horizontal_Offset, 0, 0));
                    MoveThatCamera(new Vector3(-horizontal_Offset, 0, 0));
                    break;
                case 2:
                    CalculateBounds(m_cam.transform.position + new Vector3(horizontal_Offset, 0, 0));
                    MoveThatCamera(new Vector3(horizontal_Offset, 0, 0));
                    break;
                case 3:
                    CalculateBounds(m_cam.transform.position + new Vector3(0, vertical_Offset, 0));
                    MoveThatCamera(new Vector3(0, vertical_Offset, 0));
                    break;
                case 4:
                    CalculateBounds(m_cam.transform.position + new Vector3(0, -vertical_Offset, 0));
                    MoveThatCamera(new Vector3(0, -vertical_Offset, 0));
                    break;
            }
        }
    }

    void MoveThatCamera(Vector3 dir)
    {
        if (snapToPosition)
        { transform.position = m_cam.transform.position + dir; }
        else
        { StartCoroutine(MoveCam(m_cam.transform.position + dir)); }
    }

    IEnumerator MoveCam(Vector3 end)
    {
        is_moving = true;

        while (Vector2.Distance(m_cam.transform.position, end) > 0.005)
        {
            m_cam.transform.position = Vector2.Lerp(m_cam.transform.position, end, Time.deltaTime * move_speed);
            yield return null;
        }

        m_cam.transform.position = end;

        is_moving = false;
    }

    int PlayerOutOfBounds()
    {
        if (player.position.x < left_bound)
        {
            return 1;
        }

        if (player.position.x > right_bound)
        {
            return 2;
        }

        if (player.position.y > up_bound)
        {
            return 3;
        }

        if (player.position.y < down_bound)
        {
            return 4;
        }

        return 0;
    }

    void CalculateBounds(Vector3 cam_pos)
    {
        float height = 2f * m_cam.orthographicSize;
        float width = height * m_cam.aspect;

        left_bound = cam_pos.x - (width / 2);
        right_bound = cam_pos.x + (width / 2);
        up_bound = cam_pos.y + (height / 2);
        down_bound = cam_pos.y - (height / 2);
    }
}
