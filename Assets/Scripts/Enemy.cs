using System;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    [SerializeField] public EnemyData _data;
    private Transform[] objectifs;
    private NavMeshAgent agent;
    private Vector3 currentDestination;
    private int currentDestinationIndex;
    private int enemyLife;
    private int enemyDamage;
    private Detection _detection;
    
    void Start()
    {
        _detection = GetComponent<Detection>();
        objectifs = GameManager.Instance.objectifs;
        currentDestination = objectifs[0].position;
        agent = GetComponent<NavMeshAgent>();
        agent.speed = _data.speed;
        enemyLife = _data.life;
        enemyDamage = _data.damage;
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
        _detection.DequeuEnemy();
    }
}
