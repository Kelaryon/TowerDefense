using System;
using UnityEngine.UI;
using UnityEngine;


[RequireComponent(typeof(Enemy))] 
public class EnemyHealth : MonoBehaviour
{

    [Tooltip("Adds s-amount to max hitpoint when enemy dies")]
    //[SerializeField] int difficultyRamp = 1;
    private float currentHitpoints = 0;
    [SerializeField] private Image healthbar;
    public Enemy enemy;
    //Set current hitpoint to max hitpoints
    void OnEnable()
    {
        currentHitpoints = enemy.maxHitpoints;
    }
    public void IncDamage(float incDamage, float armor, Action Death)
    {
        currentHitpoints -= (incDamage-(incDamage*armor/100));
        HealthBarUpdate();
        if (currentHitpoints <= 0)
        {
            Death();
        }
    }
    public float GetHP()
    {
        return currentHitpoints;
    }
    void HealthBarUpdate()
    {
        healthbar.fillAmount = currentHitpoints / enemy.maxHitpoints;
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