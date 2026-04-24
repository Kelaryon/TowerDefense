using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class SphereTowerV2 : Tower
{
    private Dictionary<string, string> detailList;
    public float cooldown = 4f;

    private void Update()
    {
        TargetingAndCooldown();
    }
    public override SelectedInfo GetInfo()
    {
        detailList = new Dictionary<string, string>
        {
            { "Range", range.ToString() },
            { "Cost", cost.ToString() },
            { "Damage", damage.ToString() }
        };
        return new SelectedInfo(detailList, towerIcon,null);
    }
    protected override void Attack()
    {
    }
    protected override void TargetingAndCooldown()
    {
    }
}
