using ScriptableObjects;
using UnityEngine;

public class TimedSpeedBuff : TimedStatusEffect
{
    private readonly EnemyMover enemyMover;

    public TimedSpeedBuff(ScriptableStatusEffect buff, GameObject obj) : base(buff, obj)
    {
        //Getting MovementComponent
        enemyMover = obj.GetComponent<EnemyMover>();
    }

    protected override void ApplyEffect()
    {
        ////Add speed increase to MovementComponent
        ScriptableSpeedBuff speedBuff = (ScriptableSpeedBuff)Buff;
        enemyMover.SetSpeed(enemyMover.GetSpeed() / speedBuff.speedDecreseMultiplier);
    }

    public override void End()
    {
        ////Revert speed increase
        ScriptableSpeedDebuff speedBuff = (ScriptableSpeedDebuff)Buff;
        enemyMover.SetSpeed(enemyMover.GetSpeed() * speedBuff.speedDecreseMultiplier);
        //EffectStacks = 0;
    }


}
