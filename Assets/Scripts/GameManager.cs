using System.Collections;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] public Transform[] objectifs;
    [SerializeField] private GameObject enemyGourdin;
    [SerializeField] private int delaySpawn = 5;
    public static GameManager Instance;
    public float maxEnemyRound = 5;
    public int enemySpawned;
    public int currentRound = 1;
    
    
    
    private IEnumerator DelaySpawn()
    {
        yield return new WaitForSeconds(delaySpawn);
        SpawnEnemy();
    }

    private IEnumerator DelayBetweenRound()
    {
        Debug.Log("Fin de round");
        currentRound += 1;
        yield return new WaitForSeconds(10);
        Debug.Log("round "+ currentRound);
        enemySpawned = 0;
        LaunchRound();
    }
    
    void Start()
    {
        if(Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
        SpawnEnemy();
    }

    private void LaunchRound()
    {
        currentRound += 1;
        maxEnemyRound *= (1 + 50f / 100f);
        maxEnemyRound = Mathf.RoundToInt(maxEnemyRound);
        Debug.Log("MaxEnemyRound :" + maxEnemyRound);
        SpawnEnemy();
    }
    
    public void SpawnEnemy()
    {
        if (enemySpawned < maxEnemyRound)
        {
            Instantiate(enemyGourdin, enemyGourdin.transform.position, Quaternion.identity);
            enemySpawned += 1;
            Debug.Log("Enemy In game:" + enemySpawned);
            StartCoroutine(DelaySpawn());  
        }
        else
        {
            StartCoroutine(DelayBetweenRound());
        }
    }
}
