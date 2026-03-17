using System;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] public float projectileSpeed = 15f;
    private Vector3 targetCoords;
    private Vector3 direction;
    private Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    public void ShootProjectile(Vector3 target)
    {
        targetCoords = target;
        direction = (target - transform.position).normalized;
        transform.LookAt(target);
        if (rb != null)
        {
            rb.linearVelocity = direction * projectileSpeed;
        }
    }
}
