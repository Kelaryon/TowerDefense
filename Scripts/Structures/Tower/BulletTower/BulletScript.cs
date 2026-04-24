using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class BulletScript : MonoBehaviour
{
    Enemy target;
    [SerializeField] float bulletSpeed;
    public ScriptableStatusEffect buff;
    float damage;
    //private Enemy currentEnemy;
    //Used to instantiate the bullet from the tower 
    public void Setup(Enemy target, float damage, ScriptableStatusEffect buff)
    {
        this.target = target;
        this.damage = damage;
        this.buff = buff;
        target.DeathEvent += this.DestroyProjectile;
    }
    private void Update()
    {
        Attack();
    }
    void Attack()
    {
        transform.position = Vector3.MoveTowards(transform.position, this.target.transform.position + new Vector3(0, 3.5f, 0), bulletSpeed * Time.deltaTime);
        NoNColliderImpact();
    }

    //Collider Impact
    //private void OnTriggerEnter(Collider other)
    //{
    //    if (other.CompareTag("Enemy"))
    //    {
    //        Enemy currentEnemy = other.GetComponent<Enemy>();
    //        if (buff != null)
    //        {
    //            currentEnemy.AddDebuff(buff.InitializeBuff(currentEnemy.gameObject));
    //        }
    //        currentEnemy.IncDamage(damage);
    //        Destroy(this.gameObject);
    //    }
    //}
    private void Impact()
    {
        Enemy currentEnemy = target.GetComponent<Enemy>();
        if (buff != null)
        {
            currentEnemy.AddDebuff(buff.InitializeBuff(target.gameObject));
        }
        currentEnemy.IncDamage(damage);
        //Destroy(this.gameObject);
    }

    private void NoNColliderImpact()
    {
        if (transform.position.x == target.transform.position.x && transform.position.z == target.transform.position.z)
        {
            Impact();
        }
    }
    private void DestroyProjectile(object sender, EventArgs e)
    {
        target.DeathEvent -= this.DestroyProjectile;
        Destroy(this.gameObject);
    }
}
