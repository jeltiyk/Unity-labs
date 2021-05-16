using System.Collections.Generic;
using Items.ScriptableObject;
using UnityEngine;

public class EquipmentInventory
{
    private const int EquipmentsCapacity = 7;

    private readonly Dictionary<InventoryCell, GameObject> _equipmentItems;

    private readonly Player _player;
    private readonly Inventory _inventory;
    private readonly EquipmentUIController _equipmentUIController;
    
    public EquipmentInventory(Player player, Inventory inventory, EquipmentUIController equipmentUIController)
    {
        _equipmentItems = new Dictionary<InventoryCell, GameObject>(EquipmentsCapacity);

        _player = player;
        _inventory = inventory;
        _equipmentUIController = equipmentUIController;
    }

    public bool EquipItem(Equipment equipment, InventoryCell activeCell)
    {
        if (equipment == null) return false;
        
        InventoryCell equipmentCell = _equipmentUIController.GetCellByType(equipment.EquipmentType);

        if (equipmentCell == null) return false;

        if (!UnEquip(equipmentCell)) return false;

        _inventory.GetItem(activeCell, out GameObject physicalItem);

        if (physicalItem == null) return false;

        equipmentCell.AddItemToCell(equipment);
        _equipmentItems.Add(equipmentCell, physicalItem);
        _inventory.RemoveItem(activeCell, false);
        
        return true;
    }

    public bool UnEquip(InventoryCell equipmentCell)
    {
        if (equipmentCell == null) return false;
        
        if (equipmentCell.Item == null) return true;
        
        InventoryCell inventoryCell = _inventory.InventoryUIController.GetFreeCell();

        if (inventoryCell == null) return false;

        if (!_inventory.AddItem(equipmentCell.Item, _equipmentItems[equipmentCell])) return false;
        
        equipmentCell.RemoveItemFromCell();

        return _equipmentItems.Remove(equipmentCell);
    }
}