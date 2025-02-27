using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RandomSprite : MonoBehaviour
{
    [SerializeField] private List<Sprite> sprites;

    private Image image;

    void Awake()
    {
        image = GetComponent<Image>();
    }

    void OnEnable()
    {
        image.sprite = sprites[Random.Range(0, sprites.Count)];
    }
}
