using UnityEngine;

[RequireComponent(typeof(NPC))]
public abstract class NPCInteractable : Interactable
{
    private NPC _npc;
    
    protected override void Awake()
    {
        base.Awake();

        _npc = GetComponent<NPC>();
    }
}
