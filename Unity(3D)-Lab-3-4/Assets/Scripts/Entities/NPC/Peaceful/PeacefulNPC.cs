using UnityEngine;

[RequireComponent(typeof(PeacefulNPCActionController))]
[RequireComponent(typeof(PeacefulNPCInteractable))]
public class PeacefulNPC : NPC
{
    public PeacefulNPCActionController ActionController { get; private set; }
    public PeacefulNPCInteractable InteractableController { get; private set; }

    protected override void Awake()
    {
        base.Awake();

        ActionController = GetComponent<PeacefulNPCActionController>();
        InteractableController = GetComponent<PeacefulNPCInteractable>();
    }
}
