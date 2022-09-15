using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    //It can be replaced with Inheritance classes
    [SerializeField] private string eType;
    [SerializeField] public float maxHitpoints;
    [SerializeField] [Range(0, 100)] float armor;
    [SerializeField] [Range(0f, 5)] float initspeed = 1f;
    [SerializeField] int goldPenality = 25;
    public Animator mobAnimator;
    private readonly Dictionary<ScriptableStatusEffect, TimedStatusEffect> _buffs = new Dictionary<ScriptableStatusEffect, TimedStatusEffect>();
    private EnemyHealth enemyHealth;
    private EnemyMover enemyMover;
    private bool updateCoroutine = false;
    private Bank bank;
    private EnemyManager myEnemyManager;

    private void Awake()
    {
        enemyMover = this.GetComponent<EnemyMover>();
        enemyHealth = this.GetComponent<EnemyHealth>();
    }
    public string GetEnemyType()
    {
        return eType;
    }

    public void InitializeEnemy(Bank bank, EnemyManager enemyManager, Waypoint[] waypoints)
    {

        this.bank = bank;
        myEnemyManager = enemyManager;
        enemyMover.SetMovementComponent(waypoints,initspeed);
    }
    //Enemy activationa dn Deactivation
    public void EnemyDeactivate()
    {
        RemoveEnemy();
        gameObject.SetActive(false);
        myEnemyManager.AddToPool(this);
    }
    public void RemoveEnemy()
    {
        myEnemyManager.RemoveEnemy(this);
    }
    public void EnemyActivate()
    {
        myEnemyManager.AddEnemy(this);
        this.gameObject.SetActive(true);
        enemyMover.ActivateMovemnet(FinishPath);
    }
    public void StealdGold()
    {
        if (bank == null) { return; }
        bank.Withdraw(goldPenality);
    }
    //void Update()
    //{
    //    foreach (var buff in _buffs.Values.ToList())
    //    {
    //        buff.Tick(Time.deltaTime);
    //        if (buff.IsFinished)
    //        {
    //            _buffs.Remove(buff.Buff);
    //        }
    //    }
    //}

    //Old path end function

    //Returns initial speed;
    //public float GetInitSpeed()
    //{
    //    return initspeed;
    //}
    ////Returns armor
    //public float GetArmor()
    //{
    //    return armor;
    //}
    #region Buff/Debuff System
    public void AddDebuff(TimedStatusEffect buff)
    {
        if (_buffs.ContainsKey(buff.Buff))
        {
            _buffs[buff.Buff].Activate();
        }
        else
        {
            _buffs.Add(buff.Buff, buff);
            buff.Activate();
            if(updateCoroutine == false)
            {
                StartCoroutine(BuffUpdate());
                updateCoroutine = true;
            }
        }
    }
    IEnumerator BuffUpdate()
    {
        foreach (var buff in _buffs.Values.ToList())
        {
            buff.Tick(Time.deltaTime);
            if (buff.IsFinished)
            {
                _buffs.Remove(buff.Buff);
                if (_buffs.Values.ToList() == null)
                {
                    updateCoroutine = false;
                    yield break;
                }
            }
        }
        yield return null;
    }
    #endregion
    public void PlayDeathAnimation()
    {
        if (mobAnimator != null)
        {
            mobAnimator.SetTrigger("trDie");
        }
    }
    //Path Methods
    public void SetIsFinished()
    {
        enemyMover.StopCoroutine();
    }
    private void FinishPath()
    {
        StealdGold();
        EnemyDeactivate();
    }
    //Damage Methods
    public void IncDamage(float damage)
    {
        enemyHealth.IncDamage(damage,armor,OnDeath);
    }
    private void OnDeath()
    {
        if (mobAnimator != null)
        {
            RemoveEnemy();
            SetIsFinished();
            PlayDeathAnimation();
        }
        else
        {
            EnemyDeactivate();
        }
    }
}

//Add gold to Bank
//public void RewardGold()
//{
//    if (bank == null) { return; }
//    bank.Deposit(goldReward);
//}
//Remove gold from Bank
//Information retrival function !! no longer used, remember to reuse
//public SelectedInfo GetInfo()
//{
//    return new SelectedInfo(new Dictionary<string, string>
//    {
//        { "HP", hpString},
//        { "Speed", initspeed.ToString() },
//        { "Armor", armor.ToString() },
//        { "FlavourText","Just a basic enemy" }
//    }, icon);
//}
//public void ReturnHP(float currenthp)
//{
//    hpString = (maxHitpoints + "/" + currenthp);
//}
//Returns initial speed