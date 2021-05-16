using Items.ScriptableObject;
using UnityEngine;

[RequireComponent(typeof(ItemInteractable))]
public class PhysicalItem : MonoBehaviour
{
    private MeshFilter _meshFilter;
    private MeshRenderer _meshRenderer;
    private Collider _collider;

    [SerializeField] private Item scriptableItem;

    private bool _hasInteracted;

    public MeshFilter MeshFilter => _meshFilter;
    public MeshRenderer MeshRenderer => _meshRenderer;
    public Collider Collider => _collider;

    private void Start()
    {
        _meshFilter = GetComponent<MeshFilter>();
        _meshRenderer = GetComponent<MeshRenderer>();
        _collider = GetComponent<Collider>();
    }

    private void OnDisable()
    {
        _meshFilter = GetComponent<MeshFilter>();
        _meshRenderer = GetComponent<MeshRenderer>();
        _collider = GetComponent<Collider>();
    }

    public void PickUp(Player player)
    {
        if (_hasInteracted) return;
        
        _hasInteracted = true;
        
        if (!scriptableItem.SetOwner(player)) return;

        if(player.InventoryController.Inventory.AddItem(scriptableItem, Instantiate(gameObject)))
            Destroy(gameObject);
    }
}

