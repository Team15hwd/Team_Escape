using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagerCustom : Singleton<SceneManagerCustom>
{
    public async UniTask LoadSceneAsync(string sceneName)
    {
        Debug.Log($"[SceneManager] {sceneName} 씬 로드 시작");

        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Single);
        while (!asyncOperation.isDone)
        {
            await UniTask.Yield();
        }

        Debug.Log($"[SceneManager] {sceneName} 씬 로드 완료");
    }
}
