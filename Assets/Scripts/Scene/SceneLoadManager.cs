using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Cysharp.Threading.Tasks;
using UnityEditor;
using System;

public class SceneLoadManager : Singleton<SceneLoadManager>
{
    private bool isLoading = false;
    
    public event Action OnSceneLoad = delegate { };

    public void LoadScene(string sceneName)
    {
        if (isLoading)
            return;

        LoadSceneAsync(sceneName).Forget();
    }

    public void LoadScene(SceneAsset assetRef)
    {
        if (isLoading || !assetRef)
            return;

        LoadSceneAsync(assetRef.name).Forget();
    }



    private async UniTask LoadSceneAsync(string sceneName)
    {
        isLoading = true;

        await SceneManager.LoadSceneAsync(sceneName);

        //로딩 마무리 작업도 수행 로딩 넣으면 대기allow로 바꿔야됨

        OnSceneLoad?.Invoke();

        isLoading = false;
    }

    public void ReloadScene()
    {
        var currentScene = SceneManager.GetActiveScene();

        LoadScene(currentScene.name);
    }
}
