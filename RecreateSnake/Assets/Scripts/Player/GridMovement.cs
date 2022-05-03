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

    [Header("Snake Blob")]
    [SerializeField] private Transform _snakeBlobPrefab;
    [SerializeField] private Transform _snakeBlobParent;
    //Holds the transformations of each snake tile
    private List<Transform> _snakeBlobPositions = new List<Transform>();

    //Start the player lerp movement if it hasn't started yet
    private void Update()
    {
        if(!_playerManager.IsMoving)
        {
            StartCoroutine(Move(_playerManager.GetDirection(true)));
        }
    }

    //Add a snake body game object at the given world space position
    public void AddBlob(Vector3 position)
    {
        /*Transform blob = Instantiate(_snakeBlobPrefab, _snakeBlobParent);
        blob.position = position;
        _snakeBlobPositions.Add(blob);
        //TODO: Update grid game state with new blob's information
        _playerManager.GameManager.UpdateGameState(position, GridManager.GridValues.Snake);
        */
    }

    //Adds a blob to the snake
    //It will calculate the new blob's position based on the last 2 blobs
    //Snake must have at least a length of 2
    public void AddBlob()
    {
        /*Vector3 lastBlobPosition = _snakeBlobPositions[_snakeBlobPositions.Count-1].position;
        Vector3 secondToLastBlobPosition = _snakeBlobPositions[_snakeBlobPositions.Count-2].position;
        Vector3 nextAvailablePosition = _playerManager.GameManager.NextAvailablePosition(lastBlobPosition, secondToLastBlobPosition);
        AddBlob(nextAvailablePosition);*/
    }

    public IEnumerator Move(Vector3 direction)
    {
        _playerManager.IsMoving = true;
        Vector3 currentDirection = new Vector3(direction.x, 0f, 0f);

        float elapsedTime = 0;

        _origPos = transform.position;
        _targetPos = _origPos + direction;

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
