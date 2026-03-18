using System;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    [SerializeField] public EnemyData enemyType;
    private Transform[] objectifs;
    private NavMeshAgent agent;
    private Vector3 currentDestination;
    private int currentDestinationIndex;
    [HideInInspector] public float enemyLife;
    [HideInInspector] public float enemyDamage;
    
    void Start()
    {
        objectifs = GameManager.Instance.objectifs;
        currentDestination = objectifs[0].position;
        agent = GetComponent<NavMeshAgent>();
        agent.speed = enemyType.speed;
        if (agent != null && objectifs != null)
        {
            agent.SetDestination(currentDestination);
        }
    }
    
    void Update()
    {
        if (Vector3.Distance(transform.position, currentDestination) < 0.5f)
        {
            currentDestinationIndex += 1;
            currentDestination = objectifs[currentDestinationIndex % objectifs.Length].position;
            agent.SetDestination(currentDestination);

        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Base"))
        {
            Die();
        }

        if (other.gameObject.layer == LayerMask.NameToLayer("Projectile"))
        {
            enemyLife -= 35;
            if (enemyLife <= 0)
            {
                Die();
            }
            Debug.Log("EnemyLife" + enemyLife);
            Destroy(other.gameObject);
        }
    }

    void Die()
    {
        Destroy(gameObject);
    }
}
