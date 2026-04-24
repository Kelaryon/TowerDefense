using UnityEngine;
using UnityEngine.Serialization;

public abstract class ScriptableStatusEffect : ScriptableObject
{

    /**
     * Time duration of the buff in seconds.
     */
    public float duration;

    /**
     * Duration is increased each time the buff is applied.
     */
    public bool isDurationStacked;

    /**
     * Effect value is increased each time the buff is applied.
     */
    public bool isEffectStacked;
    
    public abstract TimedStatusEffect InitializeBuff(GameObject obj);

}
