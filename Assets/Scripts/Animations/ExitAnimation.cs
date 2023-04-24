using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ExitAnimation : MonoBehaviour
{
    public float Duration = 1f; // длительность анимации в секундах
    public float Height = 100f; // высота подъема элемента

    public void OnButtonClick()
    {
        StartCoroutine(MoveUP());
    }


    private IEnumerator MoveUP()
    {
        float t = 0f;

        Vector3 startPos = transform.localPosition;
        Vector3 endPos = startPos + new Vector3(0, Height, 0);

        while (t < Duration)
        {
            t += Time.deltaTime;
            transform.localPosition = Vector3.Lerp(startPos, endPos, t / Duration);
            yield return null;
        }
    }
}
