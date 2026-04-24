using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonTowerScript : Tower
{
    private Dictionary<string, string> detailList;
    readonly float cooldown = 4;
    [SerializeField] Transform topGun;
    bool idle = true;

    private void Start()
    {
        updateFunction += TargetingAndCooldown;
        attackFuntion += Attack;
    }
    void Update()
    {
        //TargetingAndCooldown();
        updateFunction();
        IdleCheck();
    }
    void IdleCheck()
    {
        if(idle)
        {
            topGun.transform.localEulerAngles = new Vector3(0, 0, Mathf.PingPong(Time.time * 60, 90)-135);
        }
        if(firstTarget == null)
        {
            idle = true;
        }
    }
    protected override void Attack()
    {
        idle = false;
        topGun.transform.LookAt(firstTarget.transform);
        topGun.LookAt(firstTarget.transform);
        List<Enemy> enemyListHit = tLocator.GetTargetsInRangePhys(firstTarget.transform.position, 15f);
        foreach(Enemy enemy in enemyListHit)
        {
            enemy.IncDamage(damage);
            if(statusSelected != null)
            {
                enemy.AddDebuff(statusSelected.InitializeBuff(enemy.gameObject));
            }
        }
        timerBullet = cooldown;
        timerSearch = 0;
    }
    public override SelectedInfo GetInfo()
    {

        detailList = new Dictionary<string, string>
        {
            { "Range", range.ToString() },
            { "Cost", cost.ToString() },
            { "Damage", damage.ToString() },
            { "Details", "Engage!" }
        };
        return new SelectedInfo(detailList, towerIcon, null);
    }
}
