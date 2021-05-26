using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputKeyManager : MonoBehaviour
{
    PlayerInputs playerInputs;

    public Vector2 movementInput;
    

    private void OnEnable() 
    {
        if(playerInputs == null)
        {
            playerInputs = new PlayerInputs();

            playerInputs.PlayerMovement.Movement.performed += i => movementInput = i.ReadValue<Vector2>();
        }

        playerInputs.Enable();
    }

    private void OnDisable() 
    {
        playerInputs.Disable();
    }
}
