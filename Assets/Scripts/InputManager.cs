using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public GameManager gameManager;
    public PlayerController playerController;

    public void OnLeft()
	{
		playerController.OnLeft();
	}

	public void OnRight()
	{
		playerController.OnRight();
	}

	public void OnJump()
	{
		playerController.OnJump();
		gameManager.OnJump();
	}
}
