using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

//Handles all the player's input
public class InputHandler : MonoBehaviour
{
    [SerializeField] private PlayerManager _playerManager;
    [SerializeField] private PlayerInput _playerInput;

    [Header("Mouse Test Input")]
    [SerializeField] private string _mouseMoveActionString = "MousePos";
    [SerializeField] private string _mouseClickActionString = "MouseLeftClick";
    private Vector3 _mouseWorldPosition;
    private InputAction _mouseMoveAction;
    private InputAction _mouseClickAction;

    [Header("Movement Input")]
    [SerializeField] private string _playerMovementActionString = "Movement";
    [SerializeField] [Range(0,1f)] private float _maxTimeBetweenInputs = 0.3f;
    [SerializeField] [Range(0,1f)] private float _minTimeBetweenInputs = 0.1f;
    private float _currentInputTime;
    private InputAction _movementAction;

    private void Awake()
    {
        _mouseMoveAction = _playerInput.actions[_mouseMoveActionString];
        _mouseClickAction = _playerInput.actions[_mouseClickActionString];
        _movementAction = _playerInput.actions[_playerMovementActionString];

        _currentInputTime = 0f;
    }

    private void Update()
    {
        if(_currentInputTime < _maxTimeBetweenInputs)
        {
            _currentInputTime+= Time.deltaTime;
        }
    }

    //Subscribe to the various input action events when the input handler is enabled
    private void OnEnable()
    {
        //_mouseMoveAction.started += OnMouseMove;
        _mouseMoveAction.performed += OnMouseMove;
        //_mouseMoveAction.canceled += OnMouseMove;

        _mouseClickAction.started += OnMouseClick;

        //_movementAction.started += OnMovement;
        _movementAction.performed += OnMovement;
        //_movementAction.canceled += OnMovement;

        //_movementAction.started += OnGameStart;
        _movementAction.performed += OnGameStart;
    }

    //Unsubscribe from the various input action events when the input handler is disabled
    private void OnDisable()
    {
        //_mouseMoveAction.started -= OnMouseMove;
        _mouseMoveAction.performed -= OnMouseMove;
        //_mouseMoveAction.canceled -= OnMouseMove;

        _mouseClickAction.started -= OnMouseClick;

        //_movementAction.started -= OnMovement;
        _movementAction.performed -= OnMovement;
        //_movementAction.canceled -= OnMovement;
    }

    private void OnMouseMove(InputAction.CallbackContext context)
    {
        Vector3 mouseScreenPosition = context.ReadValue<Vector2>();
        _mouseWorldPosition = _playerManager.Cam.ScreenToWorldPoint(mouseScreenPosition);
    }  

    private void OnMouseClick(InputAction.CallbackContext context)
    {
        _playerManager.GameManager.SetValue(_mouseWorldPosition, _playerManager.GameManager.GridValueSnake);
    }

    private void OnMovement(InputAction.CallbackContext context)
    {
        
        Vector3 inputVector = context.ReadValue<Vector2>();
        
        if(_currentInputTime <= _maxTimeBetweenInputs && _currentInputTime >= _minTimeBetweenInputs)
        {
            _playerManager.AddDirection(inputVector);
            _currentInputTime = 0f;
        }
        else if(_currentInputTime >= _maxTimeBetweenInputs)
        {
            _playerManager.AddDirection(inputVector, true);
            _currentInputTime = 0f;
        }      
    }

    private void OnGameStart(InputAction.CallbackContext context)
    {
        _playerManager.GameManager.GameStarted = true;
        //_movementAction.started -= OnGameStart;
        _movementAction.performed -= OnGameStart;
    }

    
    
}
