using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridMovement : MonoBehaviour
{
    [SerializeField] private PlayerManager _playerManager;

    [SerializeField] [Range(0.05f, 0.2f)] private float _timeToMove = 0.2f;
    private Vector2 _origPos, _targetPos;

    private void Update()
    {
        if(!_playerManager.IsMoving)
        {
            StartCoroutine(Move(_playerManager.Direction));
        }
        
    }

    public IEnumerator Move(Vector2 direction)
    {
        _playerManager.IsMoving = true;
        Vector2 currentDirection = new Vector2(direction.x, 0f);

        float elapsedTime = 0;

        _origPos = transform.position;
        _targetPos = _origPos + direction;

        while(elapsedTime < _timeToMove)
        {
            transform.position = Vector2.Lerp(_origPos, _targetPos, (elapsedTime/_timeToMove));
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.position = _targetPos;

        _playerManager.IsMoving = false;

    }
}
