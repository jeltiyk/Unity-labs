using UnityEngine;

public class InventoryController : MonoBehaviour
{
    [SerializeField] private GameObject sceneItemArea;
    [SerializeField] private GameObject inventoryUI;
    
    public Inventory Inventory { get; private set; }

    private void Start()
    {
        Inventory = new Inventory(transform, sceneItemArea, inventoryUI);
    }
}
