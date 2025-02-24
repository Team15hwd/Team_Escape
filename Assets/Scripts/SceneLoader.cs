using Cysharp.Threading.Tasks;
using UnityEditor;
using UnityEngine;

public class SceneLoader : MonoBehaviour
{
    [SerializeField] private SceneAsset scene;

    public void LoadScene()
    {
        if (scene != null)
        {
            string sceneName = scene.name;

            // forget() : 버튼 사용 시 결과 기다리지 않고 비동기 작업 수행
            SceneManagerCustom.Instance.LoadSceneAsync(sceneName).Forget();
        }
        else
        {
            Debug.Log("[SceneLoader] 씬이 설정되지 않음");
        }
    }

    // 이렇게 하고 튜토리얼 관련 클래스나 아까 말씀하신 문 통과 함수 같은 건
    // 각 스크립트에서 관리하는 것!!!!
    // 맞나요?!?!?!?!?!??!?!?!?
}
