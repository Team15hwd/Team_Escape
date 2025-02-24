using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceProviders;
using UnityEngine.SceneManagement;

public class SceneManagerCustom : MonoBehaviour
{
    // Ʃ�丮���� �̹� �ô��� ����
    private const string TutorialKey = "TutorialCompleted";
    private static SceneManagerCustom instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public async UniTask LoadNextScene(string nextScene)
    {
        // Ʃ�丮���� �� ���� ���ٸ� Ʃ�丮�� ����
        if (nextScene == "Map1Scene")
        {
            bool isTutorialCompleted = PlayerPrefs.GetInt(TutorialKey, 0) == 1;
            if (!isTutorialCompleted)
            {
                await LoadSceneAsync("TutorialScene");
                PlayerPrefs.SetInt(TutorialKey, 1);
                PlayerPrefs.Save();
                return;
            }
        }

        await LoadSceneAsync(nextScene);
    }

    private async UniTask LoadSceneAsync(string sceneName)
    {
        Debug.Log($"[SceneManager] {sceneName} �� �ε� ����");

        AsyncOperationHandle<SceneInstance> handle = Addressables.LoadSceneAsync(sceneName, LoadSceneMode.Single);
        await handle.Task;

        Debug.Log($"[SceneManager] {sceneName} �� �ε� �Ϸ�");
    }

    // ��ŸƮ �� - SceneManager ���̰�
    // ��ư��Ʈ�ѷ� �ϳ� ���� �� �̵��ϸ� �� ��

    // await LoadSceneAsync("Map1Scene");
    // await UniTask.Delay(2000);
    // �� ���� �װ� �´� ��� ���׿�
    // �ϴ� ��ư ��Ʈ�ѷ����� ����ڽ��ϴ�
    // ���߿� ���� ����
}
