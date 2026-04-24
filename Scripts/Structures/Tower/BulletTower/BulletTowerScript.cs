using System.Collections.Generic;
using UnityEngine;
using System;

public class BulletTowerScript : Tower
{
    private Dictionary<string, string> detailList;
    private SelectedInfo selectedInfo;
    private MethodsInfo[] methodsList;
    [SerializeField] BulletScript projectile;
    float cooldown = 2.5f;


    private void Start()
    {
        damage = 15f;
        updateFunction += TargetingAndCooldown;
        attackFuntion += Attack;
        TowerBasic();
    }
    void Update()
    {
        //Problematic daca exista inamici cu o viteza mai mare dacat alti;
        //Mai poate fi optimizat
        //TargetingAndCooldown();
        //Rezolvat cumva;
        updateFunction();

    }

    //Subscribe to the attacked enemy when dying

    public override SelectedInfo GetInfo()
    {
        return selectedInfo;
    }
    protected override void Attack()
    {
        SetCooldown(cooldown);
        BulletScript activeProjectile = Instantiate(projectile, transform.position, Quaternion.identity);
        activeProjectile.Setup(firstTarget, damage, statusSelected);
    }
    private void TowerUpdateRapidFire()
    {
        cooldown = 0.5f;
        damage = 3.5f;
        selectedInfo.methodList[0] = null;
        selectedInfo.methodList[1] = null;
        selectedInfo.detailList = new Dictionary<string, string>
        {
            { "Range", range.ToString()},
            { "Cost", cost.ToString() },
            { "Damage", damage.ToString() },
            { "Details", "Poc Poc Tower" } 
        };
        bank.controlPanel.ReloadInterface();
    }
    private void TowerUpdateSniper()
    {
        cooldown = 5f;
        damage = 35f;
        ChangeRange(range * 2);
        selectedInfo.methodList[0] = null;
        selectedInfo.methodList[1] = null;
        selectedInfo.detailList = new Dictionary<string, string>
        {
            { "Range", range.ToString()},
            { "Cost", cost.ToString() },
            { "Damage", damage.ToString() },
            { "Details", "Poc Poc Tower" }
        };
        bank.controlPanel.ReloadInterface();
    }
    private void TowerBasic()
    {
        methodsList = new MethodsInfo[] {
            new MethodsInfo(TowerUpdateRapidFire,"Tower Update","Will upgrade this tower to new powers",true),
            new MethodsInfo(TowerUpdateSniper,"SniperTower","Long Range High Damage Slow Fire",true),
            null,
            new MethodsInfo(DestroyTower,"Destroy Tower","Will destroy this tower",true)};
        selectedInfo = new SelectedInfo(new Dictionary<string, string>
        {
            { "Range", range.ToString() },
            { "Cost", cost.ToString() },
            { "Damage", damage.ToString() },
            { "Details", "Poc Poc Tower" }
        },
        towerIcon,
        methodsList);
    }
    private void UnsubscribeTarget(object sender, EventArgs e)
    {

    }
}
