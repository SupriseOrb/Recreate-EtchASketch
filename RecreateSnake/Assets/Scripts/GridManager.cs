using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager
{
    [SerializeField] [Range(5,18)] private int _width;
    [SerializeField] [Range(5,10)] private int _height;
    [SerializeField] private float _cellSize;
    [SerializeField] private string[,] _gridArray;
    [SerializeField] private float _offSetWidth;
    [SerializeField] private float _offSetHeight;
    public GridManager(int width, int height, float cellSize)
    {
        _width = width;
        _offSetWidth = -1 * (_width/2f);
        _height = height;
        _offSetHeight = -1 * (_height/2f);
        _cellSize = cellSize;

        _gridArray = new string[width, height];

        for(int x = 0; x < _gridArray.GetLength(0); x++)
        {
            for(int y = 0; y < _gridArray.GetLength(1); y++)
            {
                SetValue(x,y,"Empty");
            }
        }


    }

    private Vector3 GetWorldPosition(int x, int y)
    {
        return new Vector3(x+_offSetWidth,y+_offSetHeight) * _cellSize;
    }
    
    public void SetValue(int x, int y, string value)
    {
        if(x >= 0 && y >= 0 && x < _width && y < _height)
        {
            _gridArray[x,y] = value;
        }

        Color color;
        if(value == "Empty")
        {
            color = Color.white;
        }
        else if(value == "Snake")
        {
            color = Color.green;
        }
        else
        {
            color = Color.magenta;
        }
        Debug.DrawLine(GetWorldPosition(x,y), GetWorldPosition(x+1,y+1), color, 2f);
    }

    public void GetXY(Vector3 worldPosition, out int x, out int y)
    {
        x = Mathf.FloorToInt((worldPosition.x / _cellSize) - _offSetWidth);
        y = Mathf.FloorToInt((worldPosition.y / _cellSize) - _offSetHeight);
    }

    public void SetValue(Vector3 worldPosition, string value)
    {
        int x,y;
        GetXY(worldPosition, out x, out y);
        SetValue(x, y, value);
    }

    public string GetValue(Vector3 worldPosition)
    {
        int x,y;
        GetXY(worldPosition, out x, out y);
        if(x >= 0 && y >= 0 && x < _width && y < _height)
        {
            return _gridArray[x,y];
        }
        else
        {
            return "Wall";
        }
        
    }

    
}
