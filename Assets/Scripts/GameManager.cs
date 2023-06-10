using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.IO;

public class GameManager : MonoBehaviour
{
    public enum State
    {
        Start, InGame, Death
    }

    public delegate void GameManagerEvent();
    public static event GameManagerEvent OnStartGame;

    public static GameManager instance;
    public PlayerController playerController;
    public Animator playerAnimator;
    public LevelManager levelManager;

    [Header("UI Elements")]
    public GameObject startUI;
    public GameObject deathUI;

    [Header("Death UI")]
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI coinText;
    public TextMeshProUGUI highscoreText;
    public TextMeshProUGUI totalCoinText;
    // public GameObject highscoreLabel;

    public State gameState;
    int score;    // score of the current round
    int coin; // coin of the current round
    static string saveFile; // the path for file we save on the hard drive
    SaveData gameData;

    void Start()
    {
        // singleton
        if (instance == null)
		{
            instance = this;
            DontDestroyOnLoad(gameObject);
		}
        else if (instance != this)
		{
            DestroyImmediate(this);
		}

        // save data
        ChangeStateToStart();

        saveFile = Application.persistentDataPath + "/gamedata.data";
        ReadSaveFile();

        // initialization
        levelManager.enabled = false;
    }

    void Update()
    {
        
    }

    public void OnJump()
	{
        if (gameState == State.Start)
        {
            ChangeStateToInGame();
        }
        else if (gameState == State.Death)
        {
            ChangeStateToInGame();
        }
    }

    void ChangeStateToStart()
    {
        // OnStartGame?.Invoke();
        gameState = State.Start;
        playerController.enabled = false;
        playerAnimator.StopPlayback();
        playerAnimator.SetTrigger("Reset");

        startUI.SetActive(true);
        deathUI.SetActive(false);
    }
    void ChangeStateToInGame()
    {
        OnStartGame?.Invoke();
        gameState = State.InGame;
        playerController.enabled = true;
        playerAnimator.StopPlayback();
        playerAnimator.SetTrigger("Start");
        levelManager.enabled = true;

        startUI.SetActive(false);
        deathUI.SetActive(false);
    }
    public void ChangeStateToDeath()
    {
        gameState = State.Death;
        playerController.enabled = false;
        playerAnimator.StartPlayback();

        startUI.SetActive(false);
        deathUI.SetActive(true);



        // Update stats and UI
        score = (int) playerController.transform.position.z;

        scoreText.text = score.ToString();
        coinText.text = coin.ToString();

        gameData.coin += coin;
        gameData.highscore = (score > gameData.highscore) ? score : gameData.highscore;
        
        highscoreText.text = gameData.highscore.ToString();
        totalCoinText.text = gameData.coin.ToString();

        WriteSaveFile();
    }

    public void CollectCoin()
	{
        coin++;
    }


    void ReadSaveFile()
	{
        if (File.Exists(saveFile))
        {
            string fileContents = File.ReadAllText(saveFile);
            gameData = JsonUtility.FromJson<SaveData>(fileContents);
        }
        else
		{
            gameData = new SaveData();
        }
    }
    void WriteSaveFile()
	{
        string jsonString = JsonUtility.ToJson(gameData);
        File.WriteAllText(saveFile, jsonString);
        Debug.Log("saved data to " + saveFile);
    }
}
