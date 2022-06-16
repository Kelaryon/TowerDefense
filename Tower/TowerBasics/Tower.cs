using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class Tower : MonoBehaviour,ISelectableInterface
{
    [SerializeField] public float range;
    [SerializeField] public int cost;
    [SerializeField] public float damage;
    [SerializeField] protected TargetLocatorPrime tLocator;
    protected float timerBullet = 0f;
    protected float timerSearch = 0f;
    protected Enemy firstTarget;
    float targetDisance;
    public Sprite icon;
    Waypoint waypoint;
    public EnemyManager enemyManager;
    [SerializeField] ScriptableStatusEffect[] statusList;
    protected ScriptableStatusEffect statusSelected = null;
    //private void Awake()
    //{
    //    tLocator = GetComponent<TargetLocatorPrime>();
    //}
    public bool CreateTower(Tower tower, Vector3 position,Bank bank,Waypoint waypoint)
    {
        if (bank == null)
        {
            return false;
        }

        if(bank.CurrentBallance >= cost){
            //Don't forget to set the variable values to the tower ponter not the tower itse;f
            Tower towers = Instantiate(tower, position, Quaternion.identity);
            towers.SetWaypoint(waypoint);
            bank.Withdraw(cost);
            towers.SetEnemyManager(bank.GetEnemyManager());
            return true;

        }
        return false;
    }
    void SetEnemyManager(EnemyManager eManger)
    {
        enemyManager = eManger;
    }
    public EnemyManager ReturnManager()
    {
        return enemyManager;
    }
   public virtual SelectedInfo GetInfo()
    {
        return new SelectedInfo(new Dictionary<string, string>
        {
            { "Range", range.ToString() },
            { "Cost", cost.ToString() },
            { "Damage", damage.ToString() },
            { "waypoint", waypoint.ToString() },
            { "FlavourText", "Just a basic tower" }
        }, icon);

    }
    public Waypoint GetWaypoint()
    {
        return waypoint;
    }
    public void SetWaypoint(Waypoint way)
    {
        this.waypoint = way;
    }
    protected virtual void Attack()
    {
        throw new Exception("Trebuie definita functia in turn");
    }
    protected void Target()
    {
        Debug.Log(enemyManager);
        if (enemyManager.GetEnemyList().Count != 0)
        {
            try
            {
                firstTarget = tLocator.FindClosestTarget(transform.position, ReturnManager().GetEnemyList());
                targetDisance = Vector3.Distance(transform.position, firstTarget.transform.position);
            }
            catch (NullReferenceException)
        {
            Debug.Log("Lista e goala");
        }
            if (targetDisance < range)
            {
                Attack();
            }
        }
    }
    virtual protected void TargetingAndCooldown()
    {
        if (timerBullet > 0)
        {
            timerBullet -= Time.deltaTime;
        }
        else
        {
            if (timerSearch > 0)
            {
                timerSearch -= Time.deltaTime;
            }
            else
            {
                timerSearch = 0.5f;
                Target();
            }
        }
    }
    public void SetCooldown(float cooldown)
    {
        timerBullet = cooldown;
        timerSearch = 0;
    }
    public List<string> GetStatusEffectList()
    {
        List<string> returnStatusList = new List<string>();
        if (statusList != null)
        {
            foreach (ScriptableStatusEffect st in statusList)
            {
                returnStatusList.Add(st.name);
            }
        }
        returnStatusList.Insert(0,"No effect");
        return returnStatusList;
    }
    public virtual void SetStatus(int val)
    {
        if (val == 0)
        {
            statusSelected = null;
            Debug.Log("No debuff");
        }
        else
        {
            statusSelected = statusList[val - 1];
            Debug.Log(statusSelected.name);
        }
    }
    public ScriptableStatusEffect GetStatusSelected()
    {
        return statusSelected;
    }
    public void DestroyTower()
    {
        waypoint.SetPlaceble();
        Destroy(this.gameObject);
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
