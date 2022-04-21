using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    [SerializeField] private GameManager _gameManager;
    public GameManager GameManager{ get{return _gameManager;}}

    [SerializeField] private Camera _cam;
    public Camera Cam{ get{return _cam;}}
    
    
}
