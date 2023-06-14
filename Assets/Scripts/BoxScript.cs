using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;
using UnityEngine;

public class BoxScript : MonoBehaviour
{
    private Sprite original;
    [SerializeField] private Sprite filled;
    public TextMeshProUGUI newtext;
    public int _value { get; set; }
    private void Awake()
    {
        original = GetComponent<Image>().sprite;
        newtext.text = _value.ToString();
        _value = 0;
    }

    public void SetSprite()
    {
        newtext.text = _value.ToString();
        if (_value == 1)
        {
            GetComponent<Image>().sprite = filled;
        }
        else 
        {
            GetComponent<Image>().sprite = original;
        }
        
    }
    public void ShowVisual(int i)
    {
        if(i==1)
            GetComponent<Image>().sprite = filled;
        else
            GetComponent<Image>().sprite = original;
    }
}
