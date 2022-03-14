using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    [Header("Colors")]
    [SerializeField] private Color _baseColor;
    [SerializeField] private Color _changeColor;

    [Header("Misc")]
    [Range(0,30)] private int width, height;
    public Vector2Int Size
    {
        get {return new Vector2Int(height, width);}
    }
    private SpriteRenderer _spriteRenderer;

    //use MaterialPropertyBlock because Renderer.material creates a copy of the material
    private MaterialPropertyBlock _mpb;

    //Cache integer for shader property color
    //More efficient way for color/property assignment
    static readonly int _shaderPropColor = Shader.PropertyToID("_Color");
    public MaterialPropertyBlock Mpb
    {
        get
        {
            if(_mpb == null)
                _mpb = new MaterialPropertyBlock();
            return _mpb;
        }
    }

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        //_baseColor = new Color32(0xB2, 0xB2, 0xB2, 0xFF);
        //_changeColor = Color.black;
    }

    //When player collides with pixel, change the pixel color to look like it was drawn
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            Mpb.SetColor(_shaderPropColor, _changeColor);
            _spriteRenderer.SetPropertyBlock(Mpb);
        }
    }
}
