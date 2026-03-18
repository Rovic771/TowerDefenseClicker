using UnityEngine;

public class FrondeurTowerScript : Tower
{
    
    void Start()
    {
        life = _towerType.towerLife;
        damage = _towerType.towerDamage;
        level = _towerType.towerLevel;
    }
    void Update()
    {
        
    }

    public override void Shoot(Vector3 target)
    {
        
    }
}
