using UnityEngine;

[RequireComponent(typeof(PeacefulNPC))]
public class PeacefulNPCActionController : NPCActionController
{
    private PeacefulNPC _peacefulNPC;

    protected override void Awake()
    {
        base.Awake();
        
        _peacefulNPC = GetComponent<PeacefulNPC>();
    }
}
