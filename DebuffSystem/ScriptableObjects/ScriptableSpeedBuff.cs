using UnityEngine;


namespace ScriptableObjects
{
    [CreateAssetMenu(menuName = "StatusEffects/SpeedBuff")]
    public class ScriptableSpeedBuff : ScriptableStatusEffect
    {
        public float speedDecreseMultiplier;
        public override TimedStatusEffect InitializeBuff(GameObject obj)
        {
            return new TimedSpeedDebuff(this, obj);
        }
    }
}
