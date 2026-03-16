using System;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    
    [SerializeField] private Transform[] objectifs; 
    [SerializeField] public EnemyData _data; 
    private NavMeshAgent agent;
    private Vector3 currentDestination;
    private int currentDestinationIndex;
    
    void Start()
    {
        currentDestination = objectifs[0].position;
        agent = GetComponent<NavMeshAgent>();
        agent.speed = _data.speed;
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
        Debug.Log("collision detécté");
        if (other.gameObject.layer == LayerMask.NameToLayer("Base"))
        {
            Debug.Log("mort");
            Die();
        }
    }

    void Die()
    {
        Destroy(gameObject);
    }
}
