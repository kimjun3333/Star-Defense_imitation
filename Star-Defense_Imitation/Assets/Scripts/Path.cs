using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Path : MonoBehaviour
{
    public List<Transform> Waypoints = new();

    private void Start()
    {
        Debug.Log("Path 실행");
        Waypoints.Clear();

        for(int i = 0; i < transform.childCount; i++)
        {
            Waypoints.Add(transform.GetChild(i));
        }
    }
}
