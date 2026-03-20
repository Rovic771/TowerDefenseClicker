using UnityEngine;

public class SystemGridPlacement : MonoBehaviour
{
    public float gridSize = 3;
    public GameObject towerToPlace;     
    public GameObject previewPrefab;    
    public LayerMask groundLayer;
    public LayerMask obstaclesLayer; 
    private GameObject currentPreview;  
    private bool isPlacing = false;    
    private bool isHoveringGround = false; 
    
    void Update()
    {
        if (isPlacing && currentPreview != null)
        {
            UpdatePreviewPosition();
            
            if (Input.GetMouseButtonDown(0))
            {
                if (isHoveringGround)
                {
                    if (RessourceManager.Instance.Gold >= 50 && (RessourceManager.Instance.Gold - RessourceManager.Instance.towerPrice) >= 0)
                    {
                        PlaceTower(); 
                    }
                    else
                    {
                        CancelPlacement();
                    }
                }
            }
            
            if (Input.GetMouseButtonDown(1))
            {
                CancelPlacement();
            }
        }
    }
    
    public void StartPlacementMode()
    {
        if (currentPreview != null) 
        {
            Destroy(currentPreview);
        }
        currentPreview = Instantiate(previewPrefab);
        isPlacing = true;
    }
    
    private void UpdatePreviewPosition()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        
        if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, groundLayer))
        {
            isHoveringGround = true;
            currentPreview.SetActive(true); 
            
            Vector3 pointImpact = hit.point;
            float snappedX = Mathf.Round(pointImpact.x / gridSize) * gridSize;
            float snappedZ = Mathf.Round(pointImpact.z / gridSize) * gridSize;
            currentPreview.transform.position = new Vector3(snappedX, -3.5f, snappedZ);
        }
        else
        {
            isHoveringGround = false;
            currentPreview.SetActive(false); 
        }
    }
    
    private void PlaceTower()
    {
        Vector3 positionFinale = currentPreview.transform.position;
        Collider[] collidersContact = Physics.OverlapSphere(positionFinale, 0.5f, obstaclesLayer, QueryTriggerInteraction.Ignore);
        if (collidersContact.Length == 0)
        {
            Instantiate(towerToPlace, positionFinale, Quaternion.identity);
            isPlacing = false;
            RessourceManager.Instance.DecrementGold();
            Destroy(currentPreview);
        }
        else
        {
            Debug.Log("obstacle");
        }
    }
    
    private void CancelPlacement()
    {
        isPlacing = false;
        Destroy(currentPreview);
    }
}
