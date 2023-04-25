using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemAssets : MonoBehaviour
{
    public static ItemAssets Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }

    public Sprite flourSprite;
    public Sprite riceSprite;
    public Sprite saltSprite;
    public Sprite sugarSprite;
    public Sprite eggSprite;
    public Sprite milkSprite;
    public Sprite yeastSprite;
    public Sprite butterSprite;
}
