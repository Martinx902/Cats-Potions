using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ChangeSceneTrigger : MonoBehaviour
{
    [SerializeField]
    private Scenes sceneToGo;

    public UnityEvent<Scenes> onSceneChange;

    private void OnCollisionEnter(Collision collision)
    {
        if (onSceneChange != null)
            onSceneChange.Invoke(sceneToGo);
    }

    public void Trigger()
    {
        onSceneChange.Invoke(sceneToGo);
    }
}