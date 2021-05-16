using System.Collections.Generic;
using Items.ScriptableObject;
using UnityEngine;

public class Inventory
{
    private const int InventoryCapacity = 48;

    private readonly Dictionary<InventoryCell, GameObject> _items;
    private readonly InventoryCell[] _inventoryCells;

    private readonly Transform _entityTransform;
    private readonly GameObject _itemsArea;

    private readonly GameObject _inventoryUI;
    public readonly UIController _uiController;
    
    public Inventory(Transform entityTransform, GameObject itemsArea, GameObject inventoryUI)
    {
        _items = new Dictionary<InventoryCell, GameObject>(InventoryCapacity);
        _inventoryCells = inventoryUI.GetComponentsInChildren<InventoryCell>(true);

        foreach (var cell in _inventoryCells)
            cell.Initialize(this);

        _entityTransform = entityTransform;
        _itemsArea = itemsArea;

        _inventoryUI = inventoryUI;
        _uiController = _inventoryUI.transform.root.GetComponent<UIController>();
    }

    /// <summary>
    /// Returns free inventory cell.
    /// </summary>
    /// <returns>Free inventory cell.</returns>
    public InventoryCell GetFreeCell()
    {
        foreach (var cell in _inventoryCells)
            if (cell.Item == null) return cell;

        return null;
    }
    /// <summary>
    /// Add item to player inventory. Returns value of bool type.
    /// </summary>
    /// <param name="scriptableItem">Existing scriptable item.</param>
    /// <param name="physicalItem">Existing physical item(game object with rigidbody).</param>
    /// <returns>Value of bool type.</returns>
    public bool AddItem(Item scriptableItem, GameObject physicalItem)
    {
        if (_items.Count == InventoryCapacity || scriptableItem == null || physicalItem == null) return false;

        InventoryCell freeCell = GetFreeCell();

        if (freeCell == null) return false;

        physicalItem.name = physicalItem.name.Replace("(Clone)", "");
        physicalItem.transform.SetParent(_entityTransform);
        physicalItem.SetActive(false);

        freeCell.AddItemToCell(scriptableItem);
        _items.Add(freeCell, physicalItem);
        return true;
    }

    /// <summary>
    /// Add item to player inventory active cell. Returns value of bool type.
    /// </summary>
    /// <param name="scriptableItem">Existing scriptable item.</param>
    /// <param name="physicalItem">Existing physical item(game object with rigidbody).</param>
    /// <param name="targetCell">Existing inventory cell.</param>
    /// <returns>Value of bool type.</returns>
    public bool AddItemToCell(Item scriptableItem, GameObject physicalItem, InventoryCell targetCell)
    {
        if (_items.Count == InventoryCapacity || scriptableItem == null || physicalItem == null ||
            targetCell == null) return false;
        
        if (_items.ContainsKey(targetCell)) return false; // if contains key then inventory contains item

        targetCell.AddItemToCell(scriptableItem);
        _items.Add(targetCell, physicalItem);

        return true;
    }

    /// <summary>
    /// Swap two items in player inventory. Returns value of bool type.
    /// </summary>
    /// <param name="firstInventoryCell">Existing inventory cell with item.</param>
    /// <param name="secondInventoryCell">Existing inventory cell with item.</param>
    /// <returns>Value of bool type.</returns>
    private bool Swap(InventoryCell firstInventoryCell, InventoryCell secondInventoryCell)
    {
        if (!_items.ContainsKey(firstInventoryCell) || !_items.ContainsKey(secondInventoryCell)) return false;
        
        Item scriptableItem = firstInventoryCell.Item;
        GameObject physicalItem = _items[firstInventoryCell];
        
        firstInventoryCell.RemoveItemFromCell();
        firstInventoryCell.AddItemToCell(secondInventoryCell.Item);
        
        secondInventoryCell.RemoveItemFromCell();
        secondInventoryCell.AddItemToCell(scriptableItem);
        
        _items[firstInventoryCell] = _items[secondInventoryCell];
        _items[secondInventoryCell] = physicalItem;
        
        return true;
    }
    
    /// <summary>
    /// Changes item position in player inventory. Returns value of bool type.
    /// </summary>
    /// <param name="fromCell">Existing inventory cell with item.</param>
    /// <param name="toCell">Existing inventory cell with item.</param>
    /// <returns>Returns value of bool type.</returns>
    public bool ChangeItemPosition(InventoryCell fromCell, InventoryCell toCell)
    {
        if (!_items.ContainsKey(fromCell)) return false;
        
        if (toCell.Item != null) return Swap(fromCell, toCell);
        
        toCell.AddItemToCell(fromCell.Item);
        _items[toCell] = _items[fromCell];

        return RemoveItem(fromCell);
    }

    /// <summary>
    /// Remove item from player inventory. Returns value of bool type.
    /// </summary>
    /// <param name="inventoryCell">Existing inventory cell with item.</param>
    /// <param name="isDrop">At "true" value items will drop from player inventory.</param>
    /// <returns></returns>
    public bool RemoveItem(InventoryCell inventoryCell, bool isDrop = false)
    {
        if (!_items.ContainsKey(inventoryCell)) return false;

        if (isDrop)
        {
            _items.TryGetValue(inventoryCell, out GameObject physicalItem);

            if (physicalItem == null) return false;
            
            GameObject physicalItemGameObject = physicalItem.gameObject;
        
            physicalItemGameObject.name = physicalItemGameObject.name.Replace("(Clone)", "");
            physicalItemGameObject.SetActive(true);
            physicalItem.transform.SetParent(_itemsArea.transform);
            
            // physicalItem.transform.SetParent(_itemsArea.transform);
            // physicalItem.transform.position = _entityTransform.position; // temporary solution
            // physicalItem.SetActive(true);
        }

        if (!_items.Remove(inventoryCell)) return false;
        
        inventoryCell.RemoveItemFromCell();
        return true;
    }
    
    /// <summary>
    /// Returns physical item.
    /// </summary>
    /// <param name="inventoryCell">Existing inventory cell with item.</param>
    /// <param name="physicalItem">Variable for physical item.</param>
    public void GetItem(InventoryCell inventoryCell, out GameObject physicalItem)
    {
        _items.TryGetValue(inventoryCell, out physicalItem);
    }

    /// <summary>
    /// Is Contains item?
    /// </summary>
    /// <param name="key">Key value of inventory cell item.</param>
    /// <returns></returns>
    public bool ContainsKey(InventoryCell key)
    {
        return _items.ContainsKey(key);
    }
}
