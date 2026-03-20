using System.Collections;
using JetBrains.Annotations;
using TMPro;
using Unity.VectorGraphics;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] public Transform[] objectifs;
    [SerializeField] private GameObject warrior;
    [SerializeField] private GameObject frondeur;
    [SerializeField] private GameObject tank;
    [SerializeField] private int delaySpawn = 1;
    [SerializeField] private TextMeshProUGUI roundTexte;
    [SerializeField] private GameObject gameOverMenu;
    private SystemGridPlacement systemGridPlacement;
    public static GameManager Instance;
    public float maxEnemyRound = 5;
    public int enemySpawned;
    public int currentRound = 1;
    private int nbTank;
    private int nbFrondeur;
    private int nbWarrior;
    
    
    
    private IEnumerator DelaySpawn()
    {
        yield return new WaitForSeconds(delaySpawn);
        SpawnEnemy();
    }

    private IEnumerator DelayBetweenRound()
    {
        yield return new WaitForSeconds(10);
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
        GenerationTypeEnemyNumber(Mathf.RoundToInt(maxEnemyRound));
        systemGridPlacement = GetComponent<SystemGridPlacement>();
    }

    private void LaunchRound()
    {
        currentRound += 1;
        roundTexte.text = "Round : " + currentRound.ToString();
        maxEnemyRound *= (1 + 50f / 100f);
        maxEnemyRound = Mathf.RoundToInt(maxEnemyRound);
        GenerationTypeEnemyNumber(Mathf.RoundToInt(maxEnemyRound));
    }

    private string TypeEnemyChoice()
    {
        int typeEnemy = Random.Range(0, 3);
        switch (typeEnemy)
        {
            case 0 : return "Tank";
            case 1: return "Frondeur";
            case 2: return "Warrior";
        }
        return null;
    }

    private void GenerationTypeEnemyNumber(int _enemyToGenerate)
    {
        Debug.Log(_enemyToGenerate);
        if (_enemyToGenerate <= 0)
        {
            SpawnEnemy();
            return;
        }
        int enemyToGenerate = _enemyToGenerate;
        int numberG = Random.Range(1, enemyToGenerate + 1);
        enemyToGenerate -= numberG;
        string typeEnemyGenerate = TypeEnemyChoice();
        if (typeEnemyGenerate == "Tank")
        {
            nbTank += numberG;
            Debug.Log("Tank : " + nbTank);
            GenerationTypeEnemyNumber(enemyToGenerate);
        }
        else if (typeEnemyGenerate == "Frondeur")
        {
            nbFrondeur += numberG;
            Debug.Log("nbFrondeur : " + nbFrondeur);
            GenerationTypeEnemyNumber(enemyToGenerate);
        }
        else if (typeEnemyGenerate == "Warrior")
        {
            nbWarrior += numberG;
            Debug.Log("nbWarrior : " + nbWarrior);
            GenerationTypeEnemyNumber(enemyToGenerate);
        }
        

    }
    
    public void SpawnEnemy()
    {
        if (enemySpawned < maxEnemyRound)
        {
            GameObject typeEnemy = null;
            int totalRestant = nbFrondeur + nbTank + nbWarrior;
            if (totalRestant <= 0) 
            {
                StartCoroutine(DelayBetweenRound());
                return;
            }
            
            int tirage = Random.Range(0, totalRestant);
            
            if (tirage < nbFrondeur)
            {
                typeEnemy = frondeur;
                nbFrondeur -= 1;
            }
            else if (tirage < nbFrondeur + nbTank)
            {
                typeEnemy = tank;
                nbTank -= 1;
            }
            else
            {
                typeEnemy = warrior;
                nbWarrior -= 1;
            }
            Instantiate(typeEnemy, typeEnemy.transform.position, Quaternion.identity);
            enemySpawned += 1;
            
            StartCoroutine(DelaySpawn());  
        }
        else
        {
            StartCoroutine(DelayBetweenRound());
        }
    }

    public void GameOver()
    {
        gameOverMenu.SetActive(!gameOverMenu.activeSelf);
        Time.timeScale = 0;
    }

    public void RestartButton()
    {
        SceneManager.LoadScene(1);
    }

    public void QuitButton()
    {
        Application.Quit();
    }
}
