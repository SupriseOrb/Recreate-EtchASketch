using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class InputHandler : MonoBehaviour
{
    [SerializeField] private PlayerManager _playerManager;
    [SerializeField] private PlayerInput _playerInput;
    [SerializeField] private string _mouseMoveActionString = "MousePos";
    [SerializeField] private string _mouseClickActionString = "MouseLeftClick";

    private InputAction _mouseMoveAction;
    private InputAction _mouseClickAction;

    [SerializeField] private Vector3 _mouseWorldPosition;

    private void Awake()
    {
        _mouseMoveAction = _playerInput.actions[_mouseMoveActionString];
        _mouseClickAction = _playerInput.actions[_mouseClickActionString];
    }

    private void OnEnable()
    {
        //_mouseMoveAction.started += OnMouseMove;
        _mouseMoveAction.performed += OnMouseMove;
        //_mouseMoveAction.canceled += OnMouseMove;

        _mouseClickAction.started += OnMouseClick;
    }

    private void OnDisable()
    {
        //_mouseMoveAction.started -= OnMouseMove;
        _mouseMoveAction.performed -= OnMouseMove;
        //_mouseMoveAction.canceled -= OnMouseMove;

        _mouseClickAction.started -= OnMouseClick;
    }

    private void OnMouseMove(InputAction.CallbackContext context)
    {
        Vector3 mouseScreenPosition = context.ReadValue<Vector2>();
        _mouseWorldPosition = _playerManager.Cam.ScreenToWorldPoint(mouseScreenPosition);
    }  

    private void OnMouseClick(InputAction.CallbackContext context)
    {
        _playerManager.GameManager.SetValue(_mouseWorldPosition, (int)GridManager.GridValues.Snake);
    }



    
    
}
