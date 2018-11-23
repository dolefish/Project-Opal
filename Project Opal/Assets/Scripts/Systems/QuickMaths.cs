using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuickMaths : MonoBehaviour
{
    public static Vector2 AngleToVector2(float angle)
    {
        float newAngle = angle * Mathf.Deg2Rad;
        return new Vector2(Mathf.Cos(newAngle), Mathf.Sin(newAngle));
    }

    public static float Vector2ToAngle(float x, float y)
    {
        return Mathf.Atan2(y, x) * Mathf.Rad2Deg;
    }

    /*
    public static Vector3 VectorToPlayer(Transform source)
    {      
        float xPos = PlayerController.singleton.transform.position.x - source.position.x;
        float yPos = PlayerController.singleton.transform.position.y - source.position.y;

        return new Vector3(xPos, yPos, 0).normalized;
    }

    public static float DistanceFromObjectToPlayer(Transform source)
    {
        return Vector3.Distance(source.position, PlayerController.singleton.transform.position);
    }
    */

    public static float SourceToTargetAngle(Vector3 source, Vector3 target)
    {
        Vector3 dir = target - source;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        return angle;
    }

}
