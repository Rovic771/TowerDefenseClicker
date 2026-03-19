using System;
using System.Collections;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] public float projectileSpeed = 15f;
    private Vector3 targetCoords;
    private Vector3 direction;
    private Rigidbody rb;

    IEnumerator LifeTime()
    {
        yield return new WaitForSeconds(5);
        Destroy(gameObject);
    }
    
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        StartCoroutine(LifeTime());
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
