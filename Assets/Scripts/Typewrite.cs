using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class Typewrite : MonoBehaviour
{
    public float typingSpeed= 0.05f;
    private Coroutine typingCoroutine;
    public TextMeshProUGUI textMeshPro;
    public string textToWrite;
    void OnEnable() {
        StartTyping(textMeshPro, textToWrite);
    }
    public void StartTyping(TextMeshProUGUI text, string textToType) {
        if (typingCoroutine != null) {
            StopCoroutine(typingCoroutine);
        }

        typingCoroutine = StartCoroutine(TypeText(text, textToType));
    }

    public IEnumerator TypeText(TextMeshProUGUI text, string textToType) {
        text.text = "";

        for (int i = 0; i < textToType.Length; i++) {
            text.text += textToType[i];
            yield return new WaitForSeconds(typingSpeed);
        }
    }
}
