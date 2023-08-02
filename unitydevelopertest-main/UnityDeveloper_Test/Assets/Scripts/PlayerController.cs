using System.Collections;
using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
//this script handle player movement and player animation
public class PlayerController : MonoBehaviour
{
    [SerializeField] int movSpeed = 10;
    [SerializeField] int jumpSpeed = 10;
    [SerializeField] Animator animator;
    [SerializeField] GameObject playerVisual;
    [SerializeField] LayerMask groundLayerMask;//physics layer to find ground
    [SerializeField] float groundCheckDistance;//distance for raycast
    [SerializeField] int maxTimeForPlayerNotTouchingFloor=5;//for always in air lose condition
    Gravity gravityScript;
    float noOfSecondsOfPlayerNotTouchGround;//for timer for air lose condition timer
    float jumpTimer = 0;
    Vector3 playerGravityVector;
    Vector3 rotationOfPlayerVisualVector;//player visual rotation vector
    bool onGround = true;
    bool jumpState=false;
    //variable for animator
    const string idle = "Idle";
    const string run = "Running";
    const string jump = "Falling Idle";
    //\variable for animator
    Rigidbody rb;
    private void OnCollisionEnter(Collision collision)
    {
        if (!onGround)
        {
            if (Physics.Raycast(transform.position, -transform.up, groundCheckDistance, groundLayerMask))
            {
                onGround = true;
                noOfSecondsOfPlayerNotTouchGround = 0;
            }
            
        }//this changes onground and reset no of seconds player not touch ground 
              
    }
    private void OnCollisionStay(Collision collision)
    {
        if(!onGround )
        {
            if (Physics.Raycast(transform.position, -transform.up, groundCheckDistance, groundLayerMask)&& (int)jumpTimer==1)
            {
                jumpTimer += Time.deltaTime;
                onGround = true;
                noOfSecondsOfPlayerNotTouchGround = 0;
                jumpTimer = 0;
                jumpState = false;
            }
            else
            {
                jumpTimer += Time.deltaTime;
            }
        }
    }//jump reset logic
    private void OnCollisionExit(Collision collision)
    {
        if (onGround)
        {
            if (!Physics.Raycast(transform.position, -transform.up, groundCheckDistance, groundLayerMask))
            {
                onGround = false;
                noOfSecondsOfPlayerNotTouchGround = 0;
            }
        }
    }//if player fall it will make change onground to false
    private void Awake()
    {
        noOfSecondsOfPlayerNotTouchGround = 0;
        gravityScript = FindObjectOfType<Gravity>();//finding gravity script
        playerGravityVector = gravityScript.GetGravityVector();//setting playergravityvector
        gravityScript.OnGravityChanged += PlayerGravity_OnGravityChanged;//subscribing to on gravity change event
    }

    private void PlayerGravity_OnGravityChanged(object sender, Gravity.OnGravityChangedEventArgs e)
    {
       playerGravityVector = e.gravityVector;
    }//whenever ongravitychange event occur

    private void Start()
    {
        InputManager.instance.OnJumpPressed += Instance_OnJumpPressed;
        rb = GetComponent<Rigidbody>();
    }

    private void Instance_OnJumpPressed(object sender, System.EventArgs e)
    {
        if (onGround)
        {
            rb.AddForce(rb.velocity+transform.up*jumpSpeed*Time.deltaTime,ForceMode.Impulse);
            onGround = false;
            jumpState = true;
        }//this make player jump with rigidbody add force
    }

    private void LateUpdate()
    {
        MovementBase();
        if (!onGround)
        {
            noOfSecondsOfPlayerNotTouchGround += Time.deltaTime;
            if ((int)noOfSecondsOfPlayerNotTouchGround == maxTimeForPlayerNotTouchingFloor)
            {
                GameOverUI.instance.GameOverScreen();
            }
        }//make game over if player is not touching floor for long time
    }

    private void MovementBase()
    {
        Vector3 movementVector = new Vector3(0, 0, 0);
        Vector2 tempMovementVector = InputManager.instance.GetMovementVector();
            if (Mathf.Sign(tempMovementVector.x) == 1 && tempMovementVector.x != 0)
            {
                movementVector += transform.right;
            }
            if (Mathf.Sign(tempMovementVector.x) == -1 && tempMovementVector.x != 0)
            {
                movementVector += -transform.right;
            }
            if (Mathf.Sign(tempMovementVector.y) == 1 && tempMovementVector.y != 0)
            {
                movementVector += transform.forward;
            }
            if (Mathf.Sign(tempMovementVector.y) == -1 && tempMovementVector.y != 0)
            {
                movementVector += -transform.forward;
            }
      
        //here to rotate visual
        VisualRotation(tempMovementVector);
        //till here

        if (onGround)
        {
            if (tempMovementVector != Vector2.zero)
            {
                animator.Play(run);
            }
            else
            {
                animator.Play(idle);
            }
        }//animation handle here with onground bool and tempMovementVector
        if (!onGround)
        {
            animator.Play(jump);
        }
        transform.position += movementVector * movSpeed * Time.deltaTime;

    }//player movement logic

    private void VisualRotation(Vector2 tempMovementVector)
    {
        if (tempMovementVector == Vector2.left)
        {
            rotationOfPlayerVisualVector = Vector3.up * -90;
        }
        else if (tempMovementVector == Vector2.right)
        {
            rotationOfPlayerVisualVector = Vector3.up * 90;
        }
        else if (tempMovementVector == Vector2.up)
        {
            rotationOfPlayerVisualVector = Vector3.up * 0;
        }
        else if (tempMovementVector == Vector2.down)
        {
            rotationOfPlayerVisualVector = Vector3.up * 180;
        }
        if (playerVisual.TryGetComponent(out Transform playerVisualTransform))
        {
            playerVisualTransform.localEulerAngles = rotationOfPlayerVisualVector;
        }
    }//player visual look in direction on differnt axis
}
