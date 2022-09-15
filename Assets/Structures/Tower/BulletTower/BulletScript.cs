using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    Transform target;
    [SerializeField] float bulletSpeed;
    public ScriptableStatusEffect buff;
    float damage;
    //private Enemy currentEnemy;
    //Used to instantiate the bullet from the tower 
    public void Setup(Transform target, float damage, ScriptableStatusEffect buff)
    {
        this.target = target;
        this.damage = damage;
        this.buff = buff;
    }
    private void Update()
    {
        Attack(target);
    }
    void Attack(Transform target)
    {
        transform.position = Vector3.MoveTowards(transform.position, this.target.position + new Vector3(0, 3.5f, 0), bulletSpeed * Time.deltaTime);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            Enemy currentEnemy = other.GetComponent<Enemy>();
            if (buff != null)
            {
                currentEnemy.AddDebuff(buff.InitializeBuff(currentEnemy.gameObject));
            }
            currentEnemy.IncDamage(damage);
            Destroy(this.gameObject);
        }
    }
}
