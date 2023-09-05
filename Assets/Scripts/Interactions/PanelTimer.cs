using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PanelTimer : MonoBehaviour
{
    [SerializeField]
    private float fadeTime = 1.5f;

    [SerializeField]
    private float typingSpeed = 0.1f;

    [SerializeField]
    private TextMeshProUGUI text;

    private Color originalColor;
    private Image panelImage;

    private void Awake()
    {
        panelImage = GetComponent<Image>();
        originalColor = panelImage.color;
    }

    private void OnEnable()
    {
        StartCoroutine(FadeOut());
    }

    private IEnumerator FadeOut()
    {
        text.text = string.Empty;
        panelImage.color = originalColor;

        // Display first text
        yield return StartCoroutine(TypeText("Oh little cat..."));
        text.color = Color.white;
        yield return new WaitForSeconds(1.0f);

        // Display second text
        yield return StartCoroutine(TypeText("Come back when you have what it takes."));
        yield return new WaitForSeconds(1.0f);

        float startTime = Time.time;
        Color originalTextColor = text.color; // Get the original color of the text

        // Loop through time and update the panel's alpha and text's alpha
        while (Time.time < startTime + fadeTime)
        {
            float normalizedTime = (Time.time - startTime) / fadeTime;
            panelImage.color = new Color(originalColor.r, originalColor.g, originalColor.b, Mathf.Lerp(1, 0, normalizedTime));
            text.color = new Color(originalTextColor.r, originalTextColor.g, originalTextColor.b, Mathf.Lerp(1, 0, normalizedTime));
            yield return null;
        }

        // Disable the panel once it has faded out completely
        this.gameObject.transform.parent.gameObject.SetActive(false);
    }

    private IEnumerator TypeText(string message)
    {
        text.text += "\n \n";
        foreach (char letter in message.ToCharArray())
        {
            text.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }
    }
}