using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimedDOTDebuff : TimedStatusEffect
{
    private readonly Enemy enemy;
    private bool isActivated;

    public TimedDOTDebuff(ScriptableStatusEffect buff, GameObject obj) : base(buff, obj)
    {
        obj.TryGetComponent(out enemy);
    }

    protected override void End()
    {
        isActivated = false;
    }

    protected override void ApplyEffect()
    {
        isActivated = true;
        enemy.StartCoroutine(DotDamage());
    }

    private IEnumerator DotDamage()
    {
        while (isActivated)
        {
            //Static Damage value
            //Debug.Log("HitS + duration:"+ Duration);
            enemy.IncDamage(2.75f);
            yield return new WaitForSeconds(1f);

        }
    }
}