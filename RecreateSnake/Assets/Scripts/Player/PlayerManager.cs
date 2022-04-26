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
    
    [Header("Movement Variables")]
    [SerializeField] private GridMovement _gridMovement;
    public GridMovement GridMovement{ get{return _gridMovement;}}
    [SerializeField] private bool _isMoving;
    public bool IsMoving{ get{return _isMoving;} set{_isMoving = value;}}
    [SerializeField] private Vector2 _currentDirection = new Vector2(0f,0f);
    [SerializeField] private Queue<Vector2> _queuedDirections = new Queue<Vector2>();

    [Header("Visuals")]
    [SerializeField] private int _length = 1;

    public void IncreaseLength()
    {
        _length++;
        Debug.Log("Update Visuals");
        //TODO: Update the visuals of snake
        _gameManager.UpdateGridSnakePosition();
    }
    private bool IsInputOnCurrentDirectionAxis(Vector2 direction)
    {
        if((Mathf.Abs(direction.x) == 1f && Mathf.Abs(_currentDirection.x) ==1f) ||
            (Mathf.Abs(direction.y) == 1f && Mathf.Abs(_currentDirection.y) ==1f))
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    public Vector2 GetDirection(bool dequeue = false)
    {
        if(_queuedDirections.Count > 0)
        {
            if(dequeue)
            {
                Vector2 direction = _queuedDirections.Dequeue();
                if(!IsInputOnCurrentDirectionAxis(direction))
                {
                    _currentDirection = direction;
                }
            }
            else
            {
                return _queuedDirections.Peek();
            }
        }

        return _currentDirection;
    }

    public void AddDirection(Vector2 direction, bool clearQueue = false)
    {
        if(clearQueue)
        {
            _queuedDirections.Clear();
        }

        if(Mathf.Abs(direction.x) > 0.5f)
        {
            _queuedDirections.Enqueue(new Vector2(Mathf.Sign(direction.x) * 1f, 0f));
        }
        else if(Mathf.Abs(direction.y) > 0.5f)
        {
            _queuedDirections.Enqueue(new Vector2(0f, Mathf.Sign(direction.y) * 1f));
        }
    }
}
