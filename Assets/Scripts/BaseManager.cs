using System;
using TMPro;
using UnityEngine;

public class BaseManager : MonoBehaviour
{ 
    public float baseLife = 100;
    [SerializeField] private EnemyData dataEnemy;
    [SerializeField] private TextMeshProUGUI lifeText;
    public static BaseManager Instance;

    private void Start()
    {
        if(Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
        lifeText.text = baseLife.ToString();
    }

    public void DamageBase()
    {
        baseLife -= dataEnemy.damage;
        lifeText.text = baseLife.ToString();
        if (baseLife <= 0)
        {
            GameManager.Instance.GameOver();
        }
    }
}
