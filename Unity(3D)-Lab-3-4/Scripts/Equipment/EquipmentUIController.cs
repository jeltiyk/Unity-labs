using Items.ScriptableObject;
using UnityEngine;

public class EquipmentUIController : MonoBehaviour
{
    private InventoryCell[] _equipmentCells;

    public void Initialize()
    {
        _equipmentCells = GetComponentsInChildren<InventoryCell>(true);

        foreach (var cell in _equipmentCells)
            cell.Initialize();
    }

    public InventoryCell GetCellByType(EquipmentType type)
    {
        foreach (var cell in _equipmentCells)
            if (cell.EquipmentType == type) return cell;

        return null;
    }
}
