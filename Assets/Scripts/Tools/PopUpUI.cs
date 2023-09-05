using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PopUpUI : MonoBehaviour
{
    public static PopUpUI instance { get; private set; }

    [SerializeField]
    private GameObject PopUpPanel;

    [SerializeField]
    private TextMeshProUGUI messageUI;

    [SerializeField]
    private Image imageUI;

    [SerializeField]
    private CanvasGroup uiCanvasGroup;

    [Header("Popup Fade Duration")]
    [Space(15)]
    [SerializeField]
    [Range(.1f, 1.5f)]
    private float fadeInDuration = .3f;

    [SerializeField]
    [Range(.1f, 1.5f)]
    private float delayDuration = .3f;

    [Header("Messages")]
    [Space(15)]
    [SerializeField]
    private List<PopUpMessage> PopUpMessages = new List<PopUpMessage>();

    private Dictionary<PopUpType, PopUpData> popUpMessageDictionary = new Dictionary<PopUpType, PopUpData>();

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this);
        }
        else
        {
            instance = this;
        }

        if (PopUpPanel == null)
            Debug.LogError("No PopUp Panel Added to UIController");

        foreach (PopUpMessage message in PopUpMessages)
        {
            if (!popUpMessageDictionary.ContainsKey(message.popUpKey))
                popUpMessageDictionary.Add(message.popUpKey, message.popUpData);
        }

        uiCanvasGroup.alpha = 0f;
        uiCanvasGroup.interactable = false;
    }

    public void Show(PopUpType typeOfMessage, Sprite _image = null, string _message = null)
    {
        if (!popUpMessageDictionary.ContainsKey(typeOfMessage))
        {
            messageUI.text = popUpMessageDictionary[PopUpType.None].message;
            imageUI.sprite = popUpMessageDictionary[PopUpType.None].image;
        }

        if (string.IsNullOrEmpty(_message))
        {
            messageUI.text = popUpMessageDictionary[typeOfMessage].message;
        }
        else
        {
            messageUI.text = _message;
        }

        if (_image == null)
        {
            imageUI.sprite = popUpMessageDictionary[typeOfMessage].image;
        }
        else
        {
            imageUI.sprite = _image;
        }

        Dismiss();
        StartCoroutine(FadeCanvas(uiCanvasGroup, 0, 1, fadeInDuration, true));
    }

    private IEnumerator FadeCanvas(CanvasGroup cGroup, float startAlpha, float endAlpha, float fadeDuration, bool fadeIn)
    {
        float targetAlpha = fadeIn ? 1 : 0;

        float timeElapsed = 0;
        while (timeElapsed < fadeDuration)
        {
            timeElapsed += Time.deltaTime;
            float blend = Mathf.Clamp01(timeElapsed / fadeDuration);

            cGroup.alpha = Mathf.Lerp(startAlpha, targetAlpha, blend);

            yield return null;
        }

        cGroup.alpha = targetAlpha;

        if (fadeIn && delayDuration > 0)
        {
            yield return new WaitForSeconds(delayDuration);
        }

        targetAlpha = fadeIn ? 0 : 1;
        startAlpha = cGroup.alpha;

        timeElapsed = 0;
        while (timeElapsed < fadeDuration)
        {
            timeElapsed += Time.deltaTime;
            float blend = Mathf.Clamp01(timeElapsed / fadeDuration);

            cGroup.alpha = Mathf.Lerp(startAlpha, targetAlpha, blend);

            yield return null;
        }

        cGroup.alpha = targetAlpha;
    }

    public void Dismiss()
    {
        StopAllCoroutines();
        uiCanvasGroup.alpha = 0f;
        uiCanvasGroup.interactable = false;
    }
}

public enum PopUpType
{
    InventoryFull,
    InventoryNewItem,
    InventoryRemoveItem,
    NewRecipe,
    NewMission,
    MissionCompleted,
    IntroduceAllItems,
    NotTimeToGoToBedYet,
    TimeToGoToBed,
    FellAsleep,
    NoSeeds,
    None
}

[System.Serializable]
public class PopUpMessage
{
    public PopUpType popUpKey;
    public PopUpData popUpData;

    private PopUpMessage(PopUpData popUpData, PopUpType popUpKey)
    {
        this.popUpData = popUpData;
        this.popUpKey = popUpKey;
    }
}

[System.Serializable]
public class PopUpData
{
    public string message;
    public Sprite image;

    private PopUpData(string message, Sprite image)
    {
        this.message = message;
        this.image = image;
    }
}