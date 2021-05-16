using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(Player))]
public class PCInputController : MonoBehaviour
{
    [Header("Global")]
    [Tooltip("Change visibility UI")]
    [SerializeField] private KeyCode UIKey;

    [Header("Debug")]
    [Tooltip("Use combination D + %Open key% to open")] 
    [SerializeField] private KeyCode DebugOpenKey;
    [Tooltip("Use combination D + %Close key% to close")]
    [SerializeField] private KeyCode DebugCloseKey;

    [Header("Other")]
    [Tooltip("Change visibility Info Panel")]
    [SerializeField] private KeyCode InfoKey;
    [Tooltip("Change visibility Inventory Panel")]
    [SerializeField] private KeyCode InventoryKey;
    [Tooltip("Change visibility Equipments Panel")]
    [SerializeField] private KeyCode EquipmentKey;
    [Tooltip("Interaction with world")]
    [SerializeField] private KeyCode InteractionKey;
    
    private Player _player;
    private Camera _camera;

    private UIController _uiController;
    
    private Canvas _debugCanvas;
    private GameObject _helpPanel;

    private bool _hasInteraction;
    
    public bool HasInteraction => _hasInteraction;

    private void Start()
    {
        _camera = Camera.main;

        _player = GetComponent<Player>();
        _debugCanvas = FindObjectOfType<Canvas>();
        _helpPanel = _debugCanvas.transform.GetChild(_debugCanvas.transform.childCount - 1).gameObject;
        
        _uiController = FindObjectOfType<UIController>();
    }

    private void Update()
    {
        UIKeys();

        _hasInteraction = Input.GetKeyDown(InteractionKey);
        
        if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject())
        {
            Ray ray = _camera.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out RaycastHit info, 100))
            {
                if(info.collider == null) return;
                
                _player.ActionController.OnLeftPointerClicked(info.point, info.collider);
            }
        }
    }

    private void UIKeys()
    {
        // Show / Hide Player UI
        if (Input.GetKeyDown(UIKey))
        {
            _uiController.ChangeUIVisibility();
        }
        
        // Show / Hide debug panel on UI
        if (Input.GetKey(KeyCode.D))
        {
            if(Input.GetKeyDown(DebugOpenKey))
                _uiController.ShowDebugPanel();
            else if(Input.GetKeyDown(DebugCloseKey))
                _uiController.HideDebugPanel();
        }
        
        // Show / Hide info panel on UI
        if (Input.GetKeyDown(InfoKey))
        {
            _uiController.ChangeInfoPanelVisibility();
        }

        // Show / Hide inventory panel on UI
        if (Input.GetKeyDown(InventoryKey))
        {
            _uiController.ChangeInventoryVisibility();
        }

        // Show / Hide equipment panel on UI
        if (Input.GetKeyDown(EquipmentKey))
        {
            _uiController.ChangeEquipmentVisibility();
        }
    }
}
