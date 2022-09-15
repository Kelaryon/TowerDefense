using System.Collections.Generic;
using UnityEngine;

public class BulletTowerScript : Tower
{
    private Dictionary<string, string> detailList;
    [SerializeField] BulletScript projectile;
    readonly float cooldown = 2.5f;


    private void Start()
    {
        SetRange(30f);
        tLocator = GetComponent<TargetLocatorPrime>();
        damage = 5f;
    }
    void Update()
    {
        //Problematic daca exista inamici cu o viteza mai mare dacat alti;
        //Mai poate fi optimizat
        //TargetingAndCooldown();
        TargetingAndCooldown();

    }

    //public override SelectedInfo GetInfo()
    //{
    //    detailList = new Dictionary<string, string>
    //    {
    //        { "Range", range.ToString() },
    //        { "Cost", cost.ToString() },
    //        { "Damage", damage.ToString() },
    //        { "Details", "Poc Poc Tower" }
    //    };
    //    return new SelectedInfo(detailList, towerIcon);
    //}
    protected override void Attack()
    {
        SetCooldown(cooldown);
        BulletScript activeProjectile = Instantiate(projectile, transform.position, Quaternion.identity);
        activeProjectile.Setup(firstTarget.transform, damage, statusSelected);
        //activeProjectile.Setup(firstTarget, damage);
        
    }
    void SetRange(float range)
    {
        base.range = range;
    }
}
