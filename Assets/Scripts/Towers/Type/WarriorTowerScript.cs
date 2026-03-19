using UnityEngine;

public class WarriorTowerScript : Tower
{
    void Start()
    {
        life = _towerType.towerLife;
        damage = _towerType.towerDamage;
        level = _towerType.towerLevel;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public override void Shoot(Vector3 target)
    {
        Debug.Log("aaa");
    }
}
