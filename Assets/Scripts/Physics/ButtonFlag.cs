using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonFlag : TriggerController
{
    [SerializeField] private List<TriggerController> triggers;
    [SerializeField] private Sprite onButtonSprite;

    private bool isOn = false;
    
    void Start()
    {

    }

    public override void TriggerEnter(CharacterController2D cc)
    {
        if (!isOn)
        {
            isOn = true;

            foreach (var iter in triggers)
            {
                iter.TriggerEnter(null);
            }

            if (onButtonSprite != null)
            {
                GetComponent<SpriteRenderer>().sprite = onButtonSprite;
            }
        }
    }

    public override void TriggerExit(CharacterController2D cc)
    {

    }
}
