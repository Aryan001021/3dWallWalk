using System;
using UnityEngine;
//this script change the gravity of connected gameobject

public class Gravity : MonoBehaviour
{
    public event EventHandler<OnGravityChangedEventArgs> OnGravityChanged;//event fired of when gravity is changed
    public class OnGravityChangedEventArgs : EventArgs 
    {
        public Vector3 gravityVector;
    }//data carried by ongravitychanged event
    [SerializeField]Vector3 gravityVector = new Vector3(0,0,-1);//gameobject gravity vector
    Rigidbody rb;
   
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.useGravity = false;
        InputManager.instance.OnEnterPressed += Instance_OnEnterPressed;//subscribe to enter event of input manager
    }

    private void Instance_OnEnterPressed(object sender, System.EventArgs e)//function triggered whenever enter is pressed
    {

        OnGravityChanged?.Invoke(this, new OnGravityChangedEventArgs
        {
            gravityVector = gravityVector,
        });//Ongravity event get fired containing gravity vector
    }
    private void LateUpdate()
    {
        rb.AddForce(gravityVector * 9.81f, ForceMode.Acceleration);//the gravity applied on gameObject
    }
    public Vector3 GetGravityVector()
    {
        return gravityVector;
    }//gravity vector getter function
    public void ChangeGravityVector()
    {
        gravityVector = -transform.up;
        OnGravityChanged?.Invoke(this, new OnGravityChangedEventArgs
        {
            gravityVector = gravityVector,
        });//change gravity vector and send ongravitychangedevent
    }
}
