using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class VideoScene : MonoBehaviour
{
    [SerializeField] private SceneLoader sceneLoader;

    private VideoPlayer videoPlayer;

    void Awake()
    {
        videoPlayer = GetComponent<VideoPlayer>();
        videoPlayer.loopPointReached += (v) =>
        {
            sceneLoader?.LoadScene();
        };
    }
}
