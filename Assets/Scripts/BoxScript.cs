using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;
using UnityEngine;

public class BoxScript : MonoBehaviour
{
    public static Sprite[] SpriteArray;
    public int _value { get; set; }
    private void Awake()
    {
        _value = 0;
    }

    public void SetSprite(int SpriteIndex)
    {
        if (_value == 1)
        {
            GetComponent<Image>().enabled = true;
            GetComponent<Image>().sprite = SpriteArray[SpriteIndex];
        }
        else 
        {
            GetComponent<Image>().enabled = false;

        }

    }
    public void ShowVisual(int i,int SpriteIndex)
    {
        if (i == 1)
        {
            GetComponent<Image>().enabled = true;
            GetComponent<Image>().sprite = SpriteArray[SpriteIndex];
        }
        else
        {
            GetComponent<Image>().enabled = false;
        }
    }
}
