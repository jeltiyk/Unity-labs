using UnityEngine;

[RequireComponent(typeof(NPCActionController))]
[RequireComponent(typeof(NPCInteractable))]
public class NPC : Entity
{
    public NPCActionController ActionController { get; private set; }
    public enum State
    {
        None,
        Angry,
    }

    private State _current;

    [SerializeField] private bool isFriendly;
    [Range(1.5f, 3f)][SerializeField] private float attackRange;

    public bool IsFriendly => isFriendly;
    public float AttackRange => attackRange;
    public State CurrentState => _current;

    protected override void Awake()
    {
        base.Awake();
        
        ActionController = GetComponent<NPCActionController>();
    }

    public void ChangeState(NPC.State newState)
    {
        _current = newState;
    }
}
