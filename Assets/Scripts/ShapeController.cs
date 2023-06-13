using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShapeController : MonoBehaviour
{
    [SerializeField] private Transform Board;
    private int[,] shapeArr = { { 1, 0, 0 }, { 1, 1, 1 } };
    private int ShapePosX = 3;
    private int ShapePosY = 0;
    private int PosX;
    private int PosY;
    private int LastPosX;
    private int LastPosY;
    private int MaxDepth = 15;
    private int MaxLength = 10;
    public float Timeframe = .5f;


    private bool clickProcessed_Left = false;
    private bool clickProcessed_Right = false;
    private bool clickProcessed_Up = false;
    private bool clickProcessed_Down = false;
    private void Start()
    {
        PosX = ShapePosX; PosY = ShapePosY;
        DrawShape();
    }
    private void Update()
    {
        Timeframe -= Time.deltaTime;
        if (Timeframe <0)
        {
            Timeframe = 0.5f;
            if (CheckDown())
            {
                EraseShape();
                PosY++;
                DrawShape();
            }
            else
            {
                DrawShapePermanent();
                PosY = ShapePosY;
                DrawShape();
            }
            
        }
        GetInput();
    }
    public void DrawShapePermanent()
    {
        LastPosX = PosX;
        LastPosY = PosY;
        for (int i = PosY; i < PosY + shapeArr.GetLength(0); i++)
        {
            for (int j = PosX; j < PosX + shapeArr.GetLength(1); j++)
            {
                Debug.Log(PosX + shapeArr.GetLength(0));
                Debug.Log(PosY + shapeArr.GetLength(1));
                if (shapeArr[i - PosY, j - PosX] == 1)
                {
                    Board.GetChild(i).GetChild(j).GetComponent<BoxScript>()._value=1;
                }
            }
        }
    }
    public void DrawShape()
    {
        LastPosX = PosX;
        LastPosY = PosY;
        for(int i = PosY; i < PosY + shapeArr.GetLength(0); i++)
        {
            for(int j = PosX; j < PosX + shapeArr.GetLength(1); j++)
            {
                Debug.Log(PosX + shapeArr.GetLength(0));
                Debug.Log(PosY + shapeArr.GetLength(1));
                if (shapeArr[i-PosY,j-PosX]==1)
                {
                    Board.GetChild(i).GetChild(j).GetComponent<BoxScript>().SetSprite();
                }
            }
        }
    }
    public void EraseShape()
    {
        for (int i = LastPosY; i < LastPosY + shapeArr.GetLength(0); i++)
        {
            for (int j = LastPosX; j < LastPosX + shapeArr.GetLength(1); j++)
            {
                if (shapeArr[i - LastPosY, j - LastPosX] == 1)
                {
                    Board.GetChild(i).GetChild(j).GetComponent<BoxScript>().SetSpriteNormal();
                }
            }
        }
    }
    public bool CheckDown()
    {
        if (PosY >= MaxDepth - shapeArr.GetLength(0)) return false;
        for (int i = PosY; i < PosY + shapeArr.GetLength(0); i++)
        {
            for (int j = PosX; j < PosX + shapeArr.GetLength(1); j++)
            {
                if (Board.GetChild(i+1).GetChild(j).GetComponent<BoxScript>()._value==1)
                {
                    return false;
                }
            }
        }
        return true;
    }
    public bool CheckRight()
    {
        if (PosX >= MaxLength - shapeArr.GetLength(1)) return false;
        for (int i = PosY; i < PosY + shapeArr.GetLength(0); i++)
        {
            for (int j = PosX; j < PosX + shapeArr.GetLength(1); j++)
            {
                if (Board.GetChild(i).GetChild(j+1).GetComponent<BoxScript>()._value == 1)
                {
                    return false;
                }
            }
        }
        return true;
    }
    public bool CheckLeft()
    {
        if (PosX <= 0) return false;
        for (int i = PosY; i < PosY + shapeArr.GetLength(0); i++)
        {
            for (int j = PosX; j < PosX + shapeArr.GetLength(1); j++)
            {
                if (Board.GetChild(i).GetChild(j - 1).GetComponent<BoxScript>()._value == 1)
                {
                    return false;
                }
            }
        }
        return true;
    }
    

    private void GetInput()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow) && !clickProcessed_Left)
        {
            clickProcessed_Left = true;
            HandleLeftArrowClick();
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow) && !clickProcessed_Right)
        {
            clickProcessed_Right = true;
            HandleRightArrowClick();
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow) && !clickProcessed_Down)
        {
            clickProcessed_Down = true;
            HandleUpArrowClick();
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow) && !clickProcessed_Up)
        {
            clickProcessed_Up = true;
            HandleDownArrowClick();
        }

        if (Input.GetKeyUp(KeyCode.LeftArrow) || Input.GetKeyUp(KeyCode.RightArrow) || Input.GetKeyUp(KeyCode.UpArrow) || Input.GetKeyUp(KeyCode.DownArrow))
        {
            clickProcessed_Left = false;
            clickProcessed_Right = false;
            clickProcessed_Down = false;
            clickProcessed_Up = false;
        }
    }

    private void HandleLeftArrowClick()
    {
        if (CheckLeft())
        {
            if (PosX >= 1)
            {
                PosX--;
            }
        }
    }

    private void HandleRightArrowClick()
    {
        if (CheckRight())
        {
            if (PosX < MaxLength - shapeArr.GetLength(1))
            {
                PosX++;

            }
        }
    }

    private void HandleUpArrowClick()
    {
        // Logic for handling up arrow click
        Debug.Log("Up arrow clicked.");
    }

    private void HandleDownArrowClick()
    {
        // Logic for handling down arrow click
        Debug.Log("Down arrow clicked.");
    }
}
