using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceProviders;
using UnityEngine.SceneManagement;

public class SceneManagerCustom : MonoBehaviour
{
    // 튜토리얼을 이미 봤는지 여부
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
        // 튜토리얼을 본 적이 없다면 튜토리얼 실행
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
        Debug.Log($"[SceneManager] {sceneName} 씬 로드 시작");

        AsyncOperationHandle<SceneInstance> handle = Addressables.LoadSceneAsync(sceneName, LoadSceneMode.Single);
        await handle.Task;

        Debug.Log($"[SceneManager] {sceneName} 씬 로드 완료");
    }

    // 스타트 씬 - SceneManager 붙이고
    // 버튼컨트롤러 하나 만들어서 씬 이동하면 될 듯

    // await LoadSceneAsync("Map1Scene");
    // await UniTask.Delay(2000);
    // 얘 말고 그게 맞는 방법 같네요
    // 일단 버튼 컨트롤러까지 만들겠습니다
    // 나중에 컨펌 ㅂㅌ
}
