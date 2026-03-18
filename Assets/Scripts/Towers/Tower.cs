using System;
using System.Numerics;
using UnityEngine;
using Quaternion = UnityEngine.Quaternion;
using Vector3 = UnityEngine.Vector3;

public abstract class Tower : MonoBehaviour
{
    [SerializeField] private GameObject projectile;
    [SerializeField] public TowerData _towerType;
    [HideInInspector] public float life;
    [HideInInspector] public float damage;
    [HideInInspector] public int level;
    
    public virtual void Shoot(Vector3 target)
    {
        GameObject newProjectile = Instantiate(projectile, gameObject.transform.position, Quaternion.Euler(90,0,0));
        Projectile scriptNewProjectile = newProjectile.GetComponent<Projectile>();
        scriptNewProjectile.ShootProjectile(target);
    }
}
