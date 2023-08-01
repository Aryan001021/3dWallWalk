
using UnityEngine;
using TMPro;
//this script show timer ui and gameover screen
public class TimerUI : MonoBehaviour
{
    [SerializeField]TextMeshProUGUI timerText;//timer text
    [SerializeField] CollectableBoxManager collectableBoxManager;//getting box manager script
    private void Start()
    {
        Timer.instance.OnUpdateTime += Instance_OnUpdateTime;//subscibe to onupdatetime of timer script
    }

    private void Instance_OnUpdateTime(object sender, Timer.OnUpdateTimeEventArgs e)
    {
        timerText.text=$"Time Remain: {e.collectingBoxesTime.ToString()}";//show the time on screen
        if (e.collectingBoxesTime == 0)
        {
            if (collectableBoxManager.GetNoOfBoxesCollected() != 5)//if player do not collect 5 box shows gameover screen
            {
                GameOverUI.instance.GameOverScreen();
         
            }
            else//show win screen or otherthings
            {
                
            }
        }
    }
}
