using UnityEngine;

public class SystemGridPlacement : MonoBehaviour
{
    public float gridSize = 3;
    public GameObject towerToPlace;
    public LayerMask groundLayer; 
    
    // On ajoute le calque des tours pour que le radar sache quoi chercher
    public LayerMask towerLayer; 
    
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            PlaceTower();
        }
    }

    private void PlaceTower()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        Debug.DrawRay(ray.origin, ray.direction * 100f, Color.red, 2f);
        if (Physics.Raycast(ray, out hit, Mathf.Infinity, groundLayer)) 
        {
            Vector3 pointImpact = hit.point;
            float snappedX = Mathf.Round(pointImpact.x / gridSize) * gridSize;
            float snappedZ = Mathf.Round(pointImpact.z / gridSize) * gridSize;
            Vector3 positionFinale = new Vector3(snappedX, -3.5f, snappedZ);
            Collider[] collidersContact = Physics.OverlapSphere(positionFinale, 0.5f, towerLayer);
            if (collidersContact.Length == 0)
            {
                if (hit.collider.gameObject.layer == LayerMask.NameToLayer("Ground"))
                {
                    Instantiate(towerToPlace, positionFinale, Quaternion.identity);
                }
            }
            else
            {
                Debug.Log("collider touché :  " + collidersContact[0].gameObject.name);
            }
        }
    }
}
