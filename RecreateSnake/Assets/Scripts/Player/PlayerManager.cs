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
    [SerializeField] private Vector3 _currentDirection = new Vector3(0f,0f,0f);
    [SerializeField] private Queue<Vector3> _queuedDirections = new Queue<Vector3>();

    
    [Header("Visuals")]
    [SerializeField] private TrailRenderer _trailRenderer;
    [Tooltip("Includes snake head")][SerializeField] private int _length = 2;
    [SerializeField] private float _baseTime = 0f;
    private float _timePerBlob;
    
    private void Start()
    {
        Setup();
    }
    private void Setup()
    {
        _timePerBlob = _gridMovement.TimeToMove;
        _trailRenderer.time = _baseTime + ((_length-1)*_timePerBlob);
        for(int i = 1; i<_length; i++)
        {
            Vector3 offset = new Vector3(-i,0f,0f);
            _gridMovement.AddBlob(transform.position + offset);
        }        
    }
    public void IncreaseLength()
    {
        _length++;
        _trailRenderer.time += _gridMovement.TimeToMove;
        _gridMovement.AddBlob();
    }
    private bool IsInputOnCurrentDirectionAxis(Vector3 direction)
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
    public Vector3 GetDirection(bool dequeue = false)
    {
        if(_queuedDirections.Count > 0)
        {
            if(dequeue)
            {
                Vector3 direction = _queuedDirections.Dequeue();
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

    public void AddDirection(Vector3 direction, bool clearQueue = false)
    {
        if(clearQueue)
        {
            _queuedDirections.Clear();
        }

        if(Mathf.Abs(direction.x) > 0.5f)
        {
            _queuedDirections.Enqueue(new Vector3(Mathf.Sign(direction.x) * 1f, 0f));
        }
        else if(Mathf.Abs(direction.y) > 0.5f)
        {
            _queuedDirections.Enqueue(new Vector3(0f, Mathf.Sign(direction.y) * 1f));
        }
    }
}
