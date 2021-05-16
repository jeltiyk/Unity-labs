using UnityEngine;

public class EquipmentController : MonoBehaviour
{
    [SerializeField] private GameObject equipmentsUI;
    [SerializeField] private GameObject leftPlayerHand;
    [SerializeField] private GameObject rightPlayerHand;
    
    private Player _player;
    private EquipmentsInventory _equipmentsInventory;
    
    public EquipmentsInventory Equipments => _equipmentsInventory;

    public GameObject LeftPlayerHand => leftPlayerHand;
    public GameObject RightPlayerHand => rightPlayerHand;

    private void Start()
    {
        _player = GetComponent<Player>();
        _equipmentsInventory = new EquipmentsInventory(_player, equipmentsUI);
    }
}
