using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollisionHandler : MonoBehaviour
{
    [SerializeField] private PlayerManager _playerManager;
    [SerializeField] private LayerMask _pickUpMask;
    private void OnTriggerEnter2D(Collider2D other)
    {    
        if(_pickUpMask == (_pickUpMask | (1 << other.gameObject.layer)))
        {
            int points = other.GetComponentInParent<Pickup>().GetPoints();
            _playerManager.IncreaseLength();
            _playerManager.GameManager.UpdateScore(points);
            
        }
    }

}
