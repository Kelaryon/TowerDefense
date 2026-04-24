
using ScriptableObjects;
using UnityEngine;

public class TimedSpeedDebuff : TimedStatusEffect
{
    private readonly EnemyMover enemyMover;

    public TimedSpeedDebuff(ScriptableStatusEffect buff, GameObject obj) : base(buff, obj)
    {
        //Getting MovementComponent
        obj.TryGetComponent(out enemyMover);
    }

    protected override void ApplyEffect()
    {
        //Add speed increase to MovementComponent
        ScriptableSpeedDebuff speedBuff = (ScriptableSpeedDebuff) Buff;
        enemyMover?.SetSpeed(enemyMover.GetSpeed()*speedBuff.speedDecreseMultiplier);

    }

    protected override void End()
    {
        //Revert speed increase
        ScriptableSpeedDebuff speedBuff = (ScriptableSpeedDebuff) Buff;
        enemyMover?.SetSpeed(enemyMover.GetSpeed() / speedBuff.speedDecreseMultiplier);

        //EffectStacks = 0;
    }
}
