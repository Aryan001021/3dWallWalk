using System.Collections;
using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
//this script manager all the input 

public class InputManager : MonoBehaviour
{
    public event EventHandler OnJumpPressed;//jump button pressed event
    public event EventHandler OnEnterPressed;//enter button pressed event
    public static InputManager instance { private set; get; }//instance of this script
    Vector2 gravityVariable= new Vector2(0, 0);//arrow key mapping for gravity change
    Vector2 movementVector = new Vector2(0, 0);//wasd key mapping for movement change

    private void Start()
    {
        if (instance == null)
        {
            instance = this;
        }//setting singleton pattern
        else
        {
            Destroy(this.gameObject);
        }//setting singleton pattern
    }
    void Update()
    {
        InputForGravityChange();//arrow key mapping
        InputForMovement();//wasd mapping
        if(Input.GetKeyDown(KeyCode.Space))
        {
            OnJumpPressed?.Invoke(this, EventArgs.Empty);//jump pressed event fired
        }
        if (Input.GetKeyDown(KeyCode.Return))
        {
            OnEnterPressed?.Invoke(this, EventArgs.Empty);//enter pressed event fired
        }
    }

    private void InputForMovement()
    {
        movementVector = new Vector2(0, 0);
        if (Input.GetKey(KeyCode.W))
        {
            movementVector += new Vector2(0, 1);
        }
        if (Input.GetKey(KeyCode.A))
        {
            movementVector += new Vector2(-1, 0);
        }
        if (Input.GetKey(KeyCode.S))
        {
            movementVector += new Vector2(0, -1);
        }
        if (Input.GetKey(KeyCode.D))
        {
            movementVector += new Vector2(1, 0);
        }
    }//movement Vector mapping

    private void InputForGravityChange()
    {
        gravityVariable= Vector2.zero;
        if (Input.GetKey(KeyCode.DownArrow))
        {
            gravityVariable = new Vector2(0, -1);
        }
        else if (Input.GetKey(KeyCode.UpArrow))
        {
            gravityVariable = new Vector2(0, 1);
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            gravityVariable = new Vector2(1, 0);
        }
        else if (Input.GetKey(KeyCode.LeftArrow))
        {
            gravityVariable = new Vector2(-1, 0);
        }
    }//arrow key mapping
    public Vector2 GetGravityVariable()
    {
        return gravityVariable;
    }//Gravity vector getter function
    public Vector2 GetMovementVector() 
    {
        return movementVector;
    }//movement vector getter function
}
 