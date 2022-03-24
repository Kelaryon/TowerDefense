
using ScriptableObjects;
using UnityEngine;

public class TimedSpeedBuff : TimedBuff
{
    private readonly EnemyMover enemyMover;

    public TimedSpeedBuff(ScriptableBuff buff, GameObject obj) : base(buff, obj)
    {
        //Getting MovementComponent, replace with your own implementation
        enemyMover = obj.GetComponent<EnemyMover>();
    }

    protected override void ApplyEffect()
    {
        //Add speed increase to MovementComponent
        ScriptableSpeedBuff speedBuff = (ScriptableSpeedBuff) Buff;
        enemyMover.SetSpeed(enemyMover.GetSpeed()/speedBuff.speedDecrese);
    }

    public override void End()
    {
        //Revert speed increase
        ScriptableSpeedBuff speedBuff = (ScriptableSpeedBuff) Buff;
        enemyMover.SetSpeed(enemyMover.GetSpeed() * speedBuff.speedDecrese);
        //EffectStacks = 0;
    }
}
