using System;
using Items.ScriptableObject;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CellItem : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IDropHandler, IPointerEnterHandler, IPointerExitHandler
{
    private static CellItem _draggableItem;
    
    private Vector2 _initialScale;
    private Vector2 _initialPosition;
    private Transform _initialParent;
    private CanvasGroup _canvasGroup;
    
    private InventoryCell _parentInventoryCell;
    private bool _isEquipment;

    private bool _isDropped;

    private Color _defaultColor;
    private Image _currentImage;

    private InventoryUITooltipController _tooltipController;
    
    private void Start()
    {
        _initialScale = transform.localScale;
        _initialPosition = transform.position;
        _initialParent = transform.parent;
        _canvasGroup = GetComponent<CanvasGroup>();

        _parentInventoryCell = GetComponentInParent<InventoryCell>();
        _isEquipment = _parentInventoryCell as EquipmentCell;

        _currentImage = _parentInventoryCell.GetComponent<Image>();
        _defaultColor = _currentImage.color;
        _tooltipController = GetComponent<InventoryUITooltipController>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if(eventData.button == PointerEventData.InputButton.Right) return;

        if(_parentInventoryCell.Item == null) return;
        
        if(!(_parentInventoryCell as EquipmentCell))
            _tooltipController.Destroy();
        
        _draggableItem = this;
        _canvasGroup.blocksRaycasts = false;

        transform.localScale = Vector2.one;
        _initialPosition = transform.position;
        _initialParent = transform.parent;
        
        transform.SetParent(transform.root);
    }

    public void OnDrag(PointerEventData eventData)
    {
        if(_draggableItem == null) return;
        
        transform.position = Input.mousePosition;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if(_draggableItem == null) return;

        _draggableItem = null;
        _canvasGroup.blocksRaycasts = true;

        transform.localScale = _initialScale;
        transform.position = _initialPosition;
        transform.SetParent(_initialParent);

        if (!_isDropped && !EventSystem.current.IsPointerOverGameObject())
            _parentInventoryCell.Item.Drop(_parentInventoryCell);
        
        _isDropped = false;
    }

    public void OnDrop(PointerEventData eventData)
    {
        if(_draggableItem == null) return;

        _draggableItem._isDropped = true;

        _currentImage.color = _defaultColor;

        InventoryCell destinationInventoryCell = _parentInventoryCell;
        
        // dragging between inventory
        if (!_draggableItem._isEquipment && !_isEquipment)
        {
            // _parentInventoryCell.OnDrop(_draggableItem._parentInventoryCell, _parentInventoryCell);
            destinationInventoryCell.OnDrop(_draggableItem._parentInventoryCell, destinationInventoryCell);
            return;
        }
        
        // Deny drag from equipment to equipment for avoid set equipment to not same equipment type cell
        if(_draggableItem._isEquipment && _isEquipment) return;
        
        // dragging between inventory and equipments
        EquipmentCell destinationEquipmentCell = null;

        if(_draggableItem._isEquipment) // from equipment to inventory
            destinationEquipmentCell = _draggableItem._parentInventoryCell as EquipmentCell;
        else if(_isEquipment) // from inventory to equipment
            destinationEquipmentCell = _parentInventoryCell as EquipmentCell;
            
        if(destinationEquipmentCell == null) return;
            
        // destinationEquipmentCell.OnDrop(_draggableItem._parentInventoryCell, _parentInventoryCell);
        destinationEquipmentCell.OnDrop(_draggableItem._parentInventoryCell, destinationInventoryCell);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (_parentInventoryCell.Item && !(_parentInventoryCell as EquipmentCell))
        {
            Equipment equipment = _parentInventoryCell.Item as Equipment;

            if (equipment && equipment.RequiredLvl > equipment.Owner.StatController.Lvl)
                _tooltipController.Initialize("Required lvl: " + equipment.RequiredLvl);
            else if (equipment && equipment.RequiredLvl <= equipment.Owner.StatController.Lvl)
                _tooltipController.Destroy();
        }
        
        if(_draggableItem == null) return;

        if (!_draggableItem._isEquipment && !_isEquipment)
        {
            if (!_draggableItem._parentInventoryCell.Item || !_parentInventoryCell.Item)
                _currentImage.color = new Color(0, 255, 0, 0.75f);
            else
                _currentImage.color = new Color(255, 255, 0, 0.75f);
            
            return;
        }

        if (_draggableItem._isEquipment && _isEquipment)
        {
            _currentImage.color = new Color(255, 0, 0, 0.75f);
            return;
        }
        
        if (_draggableItem._isEquipment)
        {
            EquipmentCell equipmentCell = _draggableItem._parentInventoryCell as EquipmentCell;

            if (_parentInventoryCell.Item)
            {
                Equipment inventoryEquipment = _parentInventoryCell.Item as Equipment;
                
                if (inventoryEquipment)
                {
                    if (inventoryEquipment.EquipmentType == equipmentCell.EquipmentType &&
                        inventoryEquipment.RequiredLvl <= inventoryEquipment.Owner.StatController.Lvl)
                    {
                        _currentImage.color = new Color(255, 255, 0, 0.75f);
                        return;
                    }
                }
                
                _currentImage.color = new Color(255, 0, 0, 0.75f);
                return;
            }

            _currentImage.color = new Color(0, 255, 0, 0.75f);
            return;
        }
        else if (_isEquipment)
        {
            EquipmentCell equipmentCell = _parentInventoryCell as EquipmentCell;
            Equipment equipment = _draggableItem._parentInventoryCell.Item as Equipment;
            
            if (equipmentCell.Item)
            {
                if (equipment)
                {
                    if (equipment.EquipmentType == equipmentCell.EquipmentType &&
                        equipment.RequiredLvl <= equipment.Owner.StatController.Lvl)
                    {
                        _currentImage.color = new Color(255, 255, 0, 0.75f);
                        return;
                    }
                }

                _currentImage.color = new Color(255, 0, 0, 0.75f);
                return;
            }

            if (equipment.EquipmentType == equipmentCell.EquipmentType &&
                equipment.RequiredLvl <= equipment.Owner.StatController.Lvl)
            {
                _currentImage.color = new Color(0, 255, 0, 0.75f);
                return;
            }

            _currentImage.color = new Color(255, 0, 0, 0.75f);
            return;
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        _currentImage.color = _defaultColor;
    }
}
    