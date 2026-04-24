using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHeal : ScriptableEffect
{
    public override void Activate(Enemy enemy)
    {
        enemy.Heal(effect);
    }
}
