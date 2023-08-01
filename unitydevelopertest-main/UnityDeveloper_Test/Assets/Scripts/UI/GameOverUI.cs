using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//used to show gameover screen
public class GameOverUI : MonoBehaviour
{
    public static GameOverUI instance { get;private set; } //singleton patern
    [SerializeField] GameObject gameOverImage;//gameover screen
    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
    public void GameOverScreen()//turn on game over screen
    {
        gameOverImage.SetActive(true);
    }
}
