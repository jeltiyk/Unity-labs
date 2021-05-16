using UnityEngine;

[RequireComponent(typeof(PlayerActionController))]
[RequireComponent(typeof(InventoryController))]
[RequireComponent(typeof(EquipmentController))]
[RequireComponent(typeof(PCInputController))]
public class Player : Entity
{
    public PlayerActionController ActionController { get; private set; }
    public InventoryController InventoryController { get; private set; }
    public EquipmentController EquipmentController { get; private set; }

    protected override void Awake()
    {
        base.Awake();
        
        ActionController = GetComponent<PlayerActionController>();
        
        //Inventory initialization firstly
        InventoryController = GetComponent<InventoryController>();
        InventoryController.Initialize();
        
        // And then equipment initialization
        EquipmentController = GetComponent<EquipmentController>();
        EquipmentController.Initialize();
    }
}
