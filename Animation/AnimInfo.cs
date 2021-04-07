using UnityEngine;

namespace Anim{
    [System.Serializable]
    public class AnimInfo<T>{
        [Tooltip("Animator의 상태 enum")]
        public T state;
        [Tooltip("Animator의 파라미터 값")]
        public ParmeterType parmeterType;
        [Tooltip("Animator의 파라미터 이름")]
        public string stateName;
    }
}