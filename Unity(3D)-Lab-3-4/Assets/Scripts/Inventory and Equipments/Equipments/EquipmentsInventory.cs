using System.Collections.Generic;
using Items.ScriptableObject;
using UnityEngine;

public class EquipmentsInventory
{
    private const int EquipmentsCapacity = 7;

    private readonly Dictionary<EquipmentCell, GameObject> _equipments;
    private readonly EquipmentCell[] _equipmentCells;
    
    private readonly Player _player;
    private readonly GameObject _equipmentUI;

    private readonly MeshFilter _leftPlayerHandMeshFilter;
    private readonly MeshRenderer _leftPlayerHandMeshRenderer;
    
    private readonly MeshFilter _rightPlayerHandMeshFilter;
    private readonly MeshRenderer _rightPlayerHandMeshRenderer;
    
    public readonly UIController _uiController;

    public EquipmentsInventory(Player player, GameObject equipmentUI)
    {
        _equipments = new Dictionary<EquipmentCell, GameObject>(EquipmentsCapacity);
        
        _equipmentCells = equipmentUI.GetComponentsInChildren<EquipmentCell>(true);

        foreach (var cell in _equipmentCells)
            cell.Initialize(this);
        
        
        _player = player;
        _equipmentUI = equipmentUI;
        _uiController = _equipmentUI.transform.root.GetComponent<UIController>();

        _leftPlayerHandMeshFilter = _player.EquipmentController.LeftPlayerHand.GetComponent<MeshFilter>();
        _leftPlayerHandMeshRenderer = _player.EquipmentController.LeftPlayerHand.GetComponent<MeshRenderer>();

        _rightPlayerHandMeshFilter = _player.EquipmentController.RightPlayerHand.GetComponent<MeshFilter>();
        _rightPlayerHandMeshRenderer = _player.EquipmentController.RightPlayerHand.GetComponent<MeshRenderer>();
    }
    /// <summary>
    /// Equip Player
    /// </summary>
    /// <param name="equipmentCell">Existing equipments cell with equipment.</param>
    private void EquipPlayer(EquipmentCell equipmentCell)
    {
        if(!_equipments.ContainsKey(equipmentCell)) return;
        
        if(equipmentCell.EquipmentType != EquipmentType.Weapon && equipmentCell.EquipmentType != EquipmentType.Shield) return;
        
        PhysicalItem physicalItem = _equipments[equipmentCell].GetComponent<PhysicalItem>();
        
        if (physicalItem == null)
        {
            Debug.Log("PhysicalItem is null");
            return;
        }
        
        if (physicalItem.MeshFilter == null)
        {
            Debug.Log("physicalItem.MeshFilter is null");
            return;
        }
        
        if (physicalItem.MeshRenderer == null)
        {
            Debug.Log("physicalItem.MeshRenderer is null");
            return;
        }
        
        switch (equipmentCell.EquipmentType)
        {
            case EquipmentType.Weapon:
                _rightPlayerHandMeshFilter.mesh = physicalItem.MeshFilter.mesh;
                _rightPlayerHandMeshRenderer.material = physicalItem.MeshRenderer.material;
                break;
            case EquipmentType.Shield:
                _leftPlayerHandMeshFilter.mesh = physicalItem.MeshFilter.mesh;
                _leftPlayerHandMeshRenderer.material = physicalItem.MeshRenderer.material;
                break;
        }
    }

    /// <summary>
    /// Unequip Player
    /// </summary>
    /// <param name="equipmentCell">Existing equipments cell with equipment.</param>
    private void UnequipPlayer(EquipmentCell equipmentCell)
    {
        if (!_equipments.ContainsKey(equipmentCell)) return;
      
        if(equipmentCell.EquipmentType != EquipmentType.Weapon && equipmentCell.EquipmentType != EquipmentType.Shield) return;

        switch (equipmentCell.EquipmentType)
        {
            case EquipmentType.Weapon:
                _rightPlayerHandMeshFilter.mesh = null;
                _rightPlayerHandMeshRenderer.material = null;
                break;
            case EquipmentType.Shield:
                _leftPlayerHandMeshFilter.mesh = null;
                _leftPlayerHandMeshRenderer.material = null;                
                break;
        }
    }
    
    /// <summary>
    /// Returns equipment cell by type of equipment. Returns equipment cell. If cell not found null.
    /// </summary>
    /// <param name="type">Type of equipment</param>
    /// <returns>Equipment cell. If cell not found null.</returns>
    private EquipmentCell GetCellByType(EquipmentType type)
    {
        foreach (var cell in _equipmentCells)
            if (cell.EquipmentType == type) return cell;

        return null;
    }
    
    /// <summary>
    /// Equips item to equipment cell. If equipment cell contain equipment then it returns to free inventory cell. Returns value of bool type.
    /// </summary>
    /// <param name="inventoryCell">Existing inventory cell with equipment.</param>
    /// <returns>Value of bool type.</returns>
    public bool Equip(InventoryCell inventoryCell)
    {
        if (!_player.InventoryController.Inventory.ContainsKey(inventoryCell)) return false;
        
        Equipment equipment = inventoryCell.Item as Equipment;
        
        if (equipment == null || equipment.RequiredLvl > _player.StatController.Lvl) return false;
        
        EquipmentCell equipmentCell = GetCellByType(equipment.EquipmentType);

        if (_equipments.ContainsKey(equipmentCell)) 
            return TryToReplaceBySameType(inventoryCell, equipmentCell);
        
        _player.InventoryController.Inventory.GetItem(inventoryCell, out GameObject physicalItem);
        
        equipmentCell.AddItemToCell(equipment);
        _equipments.Add(equipmentCell, physicalItem);
        EquipPlayer(equipmentCell);
        
        _player.StatController.ChangeStats(inventoryCell.Item as Equipment);
        
        inventoryCell.RemoveItemFromCell();
        return _player.InventoryController.Inventory.RemoveItem(inventoryCell);
    }
    
