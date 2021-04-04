using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationController : MonoBehaviour
{

    protected Dictionary<Enum, IState> animationState;

    private Animator animator;

    protected virtual void Start()
    {
        animator = GetComponent<Animator>();    
    }
}
