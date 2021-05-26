using UnityEngine;

public class OptimizeBehaviour : MonoBehaviour {

    public GameObject MyGameObject{
        get{
            if (myGameObject == null){
                myGameObject = gameObject;
            }

            return myGameObject;
        }
    }

    public Transform MyTransform{
        get{
            if (myTransform == null){
                myTransform = transform;
            }

            return myTransform;
        }
    }

    protected GameObject myGameObject = null;
    protected Transform myTransform = null;

    protected virtual void Awake(){
        myGameObject = gameObject;
        myTransform = transform;
    }
}