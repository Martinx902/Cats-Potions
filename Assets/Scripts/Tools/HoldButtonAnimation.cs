using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HoldButtonAnimation : MonoBehaviour
{
    public Scenes sceneToGo;

    public Image fillImage; // Reference to the Image component of the fill circle
    public float fillTime = 2f; // How long it takes to fill the circle
    public float minValue = 0f; // Minimum value for the button to be considered "held"
    public float maxValue = 1f; // Maximum value for the button to be considered "held"
    public float fadeTime = 0.5f; // How long it takes to fade in/out
    public CanvasGroup canvasGroup; // Reference to the CanvasGroup component of the GameObject

    public LevelLoader levelLoader;

    private float currentValue = 0f; // Current value of the button being held
    private bool isHolding = false; // Whether or not the button is being held
    private bool isFading = false; // Whether or not the fade animation is currently playing
    private float fillSpeed; // Speed at which the circle fills
    private float fadeSpeed; // Speed at which the alpha value of the GameObject changes

    // Start is called before the first frame update
    private void Start()
    {
        // Calculate fill speed based on fill time
        fillSpeed = (maxValue - minValue) / fillTime;
        // Calculate fade speed based on fade time
        fadeSpeed = 1f / fadeTime;
    }

    // Update is called once per frame
    private void Update()
    {
        if (Input.GetKey(KeyCode.E))
        {
            isHolding = true;
        }
        else
        {
            isHolding = false;
        }

        // If the button is being held, update the current value and fill the circle
        if (isHolding)
        {
            currentValue = Mathf.Clamp(currentValue + (fillSpeed * Time.deltaTime), minValue, maxValue);
            fillImage.fillAmount = currentValue;
        }
        // If the button is not being held, reset the current value and empty the circle
        else
        {
            currentValue = Mathf.Clamp(currentValue - (fillSpeed * Time.deltaTime), minValue, maxValue);
            fillImage.fillAmount = currentValue;
        }

        if (currentValue >= maxValue && isHolding)
        {
            levelLoader.LoadNextLevel(sceneToGo);
        }

        if (Input.anyKeyDown && !isFading)
        {
            StartCoroutine(FadeIn());
        }

        // If the button state has changed, play the appropriate fade animation
        if (isHolding && !isFading)
        {
            StartCoroutine(FadeIn());
        }
        else if (!isHolding && !isFading)
        {
            StartCoroutine(FadeOut());
        }
    }

    // Called when the button is pressed
    public void OnButtonDown()
    {
        isHolding = true;
    }

    // Called when the button is released
    public void OnButtonUp()
    {
        isHolding = false;
    }

    // Coroutine for the fade in animation
    private IEnumerator FadeIn()
    {
        isFading = true;
        while (canvasGroup.alpha < 1f)
        {
            canvasGroup.alpha = Mathf.Clamp01(canvasGroup.alpha + (fadeSpeed * Time.deltaTime));
            yield return null;
        }
        isFading = false;
    }

    // Coroutine for the fade out animation
    private IEnumerator FadeOut()
    {
        isFading = true;
        while (canvasGroup.alpha > 0f)
        {
            canvasGroup.alpha = Mathf.Clamp01(canvasGroup.alpha - (fadeSpeed * Time.deltaTime));

            if (canvasGroup.alpha <= 0.1f)
            {
                canvasGroup.alpha = 0f;
                break;
            }

            yield return null;
        }
        isFading = false;
    }
}