using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonFlag : TriggerController
{
    [SerializeField] private List<TriggerController> triggers;
    [SerializeField] private Sprite onButtonSprite;
    SpriteRenderer _spriteRenderer;

    private bool isOn = false;
    
    void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
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
                _spriteRenderer.sprite = onButtonSprite;
                //GetComponent<SpriteRenderer>().sprite = onButtonSprite;
            }
        }
        isOn = false;
   }

    public override void TriggerExit(CharacterController2D cc)
    {

    }
}
