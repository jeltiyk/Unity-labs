using UnityEngine;

public class InventoryController : MonoBehaviour
{
    [SerializeField] private InventoryUIController inventoryUIController;
    [SerializeField] private GameObject itemsArea;
    private Player _player;

    public Inventory Inventory { get; private set; }

    public void Initialize()
    {
        _player = GetComponent<Player>();
        inventoryUIController.Initialize();
        Inventory = new Inventory(_player, itemsArea, inventoryUIController);
    }
}
