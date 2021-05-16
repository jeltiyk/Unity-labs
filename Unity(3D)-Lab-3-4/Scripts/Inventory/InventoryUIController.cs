using UnityEngine;

public class InventoryUIController : MonoBehaviour
{
    private InventoryCell[] inventoryCells;
    
    public void Initialize()
    {
        inventoryCells = GetComponentsInChildren<InventoryCell>(true);

        foreach (var cell in inventoryCells)
            cell.Initialize();
    }

    public InventoryCell GetFreeCell()
    {
        foreach (var cell in inventoryCells)
        {
            if (cell.Item == null) return cell;
        }

        return null;
    }
}
