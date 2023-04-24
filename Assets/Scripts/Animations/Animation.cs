using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animation : MonoBehaviour
{
    public float duration = 1f; // длительность анимации в секундах
    public float XPosition = 100f; // высота подъема элемента
    public float YPosition = 100f; // высота подъема элемента
    public float ZPosition = 100f; // высота подъема элемента
    public RectTransform uiElement;
    public Vector2 newPosition = new Vector2(-967f, 967f); // новые координаты элемента

    void Start()
    {
        uiElement.anchoredPosition = newPosition;
        StartCoroutine(MoveUp());
    }
    private IEnumerator MoveUp()
    {
        float t = 0f;

        Vector3 startPos = transform.localPosition;
        Vector3 endPos = startPos + new Vector3(XPosition, YPosition, ZPosition);

        while (t < duration)
        {
            t += Time.deltaTime;
            transform.localPosition = Vector3.Lerp(startPos, endPos, t / duration);
            yield return null;
        }
    }
}
