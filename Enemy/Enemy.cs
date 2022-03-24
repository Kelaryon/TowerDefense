using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Enemy : MonoBehaviour,ISelectableInterface
{
    [SerializeField] public float maxHitpoints = 10;
    [SerializeField] [Range(0, 100)] float armor;
    [SerializeField] [Range(0f, 5)] float initspeed = 1f;
    [SerializeField] int goldReward = 25;
    [SerializeField] int goldPenality = 25;
    private readonly Dictionary<ScriptableBuff, TimedBuff> _buffs = new Dictionary<ScriptableBuff, TimedBuff>();
    private string hpString;
    public Sprite icon;
    Bank bank;
    void Start()
    {
        bank = FindObjectOfType<Bank>();
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
        if(bank == null) { return; }
        bank.Deposit(goldReward);
    }
    //Remove gold from Bank
    public void StealdGold()
    {
        if (bank == null) { return; }
        bank.Withdraw(goldPenality);
    }
    //Slow debuff function
    public float SlowDebuff(float initspeed)
    {
        return (initspeed / 2);
    }
    //Information retrival function
    public Dictionary<string, string> GetInfo()
    {
        Dictionary<string, string> detailList = new Dictionary<string, string>
        {
            { "HP", hpString},
            { "Speed", initspeed.ToString() },
            { "Armor", armor.ToString() },
            { "FlavourText","Just a basic enemy" }
        };
        return detailList;
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
    public void AddDebuff(TimedBuff buff)
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
}
