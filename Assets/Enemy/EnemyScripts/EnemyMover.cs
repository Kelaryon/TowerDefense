using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Enemy))]
public class EnemyMover : MonoBehaviour
{

    [SerializeField] List<Waypoint> path = new List<Waypoint>();
    [SerializeField] bool isStatic;
    private float speed;
    private float distanceProcent;
    private float travelPercent;
    private Coroutine moveCoroutine;

    public void ActivateMovemnet(Action Finish)
    {
        if (!isStatic)
        {
            ReturnToStart();
            moveCoroutine = StartCoroutine(FollowPath(Finish));
            distanceProcent = 0;
        }
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
    void ReturnToStart()
    {
        transform.position = path[0].transform.position;
    }

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

    public void SetSpeed(float speed)
    {
        this.speed = speed;
    }
    public float GetSpeed()
    {
        return speed;
    }
    public float ReturnDistanceProcent()
    {
        return (distanceProcent + travelPercent) / path.Count;
    }
}
