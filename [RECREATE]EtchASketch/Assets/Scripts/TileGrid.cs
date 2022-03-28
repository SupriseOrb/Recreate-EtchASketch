using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileGrid : MonoBehaviour
{
    [Header("Dimensions")]
    [Range(0,30)] [SerializeField] private int _width;
    [Range(0,30)] [SerializeField] private int _height;
    private Transform[,] _gridArray;

    [Header("Tile")]
    //Based on 32 PPU, and 32 x 32 pixels
    [SerializeField] private float _tileScale;
    [SerializeField] private Transform _tilePrefab;

    private void Awake()
    {
        CreateNewGridArray();
    }

    public void CreateNewGridArray()
    {
        if(_gridArray != null)
        {
           for(int x = 0; x < _gridArray.GetLength(0); x++)
            {
                for (int y = 0; y < _gridArray.GetLength(1); y++)
                {
                    Destroy(_gridArray[x,y].gameObject);
                }
            }
            transform.position = Vector3.zero;
        }

        _gridArray = new Transform[_width, _height];

        for(int x = 0; x < _gridArray.GetLength(0); x++)
        {
            for (int y = 0; y < _gridArray.GetLength(1); y++)
            {
                _gridArray[x,y] = CreateTile(x,y);
            }
        }

        //Offset Grid Position
        transform.position = new Vector3(-1*_width/2.0f + 0.5f, -1*_height/2.0f +0.5f);        
    }

    //Get the tile's world position based on it's index in the array
    private Vector3 GetWorldPosition(int x, int y)
    {
        return new Vector3(x,y) * _tileScale;
    }

    //Instantiates a tile prefab in world space
    private Transform CreateTile(int x, int y)
    {
        GameObject tile = Instantiate(_tilePrefab.gameObject,
                                            GetWorldPosition(x,y),
                                            Quaternion.identity,
                                            transform);
                tile.transform.localScale *= _tileScale;
        return tile.transform;
    }
}
