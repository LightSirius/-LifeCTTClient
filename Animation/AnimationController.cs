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
    public abstract class AnimationController<T> : MonoBehaviour
    {
        [Tooltip("플레이어 애니메이션 정보")]
        public List<AnimInfo<T>> animInfos = new List<AnimInfo<T>>();
        public T Current_State { get{ return current_state; }}
        protected T current_state;
        protected Dictionary<T, AnimInfo<T>> animationState = new Dictionary<T, AnimInfo<T>>();
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

        private void InitAnimState(){
            for (int i = 0; i < animInfos.Count; i++){
                animationState.Add(animInfos[i].state, animInfos[i]);
            }
        }

        public abstract void ChangeState(T next_state, object value = null);
    }
}