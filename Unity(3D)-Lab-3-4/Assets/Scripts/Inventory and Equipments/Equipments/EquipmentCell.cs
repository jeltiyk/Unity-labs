using Items.ScriptableObject;
using UnityEngine;
using UnityEngine.UI;

public class EquipmentCell : InventoryCell
{
    [SerializeField] private Color defaultColor = new Color(50, 50, 50, 0);
    [SerializeField] private Sprite defaultSprite;
    [SerializeField] private EquipmentType equipmentType;
    
    private EquipmentsInventory _equipmentsInventory;
    public EquipmentType EquipmentType => equipmentType;

    public void Initialize(EquipmentsInventory equipmentsInventory)
    {
        _equipmentsInventory = equipmentsInventory;
        
        CellImage = transform.GetChild(0).GetComponent<Image>();
        CellImage.sprite = defaultSprite;
        CellImage.color = defaultColor;
    }
    
    public override void RemoveItemFromCell()
    {
        Item = null;

        CellImage.sprite = defaultSprite != null ? defaultSprite : null;
        CellImage.color = defaultColor;
        
        if(ItemInfoUIController != null)
            Destroy(ItemInfoUIController.gameObject);
    }

    protected override void OpenItemPanel()
    {
        if(Item == null) return;

        if(!ItemInfoUIController)
        {
            ItemInfoUIController = Instantiate(_equipmentsInventory._uiController.ItemInfoUIController,
                _equipmentsInventory._uiController.ItemInfoUIController.transform.parent);
        }
        
        if(ItemInfoUIController.gameObject.activeInHierarchy) return;

        ItemInfoUIController.SetImage(Item.InventoryIcon);
        ItemInfoUIController.SetName(Item.Name);
        ItemInfoUIController.SetDescription(Item.Description);
        
        Equipment equipment = Item as Equipment;

        if (equipment)
        {
            if (equipment.PrimaryStats.Length > 0)
            {
                string primaryStats = "";

                foreach (var stat in equipment.PrimaryStats)
                    primaryStats += stat.StatType + ": " + stat.Amount + "\n";

                ItemInfoUIController.SetPrimaryStats(primaryStats);
            }

            if (equipment.AdditionalStats.Length > 0)
            {
                string additionalStats = "";

                foreach (var stat in equipment.AdditionalStats)
                    additionalStats += stat.StatType + ": " + stat.Amount + "\n";

                ItemInfoUIController.SetAdditionalStats(additionalStats);
            }
            
            ItemInfoUIController.SetType(ItemInfoUIController.ItemInfoType.Equipment);
        }
        else
            ItemInfoUIController.SetType(ItemInfoUIController.ItemInfoType.Item);

        ItemInfoUIController.gameObject.SetActive(true);
    }

    public override void OnDrop(InventoryCell fromCell, InventoryCell toCell)
    {
        if (fromCell == null && toCell == null) return;
        
        if (fromCell as EquipmentCell) // from equipment to inventory
        {
            if (!_equipmentsInventory.TryToReplaceBySameType(toCell, fromCell as EquipmentCell))
                _equipmentsInventory.TryToUnequip(toCell, fromCell as EquipmentCell);
        }
        
        // from inventory to equipment
        if (!_equipmentsInventory.TryToReplaceBySameType(fromCell, toCell as EquipmentCell))
            _equipmentsInventory.TryToEquip(fromCell, toCell as EquipmentCell);
    }
}
