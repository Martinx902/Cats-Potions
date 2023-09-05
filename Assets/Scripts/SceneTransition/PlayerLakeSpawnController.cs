using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerLakeSpawnController : MonoBehaviour
{
    private string previousSceneName;

    [SerializeField]
    private GameObject player;

    [SerializeField]
    private Transform bearPos;

    [SerializeField]
    private Transform foxPos;

    [SerializeField]
    private Transform farmPos;

    private void Start()
    {
        Scene currentScene = SceneManager.GetActiveScene();

        previousSceneName = PlayerPrefs.GetString("PreviousScene", currentScene.name);

        if (player == null)
            Debug.LogError("No player added to the spawn controller");

        switch (previousSceneName)
        {
            case "BearSpot":
                player.transform.position = bearPos.position;
                break;

            case "FoxSpot":
                player.transform.position = foxPos.position;
                break;

            case "MageSpot":
                player.transform.position = farmPos.position;
                break;

            default:
                player.transform.position = new Vector3(0, 5, 0);
                Debug.LogError("[Player Spawn Controller]: No scene coming from detected / detected wrongly");
                break;
        }
    }
}