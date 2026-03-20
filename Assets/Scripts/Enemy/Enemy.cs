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
    private float lifeMultiplicator;
    [HideInInspector] public float enemyLife;
    [HideInInspector] public float enemyDamage;
    
    void Start()
    {
        objectifs = GameManager.Instance.objectifs;
        currentDestination = objectifs[0].position;
        agent = GetComponent<NavMeshAgent>();
        enemyLife = enemyType.life * (0.8f + (0.2f * GameManager.Instance.currentRound));
        Debug.Log("Vie de l'ennemi" + enemyLife);
        enemyDamage = enemyType.damage;
        agent.speed = enemyType.speed;
        if (agent != null && objectifs != null)
        {
            agent.SetDestination(currentDestination);
        }
    }
    
    void Update()
    {
        if (Vector3.Distance(transform.position, currentDestination) <1f)
        {
            Debug.Log("bien arrivé");
            currentDestinationIndex += 1;
            currentDestination = objectifs[currentDestinationIndex % objectifs.Length].position;
            agent.SetDestination(currentDestination);

        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Base"))
        {
            BaseManager.Instance.DamageBase();
            Die(other.gameObject);
        }

        if (other.gameObject.layer == LayerMask.NameToLayer("Projectile"))
        {
            enemyLife -= 10;
            if (enemyLife <= 0)
            {
                Die(other.gameObject);
            }
            Debug.Log("EnemyLife" + enemyLife);
            Destroy(other.gameObject);
        }
    }
    
    
    void Die(GameObject other)
    {
        if (other.layer == LayerMask.NameToLayer("Projectile"))
        {
            RessourceManager.Instance.IncrementGold();
        }
        Destroy(gameObject);
    }
}
