using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Enemy))]
public class EnemyMover : MonoBehaviour
{

    [SerializeField] private List<Waypoint> path = new();
    [SerializeField] private List<Vector3> pathPoints = new();
    private float speed;
    private float travelDistance;
    private Coroutine moveCoroutine;

    private void Start()
    {
        StartCoroutine(FollowPath(null));
    }

    public void ActivateMovement(Action finish)
    {
            ReturnToStart();
            moveCoroutine = StartCoroutine(FollowPath(finish));
    }
    public void StopCoroutine()
    {
        StopCoroutine(moveCoroutine);
    }
    public void SetMovementComponent(Waypoint[] waypoints, float speed)
    {
        foreach( Waypoint way in waypoints)
        {
            path.Add(way);
        }
        this.speed = speed;
    }

    private void ReturnToStart()
    {
        transform.position = path[0].transform.position;
    }

    public float GetTravelDistance()
    {
        return travelDistance;
    }

/*
    IEnumerator FollowPath(Action Finish)
    {
        foreach(Waypoint waypoint in path)
        {
            Vector3 startPosition = transform.position;
            Vector3 endPosition = waypoint.transform.position;
            travelPercent = 0f;

            transform.LookAt(endPosition);
            while (travelPercent < 1)
            {
                travelPercent += Time.deltaTime * speed;
                transform.position = Vector3.Lerp(startPosition, endPosition, travelPercent);
                yield return null;
            }
            distanceProcent += 1;
        }
        Finish();
    }
*/

    //Getters and Setters

    public void SetSpeed(float speed)
    {
        this.speed = speed;
    }
    public float GetSpeed()
    {
        return speed;
    }

    private IEnumerator FollowPath(Action finish)
    {
        foreach (Vector3 waypoint in pathPoints)
        {
            float distanceToWaypoint = Vector3.Distance(transform.position, waypoint);
            speed = 5f;
            while (distanceToWaypoint > 1)
            {
                distanceToWaypoint = Vector3.Distance(transform.position, waypoint);
                Quaternion lookAtPosition = Quaternion.LookRotation(waypoint-transform.position);
                transform.rotation = Quaternion.Slerp(transform.rotation, lookAtPosition, 1.5f * Time.deltaTime);
                float deltaTime = speed * Time.deltaTime;
                transform.Translate(0, 0, deltaTime);
                travelDistance += deltaTime;
                yield return null;
            }
        }
        finish();
    }

}
