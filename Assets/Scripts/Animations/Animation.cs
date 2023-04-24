using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animation : MonoBehaviour
{
    public float duration = 1f; // ������������ �������� � ��������
    public float XPosition = 100f; // ������ ������� ��������
    public float YPosition = 100f; // ������ ������� ��������
    public float ZPosition = 100f; // ������ ������� ��������
    public RectTransform uiElement;
    public Vector2 newPosition = new Vector2(-967f, 967f); // ����� ���������� ��������

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
