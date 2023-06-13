using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class BoxScript : MonoBehaviour
{
    public Color _sprite;
    public int _value { get; set; }
    private void Awake()
    {
        _sprite = GetComponent<Image>().color;
        _value = 0;
    }

    public void SetSprite()
    {
        GetComponent<Image>().color = Color.red;
    }
    public void SetSpriteNormal()
    {
        GetComponent<Image>().color = _sprite;
    }
}
