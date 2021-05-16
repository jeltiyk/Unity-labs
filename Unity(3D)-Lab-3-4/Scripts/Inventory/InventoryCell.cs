using Items.ScriptableObject;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventoryCell : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private Color defaultColor = new Color(50, 50, 50, 255);
    [SerializeField] private Sprite defaultSprite;
    [SerializeField] private EquipmentType type;
    
    private Image _cellImage;

    public Item Item { get; private set; }
    public bool IsEquipped { get; private set; }
    public EquipmentType EquipmentType => type;

    public void Initialize()
    {
        _cellImage = transform.GetChild(0).GetComponent<Image>();
    }

    public void AddItemToCell(Item item)
    {
        if (IsEquipped)
        {
            RemoveItemFromCell();
            return;
        }

        IsEquipped = true;

        Item = item;
        _cellImage.sprite = item.InventoryIcon;
        _cellImage.color = Color.white;
    }
    
    public void RemoveItemFromCell()
    {
        IsEquipped = false;

        Item = null;

        _cellImage.sprite = defaultSprite != null ? defaultSprite : null;
        _cellImage.color = defaultColor;
    }

    private void OnLeftPointerClick()
    {
        if (Item != null)
        {
            if(Item.Drop(this))
                RemoveItemFromCell();
        }
    }
    
    private void OnRightPointerClick()
    {
        if (Item != null)
        {
            if(Item.Use(this))
                RemoveItemFromCell();
        }
    }
    
    public void OnPointerClick(PointerEventData eventData)
    {
        switch (eventData.button)
        {
            case PointerEventData.InputButton.Left:
            {
                OnLeftPointerClick();
                break;
            }
            case PointerEventData.InputButton.Middle:
            {
                break;
            }
            case PointerEventData.InputButton.Right:
            {
                OnRightPointerClick();
                break;
            }
        }
    }
}
