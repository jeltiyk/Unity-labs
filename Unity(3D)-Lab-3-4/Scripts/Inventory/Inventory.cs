using System.Collections.Generic;
using Items.ScriptableObject;
using UnityEngine;

public class Inventory
{
    private const int Capacity = 48;

    private readonly Dictionary<InventoryCell, GameObject> inventoryItems;

    private readonly Player _player;
    private readonly GameObject _itemsArea;
    public readonly InventoryUIController InventoryUIController;
    
    public Inventory(Player player, GameObject itemsArea, InventoryUIController inventoryUIController)
    {
        inventoryItems = new Dictionary<InventoryCell, GameObject>(Capacity);

        _player = player;
        _itemsArea = itemsArea;
        InventoryUIController = inventoryUIController;
    }

    public bool AddItem(Item scriptableItem, GameObject physicalItem)
    {
        if (inventoryItems.Count == Capacity || scriptableItem == null || physicalItem == null) return false;

        InventoryCell inventoryCell = InventoryUIController.GetFreeCell();

        if (inventoryCell == null) return false;
        
        inventoryCell.AddItemToCell(scriptableItem);

        physicalItem.name = physicalItem.name.Replace("(Clone)", "");
        physicalItem.transform.SetParent(_player.transform);
        physicalItem.SetActive(false);
        
        inventoryItems.Add(inventoryCell, physicalItem);
        return true;
    }

    public bool RemoveItem(InventoryCell inventoryCell, bool dropped = true)
    {
        if (inventoryCell == null) return false;

        inventoryItems.TryGetValue(inventoryCell, out GameObject physicalItem);

        if (physicalItem == null) return false;

        if (dropped)
        {
            physicalItem.transform.SetParent(_itemsArea.transform);
            physicalItem.transform.position = _player.transform.position;
            physicalItem.SetActive(true);
        }
        
        return inventoryItems.Remove(inventoryCell);
    }

    public void GetItem(InventoryCell inventoryCell, out GameObject physicalItem)
    {
        inventoryItems.TryGetValue(inventoryCell, out physicalItem);
    }
}
