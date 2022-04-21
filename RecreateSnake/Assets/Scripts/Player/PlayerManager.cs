using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    [Header("Not On Prefab")]
    [SerializeField] private GameManager _gameManager;
    public GameManager GameManager{ get{return _gameManager;}}

    [SerializeField] private Camera _cam;
    public Camera Cam{ get{return _cam;}}

    [Header("On Prefab")]
    [SerializeField] private GridMovement _gridMovement;
    public GridMovement GridMovement{ get{return _gridMovement;}}
    
    [Header("Movement Variables")]
    [SerializeField] private bool _isMoving;
    public bool IsMoving{ get{return _isMoving;} set{_isMoving = value;}}
    [SerializeField] private Vector2Int _direction;
    public Vector2Int Direction{ get{return _direction;} set{_direction = value;}}
    
    
}
