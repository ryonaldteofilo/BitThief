using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class myUtilities
{ 
    public static Vector3 Angle2Vector(float angleDeg)
    {
        float angleRad = angleDeg * Mathf.Deg2Rad;
        return new Vector3(Mathf.Cos(angleRad), Mathf.Sin(angleRad), 0f); // 0 degrees is right
    }

    public static float Vector2Angle(Vector3 vector)
    {
        vector = vector.normalized;
        float angleDeg = Mathf.Atan2(vector.y, vector.x) * Mathf.Rad2Deg;
        if(angleDeg < 0)
        {
            angleDeg += 360;
        }
        return angleDeg;
    }

}
