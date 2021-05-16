using UnityEngine;

[RequireComponent(typeof(Player))]
public class PCInputController : MonoBehaviour
{
    private Player _player;
    private Camera _camera;

    private Canvas _debugCanvas;
    private GameObject _helpPanel;
    
    private void Start()
    {
        _camera = Camera.main;

        _player = GetComponent<Player>();
        _debugCanvas = FindObjectOfType<Canvas>();
        _helpPanel = _debugCanvas.transform.GetChild(_debugCanvas.transform.childCount - 1).gameObject;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            if (!_debugCanvas.gameObject.activeInHierarchy)
            {
                _debugCanvas.gameObject.SetActive(true);
                return;
            }

            if (_debugCanvas.gameObject.activeInHierarchy)
            {
                if(_helpPanel.activeInHierarchy)
                    _helpPanel.SetActive(false);
                
                _debugCanvas.gameObject.SetActive(false);
            }

            // _debugCanvas.gameObject.SetActive(!_debugCanvas.gameObject.activeInHierarchy);
        }

        if (Input.GetKeyDown(KeyCode.K))
        {
            if(_debugCanvas.gameObject.activeInHierarchy)
                _helpPanel.SetActive(!_helpPanel.activeInHierarchy);
        }
        
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = _camera.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out RaycastHit info, 100))
            {
                _player.ActionController.OnLeftPointerClicked(info.point, info.collider);
            }
        }
    }
}
