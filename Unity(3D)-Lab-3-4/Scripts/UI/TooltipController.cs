using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class TooltipController : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private enum VisibilityMode
    {
        Appearance,
        Fading
    }

    [SerializeField] private GameObject tooltip;
    [SerializeField] private CanvasGroup _canvasGroup;
    
    private void Start()
    {
        if(tooltip == null)
            TryFindTooltip();

        _canvasGroup = tooltip.GetComponent<CanvasGroup>();
        _canvasGroup.alpha = 0;
        Debug.Log("_CanGroup: " + _canvasGroup);

        // if (tooltip != null) return;

        // TryFindTooltip();
    }

    private void TryFindTooltip()
    {
        if (transform.childCount == 0) return;

        GameObject tmpObject = GetComponentInChildren<TMP_Text>(true).transform.parent.gameObject;

        if (!tmpObject.CompareTag("Tooltip")) return;

        tooltip = tmpObject;
    }

    private void ShowTooltip()
    {
        if (!gameObject.activeInHierarchy) return;

        if (tooltip.activeInHierarchy) return;

        tooltip.SetActive(true);
    }

    private void HideTooltip()
    {
        if (!gameObject.activeInHierarchy) return;

        if (!tooltip.activeInHierarchy) return;

        tooltip.SetActive(false);
    }
    
    public void OnPointerEnter(PointerEventData eventData)
    {
        StopAllCoroutines();
        Debug.Log("Enter");
        // ShowTooltip();
        StartCoroutine(ChangeVisibility(VisibilityMode.Appearance, 0.02f));
    }
    
    public void OnPointerExit(PointerEventData eventData)
    {
        StopAllCoroutines();
        // HideTooltip();
        StartCoroutine(ChangeVisibility(VisibilityMode.Fading, 0.02f));
    }
    
    private IEnumerator ChangeVisibility(VisibilityMode mode, float transitionTime)
    {
        if (transitionTime == 0) yield break;
        
        switch (mode)
        {
            case VisibilityMode.Appearance:
            {
                while (_canvasGroup.alpha < 1f)
                {
                    _canvasGroup.alpha += 0.1f;

                    Debug.Log("GetAlpha = " + _canvasGroup.alpha);

                    yield return new WaitForSeconds(transitionTime);
                }
    
                break;
            }
            case VisibilityMode.Fading:
            {
                while (_canvasGroup.alpha > 0f)
                {
                    _canvasGroup.alpha -= 0.1f;

                    Debug.Log("GetAlpha = " + _canvasGroup.alpha);
    
                    yield return new WaitForSeconds(transitionTime);
                }
    
                break;
            }
        }
    }
}
