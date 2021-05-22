using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public static InputManager inputManager;
    
    PlayerLocomotion playerLocomotion;
    CameraManager cameraManager;
    
    #region 키 값
    public KeyCode forward {get; set;}
    public KeyCode backward {get; set;}
    public KeyCode left {get; set;}
    public KeyCode right {get; set;}
    public KeyCode jump {get; set;}
    public KeyCode option {get; set;}
    public KeyCode mouseRight {get; set;}

    #endregion

    public Vector2 movementInput;

    [Header("입력 상태")]
    public bool jump_Input;
    public bool mouseRight_Input;
    public bool option_Input;


    private void Awake() 
    {
        // 클래스 받아오기
        playerLocomotion = GetComponent<PlayerLocomotion>();
        cameraManager = GetComponent<CameraManager>();

        
        if( inputManager == null)
        {
            DontDestroyOnLoad(gameObject);
            inputManager = this;
        }
        else if(inputManager != this)
        {
            Destroy(gameObject);
        }

        // 움직임 입력
        // forward = (KeyCode) System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("forwardKey", "W"));
        // backward = (KeyCode) System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("backwardKey", "S"));
        // left = (KeyCode) System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("leftKey", "A"));
        // right = (KeyCode) System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("rightKey", "D"));

        jump = (KeyCode) System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("jumpKey", "Space"));

        // 카메라 시점 변경
        mouseRight = (KeyCode) System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("mouseRight", "Mouse1"));

        option = (KeyCode) System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("optionKey", "Escape"));
        
    }



    public void HandleAllInput()
    {
        // 움직임 입력
        HandlePlayerInput();      
        HandleJumpingInput();  
        
        // UI 관련 입력
        HandleUIInput();
        HandleOptionInput();
    }

    private void HandlePlayerInput()
    {
        
        // if(Input.GetKey(InputManager.inputManager.forward))
        // {
        //     HandleMovementInput();
        //     Debug.Log("forward");
        // }    
        // if(Input.GetKey(InputManager.inputManager.backward))
        // {
        //     HandleMovementInput();
        //     Debug.Log("backward");
        // }
        // if(Input.GetKey(InputManager.inputManager.left))
        // {
        //     Debug.Log("left");
        // }
        // if(Input.GetKey(InputManager.inputManager.right))
        // {
        //     Debug.Log("right");
        // }

        movementInput.x = Input.GetAxisRaw("Horizontal");
        movementInput.y = Input.GetAxisRaw("Vertical");

        if(Input.GetKeyDown(InputManager.inputManager.jump))
        {
            jump_Input = true;            
        }        
        
        if(Input.GetKey(InputManager.inputManager.mouseRight))
        {
            Debug.Log("마우스 우클릭");
            mouseRight_Input = true;
        }
        else
        {
            mouseRight_Input = false;
        }
        
    }
    
    private void HandleUIInput()
    {
        if(Input.GetKeyDown(InputManager.inputManager.option))
        {
            option_Input = true;
        }
    }

    private void HandleJumpingInput()
    {
        if(jump_Input)
        {
            jump_Input = false;
            playerLocomotion.HandleJumping();
        }
    }
    
    private void HandleOptionInput()
    {
        if(option_Input)
        {
            option_Input = false;
        }
    }
}
