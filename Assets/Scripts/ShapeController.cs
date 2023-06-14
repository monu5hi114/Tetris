using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ShapeController : MonoBehaviour
{
    [SerializeField]public InputSwap inputSwap;
    private int Score = 0;
    [SerializeField] private Transform Board;
    [SerializeField] private TextMeshProUGUI ScoreText;
    [SerializeField] private GameObject Node;
    [SerializeField] private GameObject nextshape;
    private int[,] shapeArr;
    private int shapeIndex;
    private int nextShapeIndex;
    private List<int[,]> Shapes = new List<int[,]>();
    private int[,] shape1 =
       { 
        { 1, 0, 0 },
        { 1, 1, 1 } 
    };
    private int[,] shape2 =
       {
        { 0, 0, 1 },
        { 1, 1, 1 }
    };
    private int[,] shape3 =
       {
        { 0, 1, 0 },
        { 1, 1, 1 }
    };
    private int[,] shape4 =
       {
        { 1, 1, 0 },
        { 0, 1, 1 }
    };
    private int[,] shape5 =
       {
        { 0, 1, 1 },
        { 1, 1, 0 }
    };
    private int[,] shape6 =
       {
        { 1, 1 },
        { 1, 1 }
    };
    private int[,] shape7 =
       {
        { 1, 1, 1, 1 }
    };
    private int ShapePosX = 3;
    private int ShapePosY = 0;
    private int PosX;
    private int PosY;
    private int LastPosX;
    private int LastPosY;
    private int MaxDepth = 20;
    private int MaxLength = 9;
    public float Timeframe = .5f;
    [SerializeField] private  Image[] SpriteShapeArray;
    [SerializeField] private  Sprite[] SpriteArray;


    private bool clickProcessed_Left = false;
    private bool clickProcessed_Right = false;
    private bool clickProcessed_Up = false;
    private bool clickProcessed_Down = false;
    private void Awake()
    {
        ScoreText.text = Score.ToString();

        BoxScript.SpriteArray = SpriteArray;
        inputSwap.OnSwipeUp += InputSwap_OnSwipeUp;
        inputSwap.OnSwipeDown += InputSwap_OnSwipeDown;
        inputSwap.OnSwipeLeft += InputSwap_OnSwipeLeft;
        inputSwap.OnSwipeRight += InputSwap_OnSwipeRight;
    }

    private void InputSwap_OnSwipeRight(object sender, System.EventArgs e)
    {
        HandleRightArrowClick();
    }

    private void InputSwap_OnSwipeLeft(object sender, System.EventArgs e)
    {
        HandleLeftArrowClick();
    }

    private void InputSwap_OnSwipeDown(object sender, System.EventArgs e)
    {
        HandleDownArrowClick();
    }

    private void InputSwap_OnSwipeUp(object sender, System.EventArgs e)
    {
        HandleUpArrowClick();
    }

    

    


    private void Start()
    {
        Shapes.Add(shape1);
        Shapes.Add(shape2);
        Shapes.Add(shape3);
        Shapes.Add(shape4);
        Shapes.Add(shape5);
        Shapes.Add(shape6);
        Shapes.Add(shape7);
        PosX = ShapePosX; PosY = ShapePosY;
        shapeIndex = UnityEngine.Random.Range(0, 7);
        nextShapeIndex = UnityEngine.Random.Range(0, 7);
        nextShapeShow();
        shapeArr = Shapes[shapeIndex];
        DrawShape();
    }
    public void nextShapeShow()
    {
        Destroy(nextshape.transform.GetChild(0).gameObject);
        Instantiate(SpriteShapeArray[nextShapeIndex], nextshape.transform.position, Quaternion.identity, nextshape.transform);
    }
    private void Update()
    {
        Timeframe -= Time.deltaTime;
        if (Timeframe <0)
        {
            Timeframe = 0.3f;
            if (CheckDown())
            {
                EraseShape();
                PosY++;
                DrawShape();
            }
            else
            {
                EraseShape();
                DrawShapePermanent();
                ScoreLines();
                PosY = ShapePosY;
                PosX = ShapePosX; 
                shapeIndex = nextShapeIndex;
                nextShapeIndex = UnityEngine.Random.Range(0, 7);
                nextShapeShow();
                shapeArr = Shapes[shapeIndex];

                DrawShape();
            }
            
        }
        GetInput();
    }
    public void DrawShapePermanent()
    {
        for (int i = PosY; i < PosY + shapeArr.GetLength(0); i++)
        {
            for (int j = PosX; j < PosX + shapeArr.GetLength(1); j++)
            {
                if (shapeArr[i - PosY, j - PosX] == 1)
                {
                    Board.GetChild(i).GetChild(j).GetComponent<BoxScript>()._value=1;
                    Board.GetChild(i).GetChild(j).GetComponent<BoxScript>().SetSprite(shapeIndex);
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
                if (shapeArr[i-PosY,j-PosX]==1)
                {
                    Board.GetChild(i).GetChild(j).GetComponent<BoxScript>().ShowVisual(1, shapeIndex);
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
                    Board.GetChild(i).GetChild(j).GetComponent<BoxScript>().ShowVisual(0, shapeIndex);
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
                if (Board.GetChild(i+1).GetChild(j).GetComponent<BoxScript>()._value==1 && shapeArr[i-PosY,j-PosX]==1)
                {
                    return false;
                }
            }
        }
        return true;
    }
    public bool CanRotate()
    {
        if (PosY >= MaxDepth - shapeArr.GetLength(1) || PosX >= MaxLength - shapeArr.GetLength(0)) return false;
        for (int i = PosY; i < PosY + shapeArr.GetLength(0); i++)
        {
            for (int j = PosX; j < PosX + shapeArr.GetLength(1); j++)
            {
                if (Board.GetChild(i).GetChild(j).GetComponent<BoxScript>()._value == 1 && shapeArr[j - PosX, i - PosY] == 1)
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
        if (CanRotate())
        {
            EraseShape();
            RotateArray(ref shapeArr);
        }
        
    }



private void HandleDownArrowClick()
    {
        // Logic for handling down arrow click
        Debug.Log("Down arrow clicked.");
    }


    private void RotateArray(ref int[,] array)
    {
        int width = array.GetLength(0);
        int height = array.GetLength(1);

        int [,] rotatedArray = new int[height, width];

        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                rotatedArray[j, width - i - 1] = array[i, j];
            }
        }
        
            array = rotatedArray;
    }
    private void ScoreLines()
    {
        int height = Board.childCount;
        int width = Board.GetChild(0).childCount;
        List<int> linesToDestroy = new List<int>(); // Store the indices of the lines to destroy

        for (int i = 0; i < height; i++)
        {
            bool isLineFilled = true;
            for (int j = 0; j < width; j++)
            {
                int value = Board.GetChild(i).GetChild(j).GetComponent<BoxScript>()._value;
                if (value != 1)
                {
                    isLineFilled = false;
                    break;
                }
            }

            if (isLineFilled)
            {
                linesToDestroy.Add(i); // Add the index of the filled line to the list
            }
        }

        // Destroy the lines in reverse order to maintain correct indices after destruction
        for (int i = linesToDestroy.Count - 1; i >= 0; i--)
        {
            int lineIndex = linesToDestroy[i];
            Destroy(Board.GetChild(lineIndex).gameObject);
            Score += 10;
        }

        // Instantiate new objects as the first child for each destroyed line
        for (int i = 0; i < linesToDestroy.Count; i++)
        {
            InstantiateAsFirstChild();
        }
        ScoreText.text = Score.ToString();
    }

    void InstantiateAsFirstChild()
    {
        GameObject newObject = Instantiate(Node); // Instantiate the prefab

        // Set the new object as the first child in the hierarchy
        newObject.transform.SetParent(Board.transform, false);
        newObject.transform.SetAsFirstSibling();
    }
}
