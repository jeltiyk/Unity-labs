using UnityEngine;

[RequireComponent(typeof(Player))]
public class PlayerActionController : ActionController
{
    private Player _player;
    private Interactable _lastTarget;

    protected override void Start()
    {
        base.Start();

        _player = GetComponent<Player>();
    }
    
    public void OnLeftPointerClicked(Vector3 targetPosition, Collider targetCollider)
    {
        if (targetCollider != null)
        {
            _lastTarget = targetCollider.GetComponent<Interactable>();

            if (_lastTarget != null)
            {
                var lastTargetTransformPosition = _lastTarget.transform.position;
                Vector3 centerPoint = new Vector3(lastTargetTransformPosition.x, _player.transform.position.y, lastTargetTransformPosition.z);
                Move(centerPoint, _lastTarget.StoppingDistance);
                return;
            }
        }
        
        Move(targetPosition);
    }
}
