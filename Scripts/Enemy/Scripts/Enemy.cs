using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using System;
public class Enemy : MonoBehaviour
{
    private static readonly int TRDie = Animator.StringToHash("trDie");

    //It can be replaced with Inheritance classes
    [SerializeField] private string eType;
    [SerializeField] public float maxHitPoints;
    [SerializeField] [Range(0, 100)] private float armor;
    [SerializeField] [Range(0f, 5)] private float initSpeed = 1f;
    [SerializeField] private int goldPenality = 25;
    [SerializeField] private Image healthBar;
    public Animator mobAnimator;
    private readonly Dictionary<ScriptableStatusEffect, TimedStatusEffect> buffs = new();

    //COMPONENTS
    private EnemyHealth enemyHealth;
    private EnemyMover enemyMover;

    private bool updateCoroutine;
    private Controller bank;
    private EnemyManager myEnemyManager;
    public event EventHandler DeathEvent;
    private Collider enemyCollider;

    private void Awake()
    {
        TryGetComponent(out enemyMover);
        TryGetComponent(out enemyHealth);
        TryGetComponent(out enemyCollider);
    }
    public string GetEnemyType()
    {
        return eType;
    }
    public void InitializeEnemy(Controller bank, EnemyManager enemyManager, Waypoint[] waypoints)
    {
        this.bank = bank;
        myEnemyManager = enemyManager;
        enemyMover?.SetMovementComponent(waypoints, initSpeed);
    }
    //Enemy activation adn Deactivation
    public void EnemyDeactivate()
    {
        RemoveEnemy();
        gameObject.SetActive(false);
        myEnemyManager.AddToPool(this);
    }

    private void RemoveEnemy()
    {
        myEnemyManager.RemoveEnemy(this);
    }
    public void EnemyActivate(bool targetType)
    {
        if (targetType)
        {
            myEnemyManager.AddEnemy(this);
        }
        this.gameObject.SetActive(true);
        enemyMover?.ActivateMovement(FinishPath);
        enemyCollider.enabled = true;
        enemyHealth.SetHp(maxHitPoints);
        healthBar.fillAmount = 1;
    }

    private void StealGold()
    {
        bank?.Withdraw(goldPenality);
    }
    #region Buff/Debuff System
    public void AddDebuff(TimedStatusEffect buff)
    {
        if (buffs.TryGetValue(buff.Buff, out var currentBuff))
        {
            currentBuff.Activate();
        }
        else
        {
            buffs.Add(buff.Buff, buff);
            buff.Activate();
            if (updateCoroutine) return;
            StartCoroutine(BuffUpdate());
            updateCoroutine = true;
        }
    }

    private IEnumerator BuffUpdate()
    {
        foreach (var buff in buffs.Values.ToList())
        {
            buff.Tick(Time.deltaTime);
            if (!buff.isFinished) continue;
            buffs.Remove(buff.Buff);
            if (buffs.Values.ToList() == null)
            {
                updateCoroutine = false;
                yield break;
            }
        }
        yield return null;
    }
    #endregion

    private void PlayDeathAnimation()
    {
        mobAnimator?.SetTrigger(TRDie);
    }
    //Path Methods
    private void SetIsFinished()
    {
        enemyMover?.StopCoroutine();
    }
    private void FinishPath()
    {
        StealGold();
        EnemyDeactivate();
    }

    //Damage Methods
    public void IncDamage(float damage)
    {
        enemyHealth.IncDamage(damage,armor,OnDeath);
        HealthBarUpdate();
    }
    public void Heal(float heal)
    {
        enemyHealth.Heal(Mathf.Clamp(enemyHealth.GetHP()+heal,0,maxHitPoints));
    }
    private void OnDeath()
    {
        enemyCollider.enabled = false;
        DeathEvent?.Invoke(this, EventArgs.Empty);
        if (mobAnimator is not null)
        {
            RemoveEnemy();
            SetIsFinished();
            PlayDeathAnimation();

            //Modification Here;
        }
        else
        {
            EnemyDeactivate();
        }
    }

    private void HealthBarUpdate()
    {
        healthBar.fillAmount = enemyHealth.GetHP()/maxHitPoints;
    }

    public float GetTravelDistance()
    {
        return enemyMover.GetTravelDistance();
    }
}