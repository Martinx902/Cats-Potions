using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class S_ButtonSound : MonoBehaviour, IPointerEnterHandler, IPointerDownHandler
{
    private void Start()
    {
        if (S_AudioManager.instance == null)
        {
            Debug.LogError("Tienes que añadir el Audio Manager a la escena");
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (S_AudioManager.instance != null)
        {
            S_AudioManager.instance.PlayHover();
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (S_AudioManager.instance != null)
        {
            S_AudioManager.instance.PlayPressed();
        }
    }
}