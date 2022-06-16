using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonTowerScript : Tower
{
    private Dictionary<string, string> detailList;
    float cooldown = 4;

    //private void Start()
    //{
    //    range = 20f;
    //    damage = 10f;
    //}
    void Update()
    {
        TargetingAndCooldown();
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
        return new SelectedInfo(detailList, icon);
    }
    protected override void Attack()
    {
        List<Enemy> enemyListHit = tLocator.GetTargetInRange(firstTarget.transform.position, ReturnManager().GetEnemyList(), 15f);
        foreach(Enemy enemy in enemyListHit)
        {
            enemy.eHealth.IncDamage(damage);
            if(statusSelected != null)
            {
                enemy.AddDebuff(statusSelected.InitializeBuff(enemy.gameObject));
            }
        }
        timerBullet = cooldown;
        timerSearch = 0;
    }
}
