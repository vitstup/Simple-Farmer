using System;
using UnityEngine;

[Serializable]
public struct MovableComponent 
{
    public CharacterController controller;
    public float speed;

    [HideInInspector] public bool isMoving;
}