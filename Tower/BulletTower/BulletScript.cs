using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    Transform firstTarget;
    [SerializeField] float bulletSpeed;
    public ScriptableBuff buff;
    private Enemy currentEnemy;
    //Used to instantiate the bullet from the tower 
    public void Setup(Transform firstTarget, float damage)
    {
        this.firstTarget = firstTarget;
    }
    private void Update()
    {
        Attack(firstTarget);
    }
    public void SetEnemy(Enemy enemy)
    {
        currentEnemy = enemy;
    }
    void Attack(Transform target)
    {
    transform.position = Vector3.MoveTowards(transform.position, firstTarget.position + new Vector3(0, 3.5f, 0), bulletSpeed * Time.deltaTime);
      //if (transform.position == firstTarget.position + new Vector3(0, 3.5f, 0))
      //  {
      //      Destroy(this);
      //  }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            Enemy buffy = other.GetComponent<Enemy>();
            buffy.AddDebuff(buff.InitializeBuff(buffy.gameObject));
            Destroy(this);
        }
    }
}
