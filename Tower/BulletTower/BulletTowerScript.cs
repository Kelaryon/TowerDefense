using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletTowerScript : Tower
{
    private Dictionary<string, string> detailList;
    [SerializeField] BulletScript projectile;
    float timerBullet = 0;
    readonly float cooldown = 5;
    Enemy firstTarget;
    float targetDisance;
    void Update()
    {
        //Problematic daca exista inamici cu o viteza mai mare dacat alti;
        //Mai poate fi optimizat
            firstTarget = TargetLocatorPrime.FindClosestTarget(transform.position,ReturnManager().GetEnemyList());
            targetDisance = Vector3.Distance(transform.position, firstTarget.transform.position);
            if (targetDisance < range && timerBullet <= 0)
            {
                BulletScript activeProjectile = Instantiate(projectile, transform.position, Quaternion.identity);
                activeProjectile.Setup(firstTarget.transform, damage);
                //activeProjectile.Setup(firstTarget, damage);
                timerBullet = cooldown;

            }
            if (timerBullet > 0)
            {
                timerBullet -= Time.deltaTime;
            }

    }
    public override Dictionary<string, string> GetInfo()
    {
        detailList = new Dictionary<string, string>
        {
            { "Range", range.ToString() },
            { "Cost", cost.ToString() },
            { "Damage", damage.ToString() },
            { "Details", "Poc Poc Tower" }
        };
        return detailList;
    }
}
