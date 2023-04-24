using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine;

public class CopyOnDrag : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerClickHandler
{
    public GameObject copyPrefab;
    public Transform panelTransform;

    private GameObject copyObject;
    private Vector3 initialPosition;

    public void OnBeginDrag(PointerEventData eventData)
    {
        copyObject = Instantiate(copyPrefab, transform.position, Quaternion.identity);
        copyObject.transform.SetParent(transform.parent);
        initialPosition = transform.position;
    }

    public void OnDrag(PointerEventData eventData)
    {
        Vector3 newPosition = Camera.main.ScreenToWorldPoint(eventData.position);
        newPosition.z = initialPosition.z;
        copyObject.transform.position = newPosition;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (panelTransform != null)
        {
            RectTransform panelRect = panelTransform.GetComponent<RectTransform>();
            if (RectTransformUtility.RectangleContainsScreenPoint(panelRect, eventData.position))
            {
                copyObject.transform.SetParent(panelTransform);
                copyObject.GetComponent<CanvasGroup>().blocksRaycasts = true;
            }
            else
            {
                Destroy(copyObject);
            }
        }
        else
        {
            Destroy(copyObject);
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Right && copyObject != null && copyObject.transform.parent == panelTransform)
        {
            Destroy(copyObject);
        }
    }
}