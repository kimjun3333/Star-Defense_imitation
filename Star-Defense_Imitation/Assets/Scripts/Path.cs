using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Path : MonoBehaviour
{
    public Transform[] Waypoints;

    private void Start()
    {
        Waypoints = new Transform[transform.childCount];

        for(int i = 0; i < transform.childCount; i++)
        {
            Waypoints[i] = transform.GetChild(i);
        }
    }
}
