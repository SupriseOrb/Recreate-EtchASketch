using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    [Header("Outside Prefab")]
    [SerializeField] private PlayerManager _playerManager;
    [SerializeField] private TextMeshProUGUI _scoreText;
    [SerializeField] private SpriteRenderer _pickupSpriteRenderer;

    [Header("Grid Data")]
    [SerializeField] [Range(5,18)] private int _width = 18;
    [SerializeField] [Range(5,10)] private int _height = 10;
    [SerializeField] [Range(0.5f,2f)] private float _cellSize = 1f;
    private GridManager _gameState;

    [Header("UI")]
    [SerializeField] private int score = 0;

    #region Grid Values
    [Header("Grid Values")]
    [SerializeField] private List<PickupData> _pickupValues;
    private PickupData _currentPickup;
    private Dictionary<string, PickupData> _pickupDict;
    public string GridValueSnake{ get{return "Snake";}}
    private string GridValueWall{ get{return "Wall";}}
    private string GridValueEmpty{ get{ return "Empty";}}
    #endregion

    //Holds the world space positions of each snake tile
    private Queue<Vector3> _snakeBlobPositions;
    private bool _gameStarted = false;
    public bool GameStarted { get{return _gameStarted;} set{_gameStarted = value;}}

    private void Awake()
    {
        _pickupDict = new Dictionary<string, PickupData>();
        for(int i = 0; i< _pickupValues.Count; i++)
        {
            _pickupDict[_pickupValues[i].name] = _pickupValues[i];
        }
    }
    private void Start()
    {
        UpdateScore(0);
        _gameState = new GridManager(_width,_height,_cellSize);
        UpdateGameStartState();
        UpdateCurrentPickup();
        SpawnPickup();
    }

    
    private void UpdateCurrentPickup(PickupData pickupData = null)
    {
        if(pickupData == null)
        {
            int randomIndex = Random.Range(0, _pickupValues.Count);
            _currentPickup = _pickupValues[randomIndex];
        }
        else
        {
            _currentPickup = pickupData;
        }
    }

    //"Spawns" a pickup in a random position
    private void SpawnPickup()
    {
        //Call function that calculates the position of the new pickup
        Vector3 position = _gameState.GetRandomEmptyPosition();
        SpawnPickup(position);
    }

    //"Spawns" a pickup in a certain position
    //Method: changes the pickup prefab sprite renderer's position and sprite based on our current pickup data
    private void SpawnPickup(Vector3 position)
    {
        //"Spawns" pickup in world space
        _pickupSpriteRenderer.transform.position = position + new Vector3(0.5f,0.5f,0);
        if(_currentPickup == null)
        {
            _currentPickup = _pickupValues[0];
        }

        _pickupSpriteRenderer.sprite = _currentPickup.Sprite;

        //"Spawn" pickup in game state
        SetValue(position, _currentPickup.name);
    }

    
    private void UpdateGameStartState()
    {
        Vector3 playerPosition = _playerManager.transform.position;
        _snakeBlobPositions = new Queue<Vector3>();
        for(int i = _playerManager.SnakeLength-1; i >=0; i--)
        {
            Vector3 calculatedPosition = new Vector3(playerPosition.x - i, playerPosition.y, playerPosition.z);
            SetValue(calculatedPosition, GridValueSnake);
            _snakeBlobPositions.Enqueue(calculatedPosition);
        }
    }

    public void SetValue(Vector3 worldPosition, string name)
    {
        _gameState.SetValue(worldPosition, name);
    }

    //Called when the snake moves to a new spot in the grid
    //Position is the new world space position of the snake head
    public void UpdateGameState(Vector3 position)
    {
        string value = _gameState.GetValue(position);
        if ((value == GridValueSnake && position != _snakeBlobPositions.Peek()) || value == GridValueWall)
        {           
            if(_playerManager.SnakeLength == _gameState.TileCount)
            {
                GameOver(win:true);
            }
            else
            {
                GameOver(win:false);
            }
            return;
        }
        else if(_pickupDict.ContainsKey(value))
        {
            int points = _pickupDict[value].Points;
            _playerManager.IncreaseLength();
            UpdateScore(points);
            _gameState.SetValue(position, GridValueSnake);
            _snakeBlobPositions.Enqueue(position);
            SpawnPickup();
        }
        else
        {
            _gameState.SetValue(position, GridValueSnake);            
            _snakeBlobPositions.Enqueue(position);
            Vector3 removeSnakePosition = _snakeBlobPositions.Dequeue();
            _gameState.SetValue(removeSnakePosition, GridValueEmpty);
        }
    }
    private void GameOver(bool win)
    {
        Time.timeScale = 0f;
        if(win)
        {
            Debug.Log("You win!");
        }
        else
        {
            Debug.Log("You lose :(");
        }
    }

    public void UpdateScore(int points)
    {
        score += points;
        if(_scoreText!= null)
        {
            _scoreText.text = "Score: " + score.ToString();
        }
        
    }
}
