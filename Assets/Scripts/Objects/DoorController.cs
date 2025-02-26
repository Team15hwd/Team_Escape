using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour
{
    [SerializeField] private SceneLoader sceneLoader;
    [SerializeField] private Sprite doorOpenSprite;

    private ClearEvent clearEvent = new ClearEvent();

    private TriggerCollision[] triggers = new TriggerCollision[2];
    private const int playerCount = 2;

    private int triggerCount = 0;

    void Awake()
    {
        clearEvent.sceneLoader = sceneLoader;

        for (int i = 0; i < playerCount; i++)
        {
            var tr = transform.GetChild(i).GetComponent<TriggerCollision>();

            tr.OnTriggerEvent += (val, obj) =>
            {
                triggerCount++;
                tr.gameObject.GetComponent<SpriteRenderer>().sprite = doorOpenSprite;

                if (triggerCount > 1)
                {
                    EventBus.Call(clearEvent);
                }
            };

            tr.OnTriggerExitEvent += (val, obj) =>
            {
                triggerCount--;
            };
        }

        if (!sceneLoader)
        {
            sceneLoader = GetComponent<SceneLoader>();
        }
    }
}
