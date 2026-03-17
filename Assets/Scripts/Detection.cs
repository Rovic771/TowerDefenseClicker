using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Detection : MonoBehaviour
{
    [SerializeField] private Tower _towerSystem;
    [SerializeField] private Projectile _projectile;
    public Queue<GameObject> listEnemy = new Queue<GameObject>();

    private IEnumerator ShootCycle()
    {
        yield return new WaitForSeconds(1);
        if (listEnemy.Count != 0)
        {
            GetTarget(listEnemy); 
        }
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            listEnemy.Enqueue(other.gameObject);
            GetTarget(listEnemy);
        }
    }

    private void GetTarget(Queue<GameObject> _enemy)
    {
        NavMeshAgent Enemy = _enemy.Peek().GetComponent<NavMeshAgent>();
        float distance = Vector3.Distance(transform.position, _enemy.Peek().transform.position);
        float flyTime = distance / _projectile.projectileSpeed;
        Vector3 futurPoint = _enemy.Peek().transform.position + (Enemy.velocity * flyTime);
        _towerSystem.Shoot(futurPoint);
        ShootCycle();
    }

    public void DequeuEnemy()
    {
        listEnemy.Dequeue();
    }
}
