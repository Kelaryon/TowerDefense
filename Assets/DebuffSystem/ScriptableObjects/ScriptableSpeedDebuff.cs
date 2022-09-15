using UnityEngine;

namespace ScriptableObjects
{
    [CreateAssetMenu(menuName = "Debuffs/SpeedDebuff")]
    public class ScriptableSpeedDebuff : ScriptableStatusEffect
    {
        public float speedDecreseMultiplier;
        public override TimedStatusEffect InitializeBuff(GameObject obj)
        {
            return new TimedSpeedDebuff(this, obj);
        }
    }
}
