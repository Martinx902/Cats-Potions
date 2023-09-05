using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleInteractNotification : MonoBehaviour, INotificable
{
    [SerializeField]
    private Material interactionMat;

    [SerializeField]
    private Material defaultMat;

    [SerializeField]
    private float materialLerpTime = 0.8f;

    private MeshRenderer meshRenderer;

    private void Awake()
    {
        meshRenderer = GetComponent<MeshRenderer>();

        meshRenderer.material = defaultMat;
    }

    public void Notify()
    {
        meshRenderer.material = interactionMat;
    }

    public void EndNotify()
    {
        meshRenderer.material = defaultMat;
    }
}