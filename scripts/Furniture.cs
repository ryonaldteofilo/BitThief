using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : MonoBehaviour
{
    [SerializeField] float points;

    public float GetPoints()
    {
        return points;
    }


}
