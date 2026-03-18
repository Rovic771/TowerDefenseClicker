using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;
using UnityEngine.AI;

public class Detection : MonoBehaviour
{
    [SerializeField] private Projectile _projectile;
    private Tower _towerSystem;
    public List<GameObject> listEnemy = new List<GameObject>();

    private IEnumerator ShootCycle()
    {
        yield return new WaitForSeconds(1);
        if (listEnemy.Count != 0)
        {
            GetTarget(listEnemy); 
        }
    }

    private void Awake()
    {
        _towerSystem = GetComponentInParent<Tower>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            listEnemy.Add(other.gameObject);
            if (listEnemy.Count == 1)
            {
                GetTarget(listEnemy); 
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            listEnemy.Remove(other.gameObject);
        }
    }

    private void GetTarget(List<GameObject> _enemy)
    {
        if (_enemy[0] != null)
        {
            NavMeshAgent enemy = _enemy[0].GetComponent<NavMeshAgent>();
            float distance = Vector3.Distance(transform.position, _enemy[0].transform.position);
            float flyTime = distance / _projectile.projectileSpeed;
            Vector3 futurPoint = _enemy[0].transform.position + (enemy.velocity * flyTime);
            _towerSystem.Shoot(futurPoint);
            StartCoroutine(ShootCycle()); 
        }
        else
        {
            _enemy.Remove(_enemy[0]);
            if (_enemy.Count != 0)
            {
                GetTarget(listEnemy);
            }
        }

    }
    
}
