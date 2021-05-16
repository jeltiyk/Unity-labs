using UnityEngine;

[RequireComponent(typeof(PlayerActionController))]
[RequireComponent(typeof(PlayerStatController))]
[RequireComponent(typeof(InventoryController))]
[RequireComponent(typeof(EquipmentController))]
[RequireComponent(typeof(PCInputController))]
public class Player : Entity
{
    public PlayerActionController ActionController { get; private set; }
    public PlayerStatController StatController { get; private set; }
    public InventoryController InventoryController { get; private set; }
    public EquipmentController EquipmentController { get; private set; }

    protected override void Awake()
    {
        base.Awake();
        
        ActionController = GetComponent<PlayerActionController>();
        StatController = GetComponent<PlayerStatController>();
        InventoryController = GetComponent<InventoryController>();
        EquipmentController = GetComponent<EquipmentController>();
    }
}
