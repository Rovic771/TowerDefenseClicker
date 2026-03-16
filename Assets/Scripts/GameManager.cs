using System.Collections;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject enemyGourdin;
    [SerializeField] private Transform spawnEnemy;
    
    private IEnumerator SpawnEnemy()
    {
        yield return new WaitForSeconds(5);
        Instantiate(enemyGourdin, spawnEnemy);
        SpawnEnemy();
    }
    
    void Start()
    {
        
    }
    
    void Update()
    {
        
    }
}
