using UnityEngine;

namespace ScriptableObjects
{
    [CreateAssetMenu(menuName = "Debuffs/DOTDebuff")]
    public class ScriptableDOTDebuff : ScriptableStatusEffect
    {
        public float damage;
        public override TimedStatusEffect InitializeBuff(GameObject obj)
        {
            return new TimedDOTDebuff(this, obj);
        }
    }
}
