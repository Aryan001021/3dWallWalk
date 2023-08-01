using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableBoxManager : MonoBehaviour
{
    int collectedBox = 0;//no of boxes collected
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.GetComponent<CollectableBox>())
        {
            Destroy(collision.gameObject);//destroy the blue box
            collectedBox++;//add +1 to variable
        }//collision check if player collided with the blue box
    }
    public int GetNoOfBoxesCollected()//getter function
    {
        return collectedBox;
    }
}
