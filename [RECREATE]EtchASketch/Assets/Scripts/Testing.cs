using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Testing : MonoBehaviour
{
    static private PlayerInput _playerInput;

    [Header("Testing: Tile Grid")]
    [SerializeField] private TileGrid _tileGrid;
    [SerializeField] private string _resetGridActionString;
    private InputAction _resetGridAction;


    private void Awake()
    {
        if(_playerInput == null)
        {
            _playerInput = gameObject.GetComponent<PlayerInput>();
        }
        _resetGridAction =_playerInput.actions[_resetGridActionString];
    }

    private void OnEnable()
    {
        _resetGridAction.performed += OnResetGrid;
    }

    private void OnDisable()
    {
        _resetGridAction.performed -= OnResetGrid;
    }
    private void OnResetGrid(InputAction.CallbackContext context)
    {
        _tileGrid.CreateNewGridArray();
    }
}
