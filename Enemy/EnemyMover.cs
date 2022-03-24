using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Enemy))]
public class EnemyMover : MonoBehaviour
{

    [SerializeField] List<Waypoint> path = new List<Waypoint>();
    [SerializeField] bool isStatic;

    private float speed;
    public Enemy enemy;
    // Start is called before the first frame update
    void OnEnable()
    {
        ActivateMovemnet();      
    }

    private void Start()
    {
        speed = enemy.GetInitSpeed();
    }

    void ActivateMovemnet()
    {
        if (!isStatic)
        {
            FindPath();
            ReturnToStart();
            StartCoroutine(FollowPath());
        }
    }

    void FindPath()
    {

        path.Clear();

        GameObject parent = GameObject.FindGameObjectWithTag("Path");

        foreach(Transform child in parent.transform)
        {
            Waypoint waypoint = child.GetComponent<Waypoint>();

            if (waypoint != null)
            {
                path.Add(waypoint);
            }
        }
    }
    void ReturnToStart()
    {
        transform.position = path[0].transform.position;
    }

    void FisnishPath()
    {
        gameObject.SetActive(false);
        enemy.StealdGold();
    }

    IEnumerator FollowPath()
    {
        foreach(Waypoint waypoint in path)
        {
            Vector3 startPosition = transform.position;
            Vector3 endPosition = waypoint.transform.position;
            float travelPercent = 0f;

            transform.LookAt(endPosition);

            while (travelPercent < 1)
            {
                travelPercent += Time.deltaTime * speed;
                transform.position = Vector3.Lerp(startPosition, endPosition, travelPercent);
                yield return new WaitForEndOfFrame();
            }
        }

        FisnishPath();
    }

    //public void AddSlow()
    //{ 
    //    speed = enemy.GetInitSpeed() / 2;
    //}
    //public void RemoveSlow()
    //{
    //    speed = enemy.GetInitSpeed();
    //}
    public void Messages()
    {
        Debug.Log("Mesaj");
    }

    public void SetSpeed(float speed)
    {
        this.speed = speed;
    }
    public float GetSpeed()
    {
        return speed;
    }
}
