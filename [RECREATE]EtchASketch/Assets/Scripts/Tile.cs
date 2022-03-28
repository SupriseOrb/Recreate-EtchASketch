using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{

    [Header("Colors")]
    [SerializeField] private Color _changeColor;
    
    private SpriteRenderer _spriteRenderer;
    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    //When player collides with pixel, change the pixel color to look like it was drawn
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            _spriteRenderer.color = _changeColor;
        }
    }
}
