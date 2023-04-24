using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class DragAndDrop : MonoBehaviour, IPointerDownHandler, IDragHandler, IEndDragHandler
{
    private bool isDragging = false;
    private Vector3 offset;

    public void OnPointerDown(PointerEventData eventData)
    {
        isDragging = true;
        offset = transform.position - Camera.main.ScreenToWorldPoint(eventData.position);
        GetComponent<Collider2D>().enabled = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = Camera.main.ScreenToWorldPoint(eventData.position) + offset;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        isDragging = false;
        GetComponent<Collider2D>().enabled = true;
        // выполнить дополнительные действия, если это необходимо
    }
}