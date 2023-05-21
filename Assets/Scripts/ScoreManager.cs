using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public Text scoreValueText;
    public float scoreValue = 0f;
    public float pointIncreasedPerSecond = 1f;
    
    void FixedUpdate()
    {
        //ok ill try a bool alr tysm
        scoreValueText.text = ((int)scoreValue).ToString();
        scoreValue += pointIncreasedPerSecond * Time.fixedDeltaTime;
    }
}
