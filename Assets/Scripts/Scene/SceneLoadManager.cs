using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Cysharp.Threading.Tasks;
using UnityEditor;
using System;
using UnityEngine.UI;

public class SceneLoadManager : Singleton<SceneLoadManager>
{
    [SerializeField] private Image fadeImage;
    [SerializeField] private float fadeSpeed = 0.1f;

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
        fadeImage.fillAmount = 0f;

        var handle = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Single);
        handle.allowSceneActivation = false;

        await StartFade();
        await UniTask.Delay(200);

        handle.allowSceneActivation = true;
        OnSceneLoad?.Invoke();
        isLoading = false;

        await EndFade();
    }

    private async UniTask StartFade()
    {
        while (fadeImage.fillAmount < 0.999f)
        {
            await UniTask.NextFrame();

            fadeImage.fillAmount = Mathf.Lerp(fadeImage.fillAmount, 1, fadeSpeed * Time.unscaledDeltaTime);
        }

        fadeImage.fillAmount = 1f;
    }

    private async UniTask EndFade()
    {
        while (fadeImage.fillAmount > 0.001f)
        {
            await UniTask.NextFrame();

            fadeImage.fillAmount = Mathf.Lerp(fadeImage.fillAmount, 0, fadeSpeed * Time.unscaledDeltaTime);
        }

        fadeImage.fillAmount = 0f;
    }

    public void ReloadScene()
    {
        var currentScene = SceneManager.GetActiveScene();

        LoadScene(currentScene.name);
    }
}
