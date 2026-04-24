
using UnityEngine;

public abstract class ScriptableEffect : ScriptableObject {
    protected float effect;
    public abstract void Activate(Enemy enemy);
}
