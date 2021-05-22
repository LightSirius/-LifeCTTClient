using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{

    public void HandleAllInput()
    {
        HandleMovementInput();

        
    }

    private void HandleMovementInput()
    {
        if(Input.GetKey(InputManager.inputManager.forward))
        {
            Debug.Log("forward");
        }    
        if(Input.GetKey(InputManager.inputManager.backward))
        {
            Debug.Log("backward");
        }
        if(Input.GetKey(InputManager.inputManager.left))
        {
            Debug.Log("left");
        }
        if(Input.GetKey(InputManager.inputManager.right))
        {
            Debug.Log("right");
        }
    }

    private void HandleJumpingInput()
    {
        if(Input.GetKey(InputManager.inputManager.jump))
        {
            Debug.Log("jump");
        }
    }
}
