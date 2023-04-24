using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class DeleteOnRightClick : MonoBehaviour, IPointerClickHandler
{
    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Right)
        {
            Destroy(gameObject); // Удаляем текущий объект со сцены
        }
    }
}