    /// <summary>
    /// Unequips and returns it to free inventory cell. Returns value of bool type.
    /// </summary>
    /// <param name="equipmentCell">Existing equipment cell with equipment.</param>
    /// <returns>Value of bool type.</returns>
    public bool Unequip(EquipmentCell equipmentCell)
    {
        if (!_equipments.ContainsKey(equipmentCell)) return false;

        InventoryCell freCell = _player.InventoryController.Inventory.GetFreeCell();

        if (freCell == null) return false;

        if (!_player.InventoryController.Inventory.AddItem(equipmentCell.Item, _equipments[equipmentCell]))
            return false;

        _player.StatController.ChangeStats(equipmentCell.Item as Equipment, false);
        UnequipPlayer(equipmentCell);
        
        equipmentCell.RemoveItemFromCell();
        return _equipments.Remove(equipmentCell);
    }
    
    /// <summary>
    /// Tries equip of equipment cell. Returns value of bool type.
    /// </summary>
    /// <param name="inventoryCell">Existing inventory cell with equipment.</param>
    /// <param name="equipmentCell">Existing equipment cell without equipment.</param>
    /// <returns>Value of bool type.</returns>
    public bool TryToEquip(InventoryCell inventoryCell, EquipmentCell equipmentCell)
    {
        if (!_player.InventoryController.Inventory.ContainsKey(inventoryCell)) return false;
        if (_equipments.ContainsKey(equipmentCell)) return false;
        
        Equipment equipment = inventoryCell.Item as Equipment;

        if (equipment == null || equipment.RequiredLvl > _player.StatController.Lvl || equipment.EquipmentType != equipmentCell.EquipmentType) return false;
        
        _player.InventoryController.Inventory.GetItem(inventoryCell, out GameObject physicalItem);

        equipmentCell.AddItemToCell(equipment);
        _equipments.Add(equipmentCell, physicalItem);
        EquipPlayer(equipmentCell);

        _player.StatController.ChangeStats(inventoryCell.Item as Equipment);
        
        inventoryCell.RemoveItemFromCell();
        return _player.InventoryController.Inventory.RemoveItem(inventoryCell);
    }

    /// <summary>
    /// Tries unequip and return it to inventory cell. Returns value of bool type.
    /// </summary>
    /// <param name="inventoryCell">Existing inventory cell without items</param>
    /// <param name="equipmentCell">Existing equipment cell with equipment</param>
    /// <returns>Value of bool type.</returns>
    public bool TryToUnequip(InventoryCell inventoryCell, EquipmentCell equipmentCell)
    {
        // must avoid filled inventory cells for possibility unequip
        if (_player.InventoryController.Inventory.ContainsKey(inventoryCell)) return false;
        // also must avoid empty equipment cells
        if (!_equipments.ContainsKey(equipmentCell)) return false;
        
        _equipments.TryGetValue(equipmentCell, out GameObject physicalItem);

        if (!_player.InventoryController.Inventory.AddItemToCell(equipmentCell.Item, physicalItem, inventoryCell)) return false;
        
        _player.StatController.ChangeStats(equipmentCell.Item as Equipment, false);
        UnequipPlayer(equipmentCell);
        
        equipmentCell.RemoveItemFromCell();
        return _equipments.Remove(equipmentCell);
    }
    
    /// <summary>
    /// Tries to swap two equipments. Returns value of bool type.
    /// </summary>
    /// <param name="inventoryCell">Existing inventory cell with equipment.</param>
    /// <param name="equipmentCell">Existing equipment cell with equipment.</param>
    /// <returns>Value of bool type.</returns>
    public bool TryToReplaceBySameType(InventoryCell inventoryCell, EquipmentCell equipmentCell)
    {
        if (!_player.InventoryController.Inventory.ContainsKey(inventoryCell)) return false;
        if (!_equipments.ContainsKey(equipmentCell)) return false;

        Equipment equipment = inventoryCell.Item as Equipment;

        if (equipment == null || equipment.RequiredLvl > _player.StatController.Lvl || equipment.EquipmentType != equipmentCell.EquipmentType) return false;

        _equipments.TryGetValue(equipmentCell, out GameObject equipmentPhysicalItem);
        _player.InventoryController.Inventory.GetItem(inventoryCell, out GameObject inventoryPhysicalItem);

        Item equipmentScriptableItem = equipmentCell.Item;

        _player.StatController.ChangeStats(equipmentCell.Item as Equipment, false);
        UnequipPlayer(equipmentCell);
        
        // assignment new equipment
        equipmentCell.RemoveItemFromCell();
        equipmentCell.AddItemToCell(inventoryCell.Item as Equipment);
        _equipments[equipmentCell] = inventoryPhysicalItem;

        _player.StatController.ChangeStats(inventoryCell.Item as Equipment);
        EquipPlayer(equipmentCell);
        
        _player.InventoryController.Inventory.RemoveItem(inventoryCell);
        return _player.InventoryController.Inventory.AddItemToCell(equipmentScriptableItem, equipmentPhysicalItem, inventoryCell);;
    }
}
