using UnityEngine;

[RequireComponent(typeof(AggressiveNPC))]
public class AggressiveNPCInteractable : NPCInteractable
{
    private AggressiveNPC _agrAggressiveNPC;

    protected override void Awake()
    {
        base.Awake();

        _agrAggressiveNPC = GetComponent<AggressiveNPC>();
    }

    protected override void Interact()
    {
        base.Interact();

        if(!CanInteract) return;
        
        // temporary solution
        if (_agrAggressiveNPC.ActionController.CurrentAction != BaseActions.Attack)
        {
            _agrAggressiveNPC.ActionController.Search();
            UIController.SetDebugTextColor(Color.red);
        }
    }
}
