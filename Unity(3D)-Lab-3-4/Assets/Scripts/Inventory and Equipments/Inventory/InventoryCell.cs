using Items.ScriptableObject;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventoryCell : MonoBehaviour, IPointerClickHandler
{
    private Inventory _inventory;
    protected Image CellImage;
    protected ItemInfoUIController ItemInfoUIController;

    public Item Item { get; protected set; }

    public virtual void Initialize(Inventory inventory)
    {
        _inventory = inventory;

        CellImage = transform.GetChild(0).GetComponent<Image>();
    }

    public virtual void AddItemToCell(Item item)
    {
        Item = item;
        CellImage.sprite = item.InventoryIcon;
        CellImage.color = Color.white;
    }
    
    public virtual void RemoveItemFromCell()
    {
        Item = null;

        CellImage.sprite = null;
        CellImage.color = Color.clear;
        
        if(ItemInfoUIController != null)
            Destroy(ItemInfoUIController.gameObject);
    }

    protected virtual void OnLeftPointerClick()
    {
        if(Item == null) return;

        Item.Drop(this);
    }

    protected virtual void OnMiddlePointerClicked()
    {
        OpenItemPanel();
    }
    
    protected virtual void OnRightPointerClick()
    {
        if (Item == null) return;

        Item.Use(this);
    }
    
    public void OnPointerClick(PointerEventData eventData)
    {
        switch (eventData.button)
        {
            case PointerEventData.InputButton.Left:
            {
                // OnLeftPointerClick();
                break;
            }
            case PointerEventData.InputButton.Middle:
            {
                OnMiddlePointerClicked();
                break;
            }
            case PointerEventData.InputButton.Right:
            {
                OnRightPointerClick();
                break;
            }
        }
    }

    protected virtual void OpenItemPanel()
    {
        if(Item == null) return;
        
        if(!ItemInfoUIController)
        {
            ItemInfoUIController = Instantiate(_inventory._uiController.ItemInfoUIController,
                _inventory._uiController.ItemInfoUIController.transform.parent);
        }
        
        if(ItemInfoUIController.gameObject.activeInHierarchy) return;

        ItemInfoUIController.transform.SetAsLastSibling();
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
    
    public virtual void OnDrop(InventoryCell fromCell, InventoryCell toCell)
    {
        if(fromCell == null || toCell == null) return;
       
        if(!_inventory.ChangeItemPosition(fromCell, toCell)) 
            Debug.Log("Cell's place do not changed");
        
        return;
    }
}
