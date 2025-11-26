using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Scripting.APIUpdating;

public class EnemyController : MonoBehaviour
{
    private EnemyInstance instance;

    private Transform[] waypoints;
    private int waypointIndex = 0;

    public void Init(EnemySO so, Transform[] path)
    {
        instance = new EnemyInstance(so);
        waypoints = path;
        waypointIndex = 0;
        transform.position = waypoints[0].position;
    }

    private void Update()
    {
        Move();
    }

    private void Move()
    {
        if (waypointIndex >= waypoints.Length) return;

        transform.position = Vector3.MoveTowards(
            transform.position,
            waypoints[waypointIndex].position,
            instance.CurrentSpeed * Time.deltaTime
            );

        if(Vector3.Distance(transform.position, waypoints[waypointIndex].position) < 0.1f)
        {
            waypointIndex++;
            if(waypointIndex >= waypoints.Length)
            {
                Debug.Log("골인");
            }
        }
    }

}
