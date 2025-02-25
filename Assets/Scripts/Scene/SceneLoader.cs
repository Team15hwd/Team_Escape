using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class SceneLoader : MonoBehaviour
{
    [SerializeField] SceneAsset sceneAsset;

    public void LoadScene()
    {
        SceneLoadManager.Instance.LoadScene(sceneAsset);
    }
}
