using UnityEngine;

public class GridManager
{
    [SerializeField] [Range(5,18)] private int _width;
    [SerializeField] [Range(5,10)] private int _height;
    [SerializeField] private float _cellSize;
    [SerializeField] private GridValues[,] _gridArray;
    [SerializeField] private float _offSetWidth;
    [SerializeField] private float _offSetHeight;
    public enum GridValues
    {
        Empty = 0,
        Snake = 1,
        Pickup = 2
    }

    public GridManager(int width, int height, float cellSize)
    {
        _width = width;
        _offSetWidth = -1 * (_width/2f);
        _height = height;
        _offSetHeight = -1 * (_height/2f);
        _cellSize = cellSize;

        _gridArray = new GridValues[width, height];

        for(int x = 0; x < _gridArray.GetLength(0); x++)
        {
            for(int y = 0; y < _gridArray.GetLength(1); y++)
            {
                SetValue(x,y,0);
                Debug.DrawLine(GetWorldPosition(x,y), GetWorldPosition(x, y+1), Color.white, 100f);
                Debug.DrawLine(GetWorldPosition(x,y), GetWorldPosition(x+1, y), Color.white, 100f);
            }
        }
        Debug.DrawLine(GetWorldPosition(0,height), GetWorldPosition(width,height), Color.white, 100f);
        Debug.DrawLine(GetWorldPosition(width,0), GetWorldPosition(width,height), Color.white, 100f);
    }

    private Vector3 GetWorldPosition(int x, int y)
    {
        return new Vector3(x+_offSetWidth,y+_offSetHeight) * _cellSize;
    }
    
    public void SetValue(int x, int y, int value)
    {
        if(x >= 0 && y >= 0 && x < _width && y < _height && System.Enum.IsDefined(typeof(GridValues), value))
        {
            _gridArray[x,y] = (GridValues)value;
            Debug.DrawLine(GetWorldPosition(x,y), GetWorldPosition(x, y+1), Color.red, 100f);
            Debug.DrawLine(GetWorldPosition(x,y), GetWorldPosition(x+1, y), Color.red, 100f);
            Debug.DrawLine(GetWorldPosition(x+1,y+1), GetWorldPosition(x, y+1), Color.red, 100f);
            Debug.DrawLine(GetWorldPosition(x+1,y+1), GetWorldPosition(x+1, y), Color.red, 100f);
        }
    }

    private void GetXY(Vector3 worldPosition, out int x, out int y)
    {
        x = Mathf.FloorToInt((worldPosition.x / _cellSize) - _offSetWidth);
        y = Mathf.FloorToInt((worldPosition.y / _cellSize) - _offSetHeight);
    }

    public void SetValue(Vector3 worldPosition, int value)
    {
        int x,y;
        GetXY(worldPosition, out x, out y);
        SetValue(x, y, value);
    }
}
