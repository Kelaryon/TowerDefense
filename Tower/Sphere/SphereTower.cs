using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereTower : Tower
{
    private Dictionary<string, string> detailList;
    int spheresNumber =0;
    [SerializeField] SphereProjectileScript sphere;
    List<SphereProjectileScript> sphereList;
    public float cooldown = 4f;

    private void Start()
    {
        sphereList = new List<SphereProjectileScript>();
    }
    //IEnumerator SpawnSpheres()
    //{
    //    while (true)
    //    {
    //        if (spheresNumber < 3)
    //        {
    //            SphereProjectileScript projectile = Instantiate(sphere, transform.position + new Vector3(2.5f, 3.25f, 2.5f), Quaternion.identity);
    //            projectile.GetComponent<SphereProjectileScript>().Setup(transform.position,this);
    //            spheresNumber++;
    //        }
    //    yield return new WaitForSeconds(4);
    //    }
    //}
    private void Update()
    {
        TargetingAndCooldown();
    }
    public override SelectedInfo GetInfo()
    {
        detailList = new Dictionary<string, string>
        {
            { "Range", range.ToString() },
            { "Cost", cost.ToString() },
            { "Damage", damage.ToString() }
        };
        return new SelectedInfo(detailList, icon);
    }
    protected override void Attack()
    {
        for(int i = spheresNumber-1; i>=0;i--)
        {
            sphereList[i].SetTracking(firstTarget);
            sphereList.RemoveAt(i);
        }
        spheresNumber -= spheresNumber;
    }
    protected override void TargetingAndCooldown()
    {
        if (spheresNumber < 3) {
            if (timerBullet > 0)
            {
                timerBullet -= Time.deltaTime;
            }
            else
            {
                SetCooldown(cooldown);
                spheresNumber++;
                SphereProjectileScript sphereCreate = Instantiate(sphere, this.transform.position + new Vector3(2.5f, 3.25f, 2.5f), Quaternion.identity);
                sphereCreate.Setup(this.transform.position, this);
                sphereList.Add(sphereCreate);
            }
        }
        if (timerSearch > 0 && spheresNumber > 0)
        {
            timerSearch -= Time.deltaTime;
        }
        else
        {
            timerSearch = 0.25f;
            Target();
        }
    }
}
