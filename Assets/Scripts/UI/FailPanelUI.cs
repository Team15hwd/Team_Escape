using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FailPanelUI : MonoBehaviour
{
    [SerializeField] Button retryButton;

    void Start()
    {
        retryButton.onClick.AddListener(() =>
        {
            SceneLoadManager.Instance.ReloadScene();
        });
    }
}
