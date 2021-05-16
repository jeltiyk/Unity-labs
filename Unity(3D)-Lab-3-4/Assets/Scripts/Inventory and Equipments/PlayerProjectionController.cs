using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerProjectionController : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private bool _isRotateAround;
    
    [SerializeField] private Camera projectionCamera;
    
    private void OnEnable()
    {
        projectionCamera.enabled = true;
    }

    private void OnDisable()
    {
        projectionCamera.enabled = false;
        _isRotateAround = false;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if(eventData.button != PointerEventData.InputButton.Left) return;

        _isRotateAround = true;
        projectionCamera.enabled = true;
    }

    public void OnDrag(PointerEventData eventData)
    {
        if(!_isRotateAround) return;
        
        projectionCamera.transform.RotateAround(projectionCamera.transform.parent.position, Vector3.up, eventData.delta.x);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        _isRotateAround = false;
    }
}
