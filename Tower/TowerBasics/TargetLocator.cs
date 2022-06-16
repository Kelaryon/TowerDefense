using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TargetLocator : TargetLocatorPrime
{
    [SerializeField] Transform weapon;
    [SerializeField] ParticleSystem projectileParticles;
    Transform target;
    Tower tower;

    private void Awake()
    {
        tower = GetComponent<Tower>();
    }
    void Update()
    {
        target = FindClosestTarget(transform.position);
        AimWeapon();
    }


    void AimWeapon()
    {
        if (target != null )
        {
            float targetDistance = Vector3.Distance(transform.position, target.position);
            weapon.LookAt(target);

            if (targetDistance < tower.range)
            {
                Attack(true);
            }
            else
            {
                Attack(false);
            }
        }
        else
        {
            Attack(false);
        }
    }
    void Attack(bool isActive)
    {
        var emissinModule = projectileParticles.emission;
        emissinModule.enabled = isActive;
    }
}
