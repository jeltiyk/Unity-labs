using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class UITooltipController : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    protected enum VisibilityMode
    {
        Appearance,
        Fading
    }

    [SerializeField] protected GameObject tooltip;
    protected CanvasGroup CanvasGroup;
    
    protected virtual void Start()
    {
        if(tooltip == null)
            TryToFindTooltip();

        CanvasGroup = tooltip.GetComponent<CanvasGroup>();
        CanvasGroup.alpha = 0;
    }

    private void TryToFindTooltip()
    {
        if (transform.childCount == 0) return;

        GameObject tooltipObject = GetComponentInChildren<TMP_Text>(true).transform.parent.gameObject;

        if (tooltipObject == null || !tooltipObject.CompareTag("Tooltip")) return;

        tooltip = tooltipObject;
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
    
    public virtual void OnPointerEnter(PointerEventData eventData)
    {
        if(!tooltip) return;

        StopAllCoroutines();
        // ShowTooltip();
        StartCoroutine(ChangeVisibility(VisibilityMode.Appearance, 0.02f));
    }
    
    public virtual void OnPointerExit(PointerEventData eventData)
    {
        if(!tooltip) return;
        
        StopAllCoroutines();
        // HideTooltip();
        StartCoroutine(ChangeVisibility(VisibilityMode.Fading, 0.02f));
    }
    
    protected virtual IEnumerator ChangeVisibility(VisibilityMode mode, float transitionTime)
    {
        if (transitionTime == 0) yield break;
        
        switch (mode)
        {
            case VisibilityMode.Appearance:
            {
                while (CanvasGroup.alpha < 1f)
                {
                    CanvasGroup.alpha += 0.1f;

                    yield return new WaitForSeconds(transitionTime);
                }
    
                break;
            }
            case VisibilityMode.Fading:
            {
                while (CanvasGroup.alpha > 0f)
                {
                    CanvasGroup.alpha -= 0.1f;
  
                    yield return new WaitForSeconds(transitionTime);
                }
    
                break;
            }
        }
    }
}
