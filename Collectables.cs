using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectables : MonoBehaviour
{
    [SerializeField] float points;

    public float GetPoints()
    {
        return points;
    }
}
