using System;
using UnityEngine;

[Serializable]
public struct MovementAnimationComponent 
{
    public Animator animator;
    public string animationKey;

    [HideInInspector] public bool currentKey;
}