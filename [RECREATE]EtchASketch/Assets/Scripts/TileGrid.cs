using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileGrid : MonoBehaviour
{
    [Range(0,100)]
    [SerializeField] private int width, height;
    [SerializeField] private Transform _tilePrefab;
    [SerializeField] private float _tileScale;
    private void Awake()
    {
        Vector2 tileSize = _tilePrefab.GetComponent<Tile>().Size;
        for(int r = 0; r < height; r++)
        {
            for(int c = 0; c < width; c++)
            {
                GameObject tile = Instantiate(_tilePrefab.gameObject,
                            new Vector3(c * _tileScale,
                                        r * _tileScale,
                                        0f),
                            Quaternion.identity,
                            transform);
                tile.transform.localScale = new Vector3(_tileScale, _tileScale, 1f);
            }
        }
        transform.position = new Vector3(_tileScale * (-width/2f + 0.5f),
                                        _tileScale * (-height/2f + 0.5f),
                                        0f);
    }
}
