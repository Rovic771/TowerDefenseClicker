using UnityEngine;

public class enemyWarrior : Enemy
{
    [SerializeField] private WarriorData _data2;
    private int speed;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        speed = _data.speed * _data2.speedMultiplicator;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
