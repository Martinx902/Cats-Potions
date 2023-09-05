using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerSpawnController : MonoBehaviour
{
    private string previousSceneName;

    [SerializeField]
    private List<PlayerSpawnPos> spawnPos;

    [SerializeField]
    private GameObject player;

    [SerializeField]
    private Transform defaultPos;

    private void Start()
    {
        Scene currentScene = SceneManager.GetActiveScene();

        previousSceneName = PlayerPrefs.GetString("PreviousScene", currentScene.name);

        if (player == null)
            Debug.LogError("No player added to the spawn controller");

        switch (previousSceneName)
        {
            case "BearSpot":
                player.transform.position = ChooseSpawnPoint(Scenes.Bear);
                break;

            case "FoxSpot":
                player.transform.position = ChooseSpawnPoint(Scenes.Fox);
                break;

            case "MageSpot":
                player.transform.position = ChooseSpawnPoint(Scenes.Farm);
                break;

            case "MageInterior":
                player.transform.position = ChooseSpawnPoint(Scenes.MageHouse);
                break;

            case "MageInterior_2ndF":
                player.transform.position = ChooseSpawnPoint(Scenes.HouseSecondFloor);
                break;

            case "SpringSpot_2":
                player.transform.position = ChooseSpawnPoint(Scenes.Lake);
                break;

            case "CursedSpringSpot":
                player.transform.position = ChooseSpawnPoint(Scenes.Lake);
                break;

            case "StartCinematic":
                player.transform.position = ChooseSpawnPoint(Scenes.StartCinematic);
                break;

            default:
                player.transform.position = defaultPos.position;
                Debug.LogError("[Player Spawn Controller]: No scene coming from detected / detected wrongly");
                break;
        }
    }

    private Vector3 ChooseSpawnPoint(Scenes sceneFrom)
    {
        foreach (PlayerSpawnPos pos in spawnPos)
        {
            if (pos.SceneName == sceneFrom)
            {
                return pos.transform.position;
            }
        }

        return defaultPos.position;
    }

    [System.Serializable]
    private struct PlayerSpawnPos
    {
        public Scenes SceneName;
        public Transform transform;
    }
}