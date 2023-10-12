using System;
using UnityEngine;

[Serializable]
public struct CarryingAnimationComponent 
{
    public Animator animator;
    public string animationKey;

    [HideInInspector] public bool currentKey;
}