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
    public float speedIncrease = 1;
    public float speedForward = 10f;


    void Update ()
    {
        //speedForward += speedIncrease * Time.deltaTime / 60;
    }
    void FixedUpdate()
    {
        if (gameManager.gameState == GameManager.State.InGame)
        {
            speedForward += speedIncrease * Time.deltaTime / 60;
            Debug.Log("yoitsstarting");
            scoreValueText.text = ((int)scoreValue).ToString();
            scoreValue += speedForward * Time.fixedDeltaTime; 
        } 
    }
}
