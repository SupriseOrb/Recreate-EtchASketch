using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    [Header("Grid Data")]
    [SerializeField] [Range(5,18)] private int _width = 18;
    [SerializeField] [Range(5,10)] private int _height = 10;
    [SerializeField] [Range(0.5f,2f)] private float _cellSize = 1f;
    private GridManager _gameState;

    [Header("UI")]
    [SerializeField] private TextMeshProUGUI _scoreText;
    [SerializeField] private int score = 0;

    private void Start()
    {
        UpdateScore(0);
        _gameState = new GridManager(_width,_height,_cellSize);
    }

    public void SetValue(Vector3 worldPosition, int value)
    {
        _gameState.SetValue(worldPosition, value);
    }

    public void UpdateGridSnakePosition()
    {
        Debug.Log("Update Grid Snake Position");
        //TODO: Update grid using SetValue();
    }

    public void UpdateScore(int points)
    {
        score += points;
        _scoreText.text = "Score: " + score.ToString();
    }
}
