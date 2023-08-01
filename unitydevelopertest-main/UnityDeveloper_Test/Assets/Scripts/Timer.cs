using System;
using UnityEngine;
//timer for 2 minute counter till game is over
public class Timer : MonoBehaviour
{
    public static Timer instance {get; private set; }
    public event EventHandler<OnUpdateTimeEventArgs> OnUpdateTime;//event to update ui
    public class OnUpdateTimeEventArgs : EventArgs
    {
        public int collectingBoxesTime;
    }//data for onupdatetime event
    [SerializeField] int maxTimeMinute=2;//timer time in min
    float maxTimeSecond;
    float currentTime;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }//singleton patern
    }
    private void Start()
    {
        maxTimeSecond = maxTimeMinute * 60;//setting up max time in second
        currentTime = maxTimeSecond;//making current time equl to maxtimesec
    }

    void LateUpdate()
    {
        currentTime = currentTime -Time.deltaTime;//subtract delta time from current time
        if ((int)currentTime<(int)maxTimeSecond)//enter only when one second is passed
        {
            maxTimeSecond = currentTime;//setting maxtimesecond to current time
            OnUpdateTime?.Invoke(this,new OnUpdateTimeEventArgs//firing of event for updating up
            {
                collectingBoxesTime=((int)maxTimeSecond)
            });
        }
        
    }
}
