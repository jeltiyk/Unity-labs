using Items.ScriptableObject;
using UnityEngine;

[RequireComponent(typeof(ItemInteractable))]
public class PhysicalItem : MonoBehaviour
{
    private MeshRenderer _meshRenderer;
    private MeshFilter _meshFilter;
    private Collider _collider;

    [SerializeField] private Item scriptableItem;

    private bool _hasInteraction;
    
    private void Start()
    {
        _meshRenderer = GetComponent<MeshRenderer>();
        _meshFilter = GetComponent<MeshFilter>();
        _collider = GetComponent<Collider>();
    }

    public void PickUp(Player player)
    {
        if (_hasInteraction) return;
        
        _hasInteraction = true;
        
        if (!scriptableItem.SetOwner(player)) return;
        
        if(player.InventoryController.Inventory.AddItem(scriptableItem, Instantiate(gameObject)))
            Destroy(gameObject);
    }
}

