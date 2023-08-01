using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//this script manage the behaviour of hologram that show when enter is pressed
public class Hologram : MonoBehaviour
{
    [SerializeField] GameObject hologram;//the hologram gameobject
    Vector2 hologramDirectionInput;//arrow key mapped vector
    Vector3 GravityVector;//vector of currently applied vector
    Vector3 globalEularVector;//global value of hologram rotation vector
    Vector3 localEularVector;//local value of hologram rotation vector
    Gravity gravityScript;//script containing objects gravity
    private void Awake()
    {
        gravityScript=FindObjectOfType<Gravity>();//finding the script
        GravityVector= gravityScript.GetGravityVector();//getting the initial gravity vector
    }
    void Start()
    {
        InputManager.instance.OnEnterPressed += Instance_OnEnterPressed;//subscribing to enter pressed event of input manager
    }


    private void Instance_OnEnterPressed(object sender, System.EventArgs e)//enter pressed event execution
    {
        transform.eulerAngles = globalEularVector;//player main rotation set same as hologram rotation
        gravityScript.ChangeGravityVector();//invoke the event of Ongravity change
    }

    private void LateUpdate()
    {
        hologramDirectionInput = InputManager.instance.GetGravityVariable();//getting arrow input

        if (hologramDirectionInput.x == 1)
        {
            GravityVector = Vector3.right;
        }
        else if (hologramDirectionInput.x == -1)
        {
            GravityVector = Vector3.left;
        }
        else if (hologramDirectionInput.y == 1)
        {
            GravityVector = Vector3.up;
        }
        else if (hologramDirectionInput.y == -1)
        {
            GravityVector = Vector3.down;
        }
        if (hologramDirectionInput == Vector2.zero)//hide hologram if there is no input
        {
            hologram.SetActive(false);
        }
        else//if there is an arrow input
        {
            hologram.SetActive(true);//show the hologram
            hologram.TryGetComponent(out Transform hologramTransform);//get the transform component

            if (Mathf.Abs(GravityVector.x) == 1)//show hologram at left or right direction
            { 
                localEularVector = new Vector3(0, 0, GravityVector.x);
            }
            else if (Mathf.Abs(GravityVector.y) == 1)//show hologram at forward or backward direction
            {
                localEularVector = new Vector3(-GravityVector.y, 0, 0);
            }
            localEularVector = localEularVector * 90;//multiplying local eular angle with 90
            hologramTransform.localEulerAngles = localEularVector;//setting up rotation of hologram
            globalEularVector = (hologramTransform.eulerAngles);//setting the global value of rotation
        }
    }
}
