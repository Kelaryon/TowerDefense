using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;


[RequireComponent(typeof(Enemy))] 
public class EnemyHealth : MonoBehaviour
{

    [Tooltip("Adds s-amount to max hitpoint when enemy dies")]
    //[SerializeField] int difficultyRamp = 1;
    private float currentHitpoints = 0;
    EnemyMover eMover;
    public Image healthbar;
    public Enemy enemy;
    //Set current hitpoint to max hitpoints
    void OnEnable()
    {
        currentHitpoints = enemy.maxHitpoints;
        enemy.ReturnHP(currentHitpoints);
    }
    //Healthbar update
    private void Update()
    {
        healthbar.fillAmount = currentHitpoints / enemy.maxHitpoints;
    }
    private void Start()
    {
        eMover = GetComponent<EnemyMover>();
    }
    private void OnParticleCollision(GameObject other)
    {
        IncDamage(1f);
        //if (other.CompareTag("Slow") && eMover.isActiveAndEnabled == true)
        //{
        //    StartCoroutine(SlowMode(1f,1f));
        //}
        //if (other.CompareTag("Poison"))
        //{
        //    PoisionApply();
        //}
    }
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
    public void IncDamage(float incDamage)
    {
        currentHitpoints -= (incDamage-(incDamage*enemy.GetArmor()/100));
        enemy.ReturnHP(currentHitpoints);
        if (currentHitpoints <= 0)
        {
            gameObject.SetActive(false);
            enemy.RewardGold();
        }
    }
    public float GetHP()
    {
        return currentHitpoints;
    }
}
