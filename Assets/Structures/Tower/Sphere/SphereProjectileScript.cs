using System;
using UnityEngine;

public class SphereProjectileScript : MonoBehaviour
{
    [SerializeField] float speed;
    Vector3 initialPosition;
    SphereTower spTower;
    Enemy enemy;
    bool tracking = false;
    private Action currentAction;


    void Update()
    {
        currentAction();
    }
    public void Setup(SphereTower spTowerLink)
    {
        spTower = spTowerLink;
        initialPosition = this.transform.position;
        currentAction = Rotate;
    }
    void Rotate()
    {
        transform.RotateAround(spTower.transform.position, Vector3.up, 120 * Time.deltaTime);
    }
    void Attack()
    {
        try
        {
            transform.position = Vector3.MoveTowards(transform.position, enemy.transform.position + new Vector3(0, 3.5f, 0), speed * Time.deltaTime);
            speed += 0.1f;

            if (this.transform.position.x == enemy.transform.position.x && this.transform.position.z == enemy.transform.position.z)
            {
                if (spTower.GetStatusSelected() != null)
                {
                    enemy.AddDebuff(spTower.GetStatusSelected().InitializeBuff(enemy.gameObject));
                }
                enemy.IncDamage(50f);
                ResetSP();
            }

        }
        catch (MissingReferenceException) {
            ResetSP();
        }
    }
    public void SetTracking(Enemy enemy)
    {
        this.enemy = enemy;
        tracking = !tracking;
        currentAction = Attack;
    }
    public void ResetSP()
    {
        Deactivate();
        this.transform.position = initialPosition;
        tracking = false;
        currentAction = Rotate;
    }
    public void Deactivate()
    {
        this.gameObject.SetActive(false);
        spTower.AddToQueue(this);
    }
    public void ActivateSP()
    {
        this.gameObject.SetActive(true);
    }

}
