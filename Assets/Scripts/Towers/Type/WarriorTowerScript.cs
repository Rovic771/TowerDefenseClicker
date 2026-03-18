using UnityEngine;

public class WarriorTowerScript : Tower
{
    [SerializeField] private WarriorTowerData _warriorData;
    private int epeesize;
    void Start()
    {
        epeesize = _warriorData.tailleEpee;
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
    }
}
