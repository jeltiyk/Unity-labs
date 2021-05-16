using UnityEngine;

[RequireComponent(typeof(PeacefulNPC))]
public class PeacefulNPCInteractable : NPCInteractable
{
    private PeacefulNPC _peacefulNPC;
    private bool _hasInteracted;

    protected override void Awake()
    {
        base.Awake();

        _peacefulNPC = GetComponent<PeacefulNPC>();
    }

    protected override void Update()
    {
        if (UIController.ChatActiveInHierarchy() && _hasInteracted) 
        {
            if (Vector3.Distance(Player.transform.position, CenterOfObject) > InteractionRadius)
            {
                UIController.ChangeChatVisibility();
                return;
            }
        }
        else if(!UIController.ChatActiveInHierarchy())
        {
            _hasInteracted = false;
        }

        base.Update();
    }

    protected override void Interact()
    {
        base.Interact();

        if(!CanInteract) return;
        
        if (!UIController.ChatActiveInHierarchy() && !_hasInteracted)
        {
            _hasInteracted = true;
            
            UIController.SetDebugTextColor(Color.green);
            UIController.ChangeChatVisibility();
        }
        
        CanInteract = false;
    }
}
