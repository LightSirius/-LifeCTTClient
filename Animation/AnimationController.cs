using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Anim;

namespace Anim{
    public enum ParmeterType{
        Integer,
        Float,
        Trigger,
        Boolean
    }

    // T는 무조건 state가 와야 함
    [RequireComponent(typeof(Animator))]
    public abstract class AnimationController : MonoBehaviour
    {
        // public List<AnimInfo> animInfos = new List<AnimInfo>();
        public Enum Current_State { get{ return current_state; }}
        protected Enum current_state;
        protected Dictionary<Enum, AnimInfo> animationState = new Dictionary<Enum, AnimInfo>();
        protected Dictionary<ParmeterType, Action<string, object>> parameterActions = new Dictionary<ParmeterType, Action<string, object>>();

        private Animator animator;

        protected virtual void Start()
        {
            animator = GetComponent<Animator>();

            InitParameterActions();
            InitAnimState();
        }

        private void InitParameterActions(){
            parameterActions.Add(ParmeterType.Trigger, (string stateName, object non) => { animator.SetTrigger(stateName); });
            parameterActions.Add(ParmeterType.Boolean, (string stateName, object boolean) => {animator.SetBool(stateName, (bool)boolean); });
            parameterActions.Add(ParmeterType.Float, (string stateName, object value) => {animator.SetFloat(stateName, (float)value); });
            parameterActions.Add(ParmeterType.Integer, (string stateName, object value) => {animator.SetInteger(stateName, (int)value); });
        }

        protected abstract void InitAnimState();

        public abstract void ChangeState(Enum next_state, Enum type = null, object value = null);
    }
}