using System;
using UnityEngine;

namespace Anim{
    [System.Serializable]
    public class AnimInfo{
        [Tooltip("Animator의 상태 enum")]
        public Enum state;
        [Tooltip("Animator의 파라미터 값")]
        public ParmeterType parmeterType;
        [Tooltip("Animator의 파라미터 이름")]
        public string stateName;

        public AnimInfo(Enum _state, ParmeterType _type, string _name){
            state = _state;
            parmeterType = _type;
            stateName = _name;
        }
    }
}