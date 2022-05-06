using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Pickup Data")]
public class PickupData : ScriptableObject
{
    [SerializeField] [Range(0,30)] private int _points;
    public int Points{ get{return _points;}}
    [SerializeField] private Sprite _sprite;
    public Sprite Sprite{ get{return _sprite;}}
    
}
