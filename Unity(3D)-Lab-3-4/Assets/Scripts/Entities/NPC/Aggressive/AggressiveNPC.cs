using UnityEngine;

[RequireComponent(typeof(AggressiveNPCActionController))]
[RequireComponent(typeof(AggressiveNPCInteractable))]
public class AggressiveNPC : NPC
{
    [SerializeField] private float attackRange;
    
    public AggressiveNPCActionController ActionController { get; private set; }
    public AggressiveNPCInteractable InteractableController { get; private set; }
    public float AttackRange => attackRange;

    protected override void Awake()
    {
        base.Awake();

        ActionController = GetComponent<AggressiveNPCActionController>();
        InteractableController = GetComponent<AggressiveNPCInteractable>();
    }

    private void Start()
    {
        if (attackRange < InteractableController.StoppingDistance)
            attackRange = InteractableController.StoppingDistance;

        if (attackRange > FieldOfView)
            attackRange = FieldOfView;
    }
}
