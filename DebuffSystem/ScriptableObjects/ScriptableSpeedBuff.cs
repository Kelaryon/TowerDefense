using UnityEngine;

namespace ScriptableObjects
{
    [CreateAssetMenu(menuName = "Debuffs/SpeedBuff")]
    public class ScriptableSpeedBuff : ScriptableBuff
    {
        public float speedDecrese;
        public override TimedBuff InitializeBuff(GameObject obj)
        {
            return new TimedSpeedBuff(this, obj);
        }
    }
}
