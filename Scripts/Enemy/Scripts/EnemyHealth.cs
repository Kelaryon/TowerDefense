using System;
using UnityEngine;


[RequireComponent(typeof(Enemy))] 
public class EnemyHealth : MonoBehaviour
{
    private float currentHitpoints = 0;
    //Set current hitpoint to max hitpoints
    public void IncDamage(float incDamage, float armor, Action KillEnemy)
    {
        currentHitpoints -= (incDamage-(incDamage*armor/100));
        if (currentHitpoints <= 0)
        {
            KillEnemy();
        }
    }
    public void Heal(float health)
    {
        currentHitpoints += health;
    }
    public float GetHP()
    {
        return currentHitpoints;
    }
    public void SetHp(float value)
    {
        currentHitpoints = value;
    }

}

//Healthbar update
//private void OnParticleCollision(GameObject other)
//{
//    IncDamage(1f);
//}
//Old Debuff Code

//IEnumerator SlowMode(float duration, float intensity)
//{
//    if (eMover.isActiveAndEnabled == true)
//    {
//        eMover.AddSlow();
//        yield return new WaitForSeconds(3);
//        eMover.RemoveSlow();
//    }
//}
//IEnumerator PoisonMode(float duration, float dot)
//{
//    isPoisoned = true;
//    for (int i = 0; i < duration; i++)
//    {
//        IncDamage(dot);
//        yield return new WaitForSeconds(1);
//    }
//    isPoisoned = false;
//}
//void PoisionApply()
//{
//    if (isPoisoned == false && isActiveAndEnabled == true)
//    {
//        StartCoroutine(PoisonMode(6f,0.5f));
//    }
//    else
//    {
//        StopCoroutine("PoisonMode");
//        StartCoroutine(PoisonMode(6f,0.5f));
//    }
//}
//public void StratCoroutine(IEnumerator dotCoroutine)
//{
//    StartCoroutine(dotCoroutine);
//}