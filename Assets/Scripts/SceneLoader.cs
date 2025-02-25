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

            // forget() : ��ư ��� �� ��� ��ٸ��� �ʰ� �񵿱� �۾� ����
            SceneManagerCustom.Instance.LoadSceneAsync(sceneName).Forget();
        }
        else
        {
            Debug.Log("[SceneLoader] ���� �������� ����");
        }
    }

    // �̷��� �ϰ� Ʃ�丮�� ���� Ŭ������ �Ʊ� �����Ͻ� �� ��� �Լ� ���� ��
    // �� ��ũ��Ʈ���� �����ϴ� ��!!!!
    // �³���?!?!?!?!?!??!?!?!?
}
