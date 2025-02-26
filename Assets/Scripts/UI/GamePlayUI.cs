using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GamePlayUI : MonoBehaviour
{
    [SerializeField] private RectTransform clearPannel;
    [SerializeField] private RectTransform failPannel;
    [SerializeField] private ClearPanelUI clearPanelUI;
    [SerializeField] private List<Image> timeStars = new();

    private StageInfo info;

    void Start()
    {
        EventBus.Subscribe<ClearEvent>((val) => StageClear(val));

        clearPannel.gameObject.SetActive(false);
        failPannel.gameObject.SetActive(false);

        OnStage();
    }

    void Update()
    {
        DisplayTimeStar();
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
        //clearPannel.gameObject.SetActive(true);
        ////조건에 따라 별 생성

        //callback.sceneLoader.LoadScene();
        clearPanelUI.gameObject.SetActive(true);
        clearPanelUI.StageClear(info);
        clearPanelUI.LoadScene(() => clearEvent.sceneLoader.LoadScene());

        timeStars.ForEach(s => s.fillAmount = 1f);
    }

    private void StageFail()
    {

    }

    private void DisplayTimeStar()
    {
        float currentTime = GameTime.Time();

        if (info.PerpectClearTime > currentTime)
        {
            timeStars[0].fillAmount = 1 - (currentTime / info.PerpectClearTime);
        }
        else if (info.NormalClearTime > currentTime)
        {
            timeStars[1].fillAmount = 1 - ((currentTime - info.PerpectClearTime) / (info.NormalClearTime - info.PerpectClearTime));
        }
    }
}
