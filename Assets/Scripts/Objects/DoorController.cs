using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour
{
    [SerializeField] private SceneLoader sceneLoader;

    private TriggerCollision[] triggers = new TriggerCollision[2];
    private const int playerCount = 2;

    private int triggerCount = 0;

    void Awake()
    {
        for (int i = 0; i < playerCount; i++)
        {
            var tr = transform.GetChild(i).GetComponent<TriggerCollision>();

            tr.OnTriggerEvent += (val, obj) =>
            {
                triggerCount++;

                if (triggerCount > 1)
                {
                    sceneLoader.LoadScene();
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
