using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public Text scoreValueText;
    public float scoreValue = 0f;
    public float pointIncreasedPerSecond = 1f;
    bool isInGame;
    public GameManager gameManager;
    
    void FixedUpdate()
    {
        if (gameManager.gameState == GameManager.State.InGame)
        {
            Debug.Log("yoitsstarting");
            scoreValueText.text = ((int)scoreValue).ToString();
            scoreValue += pointIncreasedPerSecond * Time.fixedDeltaTime; 
        } 
    }
}
