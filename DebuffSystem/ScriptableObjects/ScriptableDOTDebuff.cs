using UnityEngine;

namespace ScriptableObjects
{
    [CreateAssetMenu(menuName = "Debuffs/DOTDebuff")]
    public class ScriptableDOTDebuff : ScriptableBuff
    {
        public float damage;
        public override TimedBuff InitializeBuff(GameObject obj)
        {
            return new TimedDOTDebuff(this, obj);
        }
    }
}
