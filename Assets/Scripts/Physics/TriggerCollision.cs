using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class TriggerCollision : MonoBehaviour
{
    public event Action<TriggerCollision, GameObject> OnTriggerEvent = delegate { };
    public event Action<TriggerCollision, GameObject> OnTriggerExitEvent = delegate { };

    private void OnTriggerEnter2D(Collider2D collision)
    {
        OnTriggerEvent?.Invoke(this, collision.gameObject);

        if (collision.TryGetComponent(out TriggerController tc))
        {
            tc.TriggerEnter(null);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        OnTriggerExitEvent?.Invoke(this, collision.gameObject);

        if (collision.TryGetComponent(out TriggerController tc))
        {
            tc.TriggerExit(null);
        }
    }
}
