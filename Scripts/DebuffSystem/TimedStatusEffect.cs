using UnityEngine;

public abstract class TimedStatusEffect 
{
    private float duration;
    protected int effectStacks;
    public ScriptableStatusEffect Buff { get; }
    protected readonly GameObject obj;
    public bool isFinished;

    protected TimedStatusEffect(ScriptableStatusEffect buff, GameObject obj)
    {
        Buff = buff;
        this.obj = obj;
    }

    public void Tick(float delta)
    {
        duration -= delta;
        if (!(duration <= 0)) return;
        End();
        isFinished = true;
    }
    /**
     * Activates buff or extends duration if ScriptableBuff has IsDurationStacked or IsEffectStacked set to true.
     */
    public void Activate()
    {
        if (Buff.isEffectStacked || duration <= 0)
        {
            ApplyEffect();
            effectStacks++;
        }      
        if (Buff.isDurationStacked || duration <= 0)
        {
            duration += Buff.duration;
        }
        else
        {
            duration = Buff.duration;
        }
    }
    protected abstract void ApplyEffect();
    protected abstract void End();
}