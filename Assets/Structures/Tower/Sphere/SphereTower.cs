using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereTower : Tower
{
    private Dictionary<string, string> detailList;
    [SerializeField] SphereProjectileScript sphere;
    Queue<SphereProjectileScript> sphereList;
    List<SphereProjectileScript> sphereActiveList;
    public float cooldown = 4f;

    private void Start()
    {
        sphereList = new Queue<SphereProjectileScript>();
        sphereActiveList = new List<SphereProjectileScript>();
        SphereSetup();
    }
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
        return new SelectedInfo(detailList, towerIcon);
    }
    protected override void Attack()
    {
        //MAIN PROBLEM
        for(int i = sphereActiveList.Count-1; i>=0; i--)
        {
            SphereProjectileScript sp = sphereActiveList[i];
            sp.SetTracking(firstTarget);
        }
        sphereActiveList.Clear();
    }
    protected override void TargetingAndCooldown()
    {
        if (sphereList.Count > 0) {
            if (timerBullet > 0 && sphereActiveList.Count <3)
            {
                timerBullet -= Time.deltaTime;
            }
            else
            {
                Initiate();
                SetCooldown(cooldown);
            }
        }
        if (timerSearch > 0 && sphereActiveList.Count > 0)
        {
            timerSearch -= Time.deltaTime;
        }
        else
        {
            timerSearch = 0.25f;
            Target();
        }
    }
    public void SphereSetup()
    {
        for (int i = 0; i < 3; i++)
        {
            SphereProjectileScript sp = Instantiate(sphere, this.transform.position + new Vector3(2.5f, 3.25f, 2.5f), Quaternion.identity);
            sp.Setup(this);
            sp.Deactivate();
            //AddToQueue(sp);
        }
    }
    public void AddToQueue(SphereProjectileScript sp)
    {
        sphereList.Enqueue(sp);
    }
    private void Initiate()
    {
        SphereProjectileScript sp = sphereList.Dequeue();
        sphereActiveList.Add(sp);
        sp.ActivateSP();
    }
    public override void DestroyTower()
    {
        //Utils.DispalyList<SphereProjectileScript>(sphereList);
        foreach(SphereProjectileScript sp in sphereActiveList)
        {
            Destroy(sp.gameObject);
        }
        foreach(SphereProjectileScript sp in sphereList)
        {
            Destroy(sp.gameObject);
        }
        base.DestroyTower();
    }
}
