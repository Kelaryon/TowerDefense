using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoomerangTowerScript : Tower
{
    private Dictionary<string, string> detailList;
    [SerializeField] BoomerangScript projectile;
    float timerBullet = 0;
    float cooldown = 5;
    // Start is called before the first frame update
    // Update is called once per frame

    public void Reset()
    {
        damage = 2.0f;
    }
    void Update()
    {
        Transform firstTarget = TargetLocatorPrime.FindClosestTarget(transform.position);
        if (firstTarget != null)
        {
            float targetDisance = Vector3.Distance(transform.position, firstTarget.position);
            if (targetDisance < range && timerBullet <= 0)
            {
                BoomerangScript activeProjectile = Instantiate(projectile, transform.position, Quaternion.identity);
                //activeProjectile.GetComponent<BoomerangScript>().Setup(firstTarget,damage);
                activeProjectile.Setup(firstTarget, damage);
                timerBullet = cooldown;

            }
            if (timerBullet > 0)
            {
                timerBullet -= Time.deltaTime;
            }
        }
    }
    public override Dictionary<string, string> GetInfo()
    {
        detailList = new Dictionary<string, string>
        {
            { "Range", range.ToString() },
            { "Cost", cost.ToString() },
            { "Damage", damage.ToString() },
            { "Details", "Boomerang goes brrr" }
        };
        return detailList;
    }
}
