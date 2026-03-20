using System;
using TMPro;
using UnityEngine;

public class RessourceManager : MonoBehaviour
{
    [SerializeField] private Ressources ressourcesData;
    [SerializeField] private TextMeshProUGUI nbGold;
    [SerializeField] private TextMeshProUGUI towerPriceText;
    public static RessourceManager Instance;
    public float towerPrice = 50;
    public int Gold;
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
        Gold = ressourcesData.Gold;
        nbGold.text = Gold.ToString();
        towerPriceText.text = towerPrice.ToString();
    }

    public void IncrementGold()
    {
        Gold += 15;
        nbGold.text = Gold.ToString();
    }

    public void DecrementGold()
    {
        Gold -= Mathf.RoundToInt(towerPrice);
        towerPrice *= 1 + 20f / 100f;
        nbGold.text = Gold.ToString();
        towerPriceText.text = Mathf.RoundToInt(towerPrice).ToString();
    }
}
