using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereTower : Tower
{
    private Dictionary<string, string> detailList;
    int spheresNumber =0;
    [SerializeField] Transform sphere;

    public void Reset()
    {
        damage = 1.5f;
    }
    private void Start()
    {
        StartCoroutine(SpawnSpheres());
        //line = gameObject.GetComponent<LineRenderer>();

        //line.SetVertexCount(10 + 1);
        //line.useWorldSpace = false;
        //CreateLine();
        //line.enabled = false;
    }
    IEnumerator SpawnSpheres()
    {
        while (true)
        {
            if (spheresNumber < 3)
            {
                Transform projectile = Instantiate(sphere, transform.position + new Vector3(2.5f, 3.25f, 2.5f), Quaternion.identity);
                projectile.GetComponent<ProjectileScript>().Setup(transform.position,this);
                spheresNumber++;
            }
        yield return new WaitForSeconds(4);
        }
    }
    public void ReduceSphereNumber()
    {
        spheresNumber--;
    }

    public override Dictionary<string,string> GetInfo()
    {
        detailList = new Dictionary<string, string>
        {
            { "Range", range.ToString() },
            { "Cost", cost.ToString() },
            { "Damage", damage.ToString() }
        };
        return detailList;
    }
}
