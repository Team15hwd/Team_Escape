using Cysharp.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagerCustom : Singleton<SceneManagerCustom>
{
    public async UniTask LoadSceneAsync(string sceneName)
    {
        Debug.Log($"[SceneManager] {sceneName} �� �ε� ����");

        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(sceneName);
        while (!asyncOperation.isDone)
        {
            await UniTask.Yield();
        }

        Debug.Log($"[SceneManager] {sceneName} �� �ε� �Ϸ�");
    }
}
