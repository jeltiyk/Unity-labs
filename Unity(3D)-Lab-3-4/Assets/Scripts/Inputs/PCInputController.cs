using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(Player))]
public class PCInputController : MonoBehaviour
{
    [HideInInspector]
    public bool isActiveKeys;
    
    private Camera _camera;
    private Player _player;

    [SerializeField] private UIController uiController;
    
    [Header("Global")]
    [Tooltip("Change visibility UI")]
    [SerializeField] private KeyCode uiKey;

    [Header("Debug")]
    [Tooltip("Use combination D + %Open key% to open")] 
    [SerializeField] private KeyCode debugOpenKey;
    [Tooltip("Use combination D + %Close key% to close")]
    [SerializeField] private KeyCode debugCloseKey;

    [Header("Other")]
    [Tooltip("Change visibility Info Panel")]
    [SerializeField] private KeyCode infoKey;
    [Tooltip("Change visibility Inventory Panel")]
    [SerializeField] private KeyCode inventoryKey;
    [Tooltip("Change visibility Equipments Panel")]
    [SerializeField] private KeyCode equipmentKey;
    [Tooltip("Interaction with world")]
    [SerializeField] private KeyCode interactionKey;
    [Tooltip("Change visibility Chat Panel")] 
    [SerializeField] private KeyCode chatKey;
    
    public KeyCode InteractionKeyCode => interactionKey;

    private void Start()
    {
        _camera = Camera.main;
        _player = GetComponent<Player>();

        isActiveKeys = true;
    }

    private void Update()
    {
        if(!isActiveKeys) return;
        
        UIKeys();

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
        if (Input.GetKeyDown(uiKey))
        {
            uiController.ChangeUIVisibility();
        }
        
        // Show / Hide debug panel on UI
        if (Input.GetKey(KeyCode.D))
        {
            if(Input.GetKeyDown(debugOpenKey))
                uiController.ShowDebugPanel();
            else if(Input.GetKeyDown(debugCloseKey))
                uiController.HideDebugPanel();
        }
        
        // Show / Hide info panel on UI
        if (Input.GetKeyDown(infoKey))
        {
            uiController.ChangeInfoPanelVisibility();
        }

        // Show / Hide inventory panel on UI
        if (Input.GetKeyDown(inventoryKey))
        {
            uiController.ChangeInventoryVisibility();
        }

        // Show / Hide equipment panel on UI
        if (Input.GetKeyDown(equipmentKey))
        {
            uiController.ChangeEquipmentVisibility();
        }

        if (Input.GetKeyDown(chatKey))
        {
            uiController.ChangeChatVisibility();
        }
    }
}
