using UnityEngine;

[RequireComponent(typeof(PhysicalItem))]
public class ItemInteractable : Interactable
{
    private PhysicalItem _physicalItem;
    
    protected override void Awake()
    {
        base.Awake();

        _physicalItem = GetComponent<PhysicalItem>();
    }

    protected override void Interact()
    {
        base.Interact();
        if(!CanInteract) return;
        
        UIController.SetDebugTextColor(Color.yellow);
        
        _physicalItem.PickUp(Player);

        //redundant
        //CanInteract = false;
    }
}
