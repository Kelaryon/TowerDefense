using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;

public class Tower : Building
{

    //Basic Values
    [SerializeField] public float range;
    [SerializeField] public float damage;

    //Tehnical
    [SerializeField] protected TargetLocatorPrime tLocator;
    //public EnemyManager enemyManager;

    [SerializeField] private GameObject rangeCricle;
    public Sprite towerIcon;
    //Timers
    protected float timerBullet = 0f;
    protected float timerSearch = 0f;

    protected Enemy firstTarget;
    float targetDisance;



    [SerializeField] ScriptableStatusEffect[] statusList;
    protected ScriptableStatusEffect statusSelected = null;

    private void Awake()
    {
        if (rangeCricle != null)
        {
            rangeCricle.transform.localScale = (rangeCricle.transform.localScale / 10 * range);
            ActivateRangeCircle(false);
        }
    }
    public override void ActivateRangeCircle(bool rangeBool)
    {
        rangeCricle.SetActive(rangeBool);
    }
    public override void Setup(List<Waypoint> wayList, Bank bank)
    {
        base.Setup(wayList,bank);
        //SetEnemyManager(bank.GetEnemyManager());
    }

    public override SelectedInfo GetInfo()
    {
        return new SelectedInfo(
            new Dictionary<string, string>
            {
                { "Range", range.ToString() },
                { "Cost", cost.ToString() },
                { "Damage", damage.ToString() },
                { "FlavourText", "Just a basic tower" }
            },
            towerIcon,
            new MethodsInfo[]
            {
                new MethodsInfo(DestroyTower,"Destroy Tower","Click to destroy the selected tower",true)
            }
        );

    }
    protected virtual void Attack()
    {
        throw new Exception("Trebuie definita functia in turn");
    }

    protected bool Target()
    {
        //Debug.Log(enemyManager);
        if (bank.GetEnemyList().Count != 0)
        {
            try
            {
                firstTarget = tLocator.FindClosestTarget(transform.position, bank.GetEnemyList());
                targetDisance = Vector3.Distance(transform.position, firstTarget.transform.position);
            }
            catch (NullReferenceException)
            {
                Debug.Log("Lista e goala");
            }
            if (targetDisance < range)
            {
                Attack();
                return true;
            }
            else { return false; }
        }
        else
        {
            return false;
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

    #region   Dot System
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
    #endregion

    public virtual void DestroyTower()
    {
        foreach (Waypoint way in waypointList)
        {
            way.SetPlaceble();
            way.SetStructure(null);
        }
        bank.controlPanel.Deselect();
        Destroy(this.gameObject);
    }

    // Interface Methods
    public override void Activate()
    {
        ActivateRangeCircle(true);
    }
    public override void Deactivate()
    {
        ActivateRangeCircle(false);
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
    //public new virtual SelectedInfo GetInfo()
    //{
    //    return new SelectedInfo(new Dictionary<string, string>
    //    {
    //        { "Range", range.ToString() },
    //        { "Cost", cost.ToString() },
    //        { "Damage", damage.ToString() },
    //        { "waypoint", waypointList.ToString() },
    //        { "FlavourText", "Just a basic tower" }
    //    }, towerIcon);

    //}
    //void SetEnemyManager(EnemyManager eManger)
    //{
    //    enemyManager = eManger;
    //}
    //public EnemyManager ReturnManager()
    //{
    //    return enemyManager;
    //}
    //Current targeting system tha uses the enemy list from the enemy manager
    //currently only works with one manager
    //To be modified for adiditonal managers
    //The whole system need to be reimplemented;
}
