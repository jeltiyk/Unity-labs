using UnityEngine;

[RequireComponent(typeof(Player))]
public class EquipmentController : MonoBehaviour
{
    [SerializeField] private EquipmentUIController equipmentUIController;
    private EquipmentInventory _equipmentInventory;
    private Player _player;

    public EquipmentInventory Equipments => _equipmentInventory;
    
    public void Initialize()
    {
        _player = GetComponent<Player>();
        equipmentUIController.Initialize();
        _equipmentInventory = new EquipmentInventory(_player, _player.InventoryController.Inventory, equipmentUIController);
    }
}