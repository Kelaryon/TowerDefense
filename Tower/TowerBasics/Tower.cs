using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tower : MonoBehaviour,ISelectableInterface
{
    public float range = 15f;
    public int cost = 75;
    public float damage = 1f;
    private Dictionary<string, string> detailList;
    //public LineRenderer line;
    public Sprite icon;
    Waypoint waypoint;
    public EnemyManager enemyManager;
    public bool CreateTower(Tower tower, Vector3 position,Bank bank,Waypoint waypoint)
    {
        if (bank == null)
        {
            return false;
        }

        if(bank.CurrentBallance >= cost){

            Tower towers = Instantiate(tower, position, Quaternion.identity);
            towers.SetWaypoint(waypoint);
            bank.Withdraw(cost);
            enemyManager = bank.GetEnemyManager();
            return true;

        }
        return false;
    }
    public EnemyManager ReturnManager()
    {
        return enemyManager;
    }
   public virtual Dictionary<string,string> GetInfo()
    {
        detailList = new Dictionary<string, string>
        {
            { "Range", range.ToString() },
            { "Cost", cost.ToString() },
            { "Damage", damage.ToString() },
            { "waypoint", waypoint.ToString() },
            { "FlavourText", "Just a basic tower" }
        };
        return detailList;

    }
    public Waypoint GetWaypoint()
    {
        return waypoint;
    }
    public void SetWaypoint(Waypoint way)
    {
        this.waypoint = way;
    }
    //private void Start()
    //{
    //    //line = gameObject.GetComponent<LineRenderer>();

    //    //line.SetVertexCount(30 + 1);
    //    //line.useWorldSpace = false;
    //    //CreateLine();
    //    //line.enabled = false;

    //}
    //private void OnMouseOver()
    //{
    //    line.enabled = true;
    //}
    //private void OnMouseExit()
    //{
    //    line.enabled = false;
    //}

    //public void CreateLine()
    //{
    //    float x;
    //    float z;
    //    float angle = 30;

    //    for(int i =0; i < 31; i++)
    //    {
    //        x = Mathf.Sin(Mathf.Deg2Rad * angle) * range;
    //        z = Mathf.Cos(Mathf.Deg2Rad * angle) * range;
    //        line.SetPosition(i, new Vector3(x, 0, z));
    //        angle += (360f / 30);
    //    }
    //}
}
