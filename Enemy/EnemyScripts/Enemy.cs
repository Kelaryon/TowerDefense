using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Enemy : MonoBehaviour, ISelectableInterface
{
    [SerializeField] public float maxHitpoints = 10;
    [SerializeField] [Range(0, 100)] float armor;
    [SerializeField] [Range(0f, 5)] float initspeed = 1f;
    [SerializeField] int goldReward = 25;
    [SerializeField] int goldPenality = 25;
    private bool isFinished = false;
    public Animator mobAnimator;
    private readonly Dictionary<ScriptableStatusEffect, TimedStatusEffect> _buffs = new Dictionary<ScriptableStatusEffect, TimedStatusEffect>();
    private string hpString;
    public Sprite icon;
    public EnemyHealth eHealth;
    Bank bank;

    public void InitializeEnemy(Bank bank)
    {
        this.bank = bank;
        bank.GetEnemyManager().AddEnemy(this);
    }
    void Update()
    {
        foreach (var buff in _buffs.Values.ToList())
        {
            buff.Tick(Time.deltaTime);
            if (buff.IsFinished)
            {
                _buffs.Remove(buff.Buff);
            }
        }
    }
    //Add gold to Bank
    public void RewardGold()
    {
        if (bank == null) { return; }
        bank.Deposit(goldReward);
    }
    //Remove gold from Bank
    public void StealdGold()
    {
        if (bank == null) { return; }
        bank.Withdraw(goldPenality);
    }
    //Information retrival function
    public SelectedInfo GetInfo()
    {
        return new SelectedInfo(new Dictionary<string, string>
        {
            { "HP", hpString},
            { "Speed", initspeed.ToString() },
            { "Armor", armor.ToString() },
            { "FlavourText","Just a basic enemy" }
        }, icon);
    }
    public void ReturnHP(float currenthp)
    {
        hpString = (maxHitpoints + "/" + currenthp);
    }
    //Returns initial speed
    public float GetInitSpeed()
    {
        return initspeed;
    }
    //Returns armor
    public float GetArmor()
    {
        return armor;
    }
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
        }
    }
    public void RemoveEnemy()
    {
        bank.GetEnemyManager().RemoveEnemy(this);
    }
    public void PlayAnimation()
    {
        if (mobAnimator != null)
        {
            mobAnimator.SetTrigger("trDie");
        }
    }
    public bool ReturnIsFinished()
    {
        return isFinished;
    }
    public void SetIsFinished()
    {
        isFinished = true;
    }
    public void DeleteEnemy()
    {
        RemoveEnemy();
        Destroy(this.gameObject);
    }
}
