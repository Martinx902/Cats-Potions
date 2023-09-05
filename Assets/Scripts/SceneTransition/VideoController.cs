using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class VideoController : MonoBehaviour
{
    private VideoPlayer video;

    [SerializeField]
    private LevelLoader levelLoader;

    [SerializeField]
    private Scenes sceneToGo;

    private void Awake()
    {
        video = GetComponent<VideoPlayer>();

        video.loopPointReached += EndReached;
    }

    private void EndReached(UnityEngine.Video.VideoPlayer vp)
    {
        levelLoader.LoadNextLevel(sceneToGo);
    }
}