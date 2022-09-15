using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoomerangTowerScript : Tower
{
    private Dictionary<string, string> detailList;
    [SerializeField] BoomerangScript projectile;
    float cooldown = 5;

    public void Reset()
    {
        damage = 2.0f;
    }
    private void Start()
    {
        range = 20f;
        damage = 10f;
    }
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
            { "Details", "Boomerang goes brrr" }
        };
        return new SelectedInfo(detailList,towerIcon);
    }
    protected override void Attack()
    {
        BoomerangScript activeProjectile = Instantiate(projectile, transform.position, Quaternion.identity);
        activeProjectile.Setup(firstTarget, damage, this);
        timerBullet = cooldown;
        timerSearch = 0;
        //lol
    }
    public TargetLocatorPrime ReturnTargetLocator()
    {
        return tLocator;
    }
    //void Target()
    //{
    //    if (ReturnManager().GetEnemyList().Count != 0)
    //    {
    //        try
    //        {
    //            Enemy firstTarget = tLocator.FindClosestTarget(transform.position, ReturnManager().GetEnemyList());
    //            targetDisance = Vector3.Distance(transform.position, firstTarget.transform.position);
    //        }
    //        catch (NullReferenceException)
    //        {
    //            Debug.Log("Lista e goala");
    //        }
    //        if (targetDisance < range && timerBullet <= 0)
    //        {
    //            BulletScript activeProjectile = Instantiate(projectile, transform.position, Quaternion.identity);
    //            activeProjectile.Setup(firstTarget.transform, damage);
    //            //activeProjectile.Setup(firstTarget, damage);
    //            timerBullet = cooldown;

    //        }
    //    }
    //}
}
