using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Cysharp.Threading.Tasks;
using System.Threading.Tasks;

public class Potal : MonoBehaviour
{
    [SerializeField] private SoundData soundData;

    private List<TriggerCollision> triggerCollisions = new(2);
    private Vector2[] linkedPos= new Vector2[2];

    private bool isTeleportion = false;

    private CountdownTimer timer = new(0.5f);

    void Start()
    {
        for (int i = 0; i < 2; i++)
        {
            triggerCollisions.Add(transform.GetChild(i).GetComponent<TriggerCollision>());

            triggerCollisions[i].OnTriggerEvent += OnPotal;
            triggerCollisions[i].OnTriggerExitEvent += OnExitPotal;

            linkedPos[i] = triggerCollisions[i].transform.GetChild(0).transform.position;
        }

        timer.OnTimerStart += () => isTeleportion = true;
        timer.OnTimerStop += () => isTeleportion = false;
    }

    private void OnPotal(TriggerCollision col, GameObject go)
    {
        if (!isTeleportion)
        {
            timer.Reset();
            timer.Start(false);

            int idx = triggerCollisions.FindIndex(x => x == col);
            idx = idx > 0 ? 0 : 1;

            go.transform.position = linkedPos[idx];

            SoundManager.Instance.Bulider().WidthSoundData(soundData).Play();
        }
    }

    private void OnExitPotal(TriggerCollision col, GameObject go)
    {

    }
}
