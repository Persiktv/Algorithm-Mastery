using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ExitAnimAutorization : MonoBehaviour
{
    public float duration = 1f; // длительность анимации в секундах
    public float height = 100f; // высота подъема элемента
    public Text messageText;

    public void OnButtonClick()
    {
        char firstChar = messageText.text[0];
        if (firstChar == 'Д')
        {
            StartCoroutine(MoveUp());
        }
    }


    private IEnumerator MoveUp()
    {
        float t = 0f;

        Vector3 startPos = transform.localPosition;
        Vector3 endPos = startPos + new Vector3(0, height, 0);

        while (t < duration)
        {
            t += Time.deltaTime;
            transform.localPosition = Vector3.Lerp(startPos, endPos, t / duration);
            yield return null;
        }
    }
}
