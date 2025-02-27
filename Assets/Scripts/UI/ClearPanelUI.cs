using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using System;

public class ClearPanelUI : MonoBehaviour
{
    [SerializeField] private Button retryBtn;
    [SerializeField] private Button nextBtn;
    [SerializeField] private Button cutSceneBtn;
    [SerializeField] private List<RectTransform> stars = new();

    public void StageClear(StageInfo info)
    {
        float clearTime = info.WhatTime();

        if (clearTime < info.PerpectClearTime)
        {
            stars.ForEach(s => s.gameObject.SetActive(true));

            nextBtn.gameObject.SetActive(true);
            cutSceneBtn.gameObject.SetActive(true);
        }
        else if (clearTime < info.NormalClearTime)
        {
            //별 두개
            stars[0].gameObject.SetActive(true);
            stars[2].gameObject.SetActive(true);

            nextBtn.gameObject.SetActive(true);
            retryBtn.gameObject.SetActive(true);
        }
        else
        {
            stars[1].gameObject.SetActive(true);

            nextBtn.gameObject.SetActive(true);
            retryBtn.gameObject.SetActive(true);
        }
    }

    private Action sceneLoadAction = delegate { };

    void Start()
    {
        retryBtn.onClick.AddListener(() =>
        {
            SceneLoadManager.Instance.ReloadScene();
        });
    }

    void OnEnable()
    {
        nextBtn.onClick.RemoveAllListeners();
        nextBtn.onClick.AddListener(() =>
        {
            sceneLoadAction?.Invoke();
        });
    }

    public void LoadScene(Action action)
    {
        sceneLoadAction = action;
    }

    void OnDisable()
    {
        stars.ForEach(s => s.gameObject.SetActive(false));
        retryBtn.gameObject.SetActive(false);
        nextBtn.gameObject.SetActive(false);
    }
}
