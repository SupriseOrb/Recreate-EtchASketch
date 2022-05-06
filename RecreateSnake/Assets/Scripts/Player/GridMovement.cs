using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridMovement : MonoBehaviour
{
    [SerializeField] private PlayerManager _playerManager;

    [Header("Snake Movement")]
    [SerializeField] [Range(0.05f, 0.2f)] private float _timeToMove = 0.2f;
    public float TimeToMove { get{return _timeToMove;}}
    private Vector3 _origPos, _targetPos;

    //Start the player lerp movement if it hasn't started yet
    private void Update()
    {
        if(!_playerManager.IsMoving && _playerManager.GameManager.GameStarted)
        {
            StartCoroutine(Move(_playerManager.GetDirection(true)));
        }
    }

    public IEnumerator Move(Vector3 direction)
    {
        _playerManager.IsMoving = true;
        Vector3 currentDirection = new Vector3(direction.x, 0f, 0f);

        float elapsedTime = 0;

        _origPos = transform.position;
        _targetPos = _origPos + direction;
        _playerManager.GameManager.UpdateGameState(_targetPos);
        while(elapsedTime < _timeToMove)
        {
            transform.position = Vector3.Lerp(_origPos, _targetPos, (elapsedTime/_timeToMove));
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.position = _targetPos;

        _playerManager.IsMoving = false;

    }
}
