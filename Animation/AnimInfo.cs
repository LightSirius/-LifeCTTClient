using System;
using UnityEngine;

namespace Anim{
    [System.Serializable]
    public class AnimInfo{
        [Tooltip("Animator�� ���� enum")]
        public Enum state;
        [Tooltip("Animator�� �Ķ���� ��")]
        public ParmeterType parmeterType;
        [Tooltip("Animator�� �Ķ���� �̸�")]
        public string stateName;

        public AnimInfo(Enum _state, ParmeterType _type, string _name){
            state = _state;
            parmeterType = _type;
            stateName = _name;
        }
    }
}