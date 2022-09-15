using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimedDOTDebuff : TimedStatusEffect
{
    private readonly Enemy enemy;
    private bool isActivated;

    public TimedDOTDebuff(ScriptableStatusEffect buff, GameObject obj) : base(buff, obj)
    {
        enemy = obj.GetComponent<Enemy>();
    }

    public override void End()
    {
        //throw new System.NotImplementedException();
        isActivated = false;
        Debug.Log("End");
    }

    protected override void ApplyEffect()
    {
        isActivated = true;
        enemy.StartCoroutine(DotDamage());
    }
    IEnumerator DotDamage()
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