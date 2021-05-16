using UnityEngine;

[RequireComponent(typeof(Player))]
public class PlayerActionController : ActionController
{
    private Player _player;
    private Interactable _lastInteraction;

    protected override void Awake()
    {
        base.Awake();

        _player = GetComponent<Player>();
    }
    
    public void OnLeftPointerClicked(Vector3 targetPosition, Collider targetCollider)
    {
        if (targetCollider != null)
        {
            _lastInteraction = targetCollider.GetComponent<Interactable>();

            if (_lastInteraction != null)
            {
                MoveTo(_lastInteraction.CenterOfObject, _lastInteraction.StoppingDistance);
                return;
            }
        }
        
        MoveTo(targetPosition, 0.3f);
    }
}
