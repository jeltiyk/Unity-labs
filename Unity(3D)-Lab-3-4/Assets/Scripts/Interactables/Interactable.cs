using System.Collections;
using UnityEngine;

public abstract class Interactable : MonoBehaviour
{
    private bool _isInteracting;
    private float _stoppingDistance;
    private Collider _collider;
    private Vector3 _centerOfPlayer;
    private Vector3 _centerOfObject;

    private PCInputController _pcInputController;
    
    protected bool CanInteract;
    
    [SerializeField] private Player player;
    [SerializeField] private UIController uiController;
    [SerializeField] private float interactionRadius;
    [Range(0f, 1f)] [SerializeField] private float stoppingIndex;
    [Tooltip("When the player tries to interact")][Range(0f, 1f)][SerializeField] private float updateRate = 0.25f;

    public Player Player => player;
    protected UIController UIController => uiController;
    public float InteractionRadius => interactionRadius;
    public float StoppingDistance => _stoppingDistance;
    public Vector3 CenterOfObject => _centerOfObject;
   
    protected virtual void Awake()
    {
        _collider = GetComponent<Collider>();
       
        _pcInputController = player.GetComponent<PCInputController>();
        _stoppingDistance = interactionRadius * stoppingIndex;
    }

    protected virtual void Update()
    {
        // !!!
        _centerOfObject = new Vector3(_collider.bounds.center.x, player.transform.position.y, _collider.bounds.center.z);
        
        if (Vector3.Distance(player.transform.position, CenterOfObject) <= interactionRadius)
        {
            if (!Input.GetKeyDown(_pcInputController.InteractionKeyCode) && Input.anyKeyDown && _isInteracting)
            {
                _isInteracting = false;
                StopCoroutine(WalkUpToObject());
                player.ActionController.MoveTo(player.transform.position);
            }

            if (Input.GetKeyDown(_pcInputController.InteractionKeyCode) && !_isInteracting)
            {
                _isInteracting = true;
                Interact();
            }
        }
    }

    protected virtual void Interact()
    {
        if (!CanInteract)
            StartCoroutine(WalkUpToObject());
        else uiController.SetDebugText("Interacted with: " + gameObject.name);
    }

    private IEnumerator WalkUpToObject()
    {
        while (_isInteracting)
        {
            if(Vector3.Distance(player.transform.position, CenterOfObject) > _stoppingDistance)
                player.ActionController.MoveTo(CenterOfObject, _stoppingDistance);
            else
            {
                CanInteract = true;
                _isInteracting = false;
                Interact();
                
                yield break;
            }

            yield return new WaitForSeconds(updateRate);
        }
    }

    protected virtual void OnDrawGizmos()
    {
        Gizmos.color = Color.white; 
        Gizmos.DrawWireSphere(CenterOfObject, interactionRadius);

        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(CenterOfObject, StoppingDistance);
    }
}
