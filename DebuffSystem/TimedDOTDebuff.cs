using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimedDOTDebuff : TimedBuff
{
    private readonly EnemyHealth enemyHealth;

    public TimedDOTDebuff(ScriptableBuff buff, GameObject obj) : base(buff, obj)
    {
        enemyHealth = obj.GetComponent<EnemyHealth>();
    }

    public override void End()
    {
        throw new System.NotImplementedException();
    }

    protected override void ApplyEffect()
    {
        StartCoroutine(DotDamage());
    }
    IEnumerator DotDamage()
    {
        while (Duration > 0)
        {
            //Static Damage value
            enemyHealth.IncDamage(0.75f);
            yield return new WaitForSeconds(1f);
        }
    }
}
