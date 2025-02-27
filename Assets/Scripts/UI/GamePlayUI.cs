using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GamePlayUI : MonoBehaviour
{
    [SerializeField] private RectTransform clearPannel;
    [SerializeField] private RectTransform failPannel;
    [SerializeField] private ClearPanelUI clearPanelUI;
    [SerializeField] private List<Image> timerFillImages = new();

    private StageInfo info;

    void Start()
    {
        EventBus.Subscribe<ClearEvent>(StageClear);
        EventBus.Subscribe<DeadEvent>(StageFail);

        clearPannel.gameObject.SetActive(false);
        failPannel.gameObject.SetActive(false);

        OnStage();
    }

    void OnDisable()
    {
        
    }

    void OnDestroy()
    {
        EventBus.Unsubscribe<ClearEvent>(StageClear);
        EventBus.Unsubscribe<DeadEvent>(StageFail);
    }

    void Update()
    {
        DisplayTimer();
    }

    private void OnStage()
    {
        var info = FindFirstObjectByType<StageInfo>();
        this.info = info;
    }
    
    private void StageClear(ClearEvent clearEvent)
    {
        if (info == null)
            return;

        clearPanelUI.gameObject.SetActive(true);
        clearPanelUI.StageClear(info);
        clearPanelUI.LoadScene(() => clearEvent.sceneLoader.LoadScene());

        timerFillImages.ForEach(s => s.fillAmount = 1f);
    }

    private void StageFail(DeadEvent deadEvent)
    {
        failPannel.gameObject.SetActive(true);
    }

    private void DisplayTimer()
    {
        float currentTime = GameTime.Time();

        if (info.PerpectClearTime > currentTime)
        {
            timerFillImages[0].fillAmount = 1 - (currentTime / info.PerpectClearTime);
        }
        else if (info.NormalClearTime > currentTime)
        {
            timerFillImages[1].fillAmount = 1 - ((currentTime - info.PerpectClearTime) / (info.NormalClearTime - info.PerpectClearTime));
        }
    }
}
