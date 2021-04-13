using UnityEngine;

namespace Anim{
    [System.Serializable]
    public class AnimInfo<T>{
        [Tooltip("Animator�� ���� enum")]
        public T state;
        [Tooltip("Animator�� �Ķ���� ��")]
        public ParmeterType parmeterType;
        [Tooltip("Animator�� �Ķ���� �̸�")]
        public string stateName;
    }
}