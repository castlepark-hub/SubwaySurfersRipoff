using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public CharacterController controller;
    public Animator animator;
    public float speedForward = 10f;
    public float speedIncrease = 1;
    public float speedSide = 10;
    public float sideDistance = 2;
    public float jumpSpeed = 10;
    public float gravity = 10;

    int pos = 0; // -1 to 1
    float yVel = -1; // yVelocity
    Vector3 startPos;
    bool jump = false;


    void Start()
    {
        startPos = transform.position;

        GameManager.OnStartGame += ResetPlayer; // subscribing
    }

	private void OnDestroy()
    {
        GameManager.OnStartGame -= ResetPlayer; // UN-subscribing
    }

	void Update()
    {
        speedForward += speedIncrease * Time.deltaTime / 60;


        // side movement
        float targetX = pos * sideDistance;
        // currentX = transform.position.x
        float movementNeeded = targetX - transform.position.x;
        float sideMovement = speedSide * Time.deltaTime;
		// if too little movement, move to the exact target
		if (Mathf.Abs(movementNeeded) < sideMovement)
		{
			sideMovement = movementNeeded;
		}
		// make side movement go to the left
		else if (movementNeeded < 0)
		{
			sideMovement = -sideMovement;
		}


		// jump
		if (controller.isGrounded && jump)
        {
            jump = false;
            //Debug.Log("Jump input received");
            yVel = jumpSpeed;
            animator.SetTrigger("Jump");
        }
        else
		{
            yVel = controller.isGrounded ? -1 : yVel - gravity * Time.deltaTime;
		}


        // apply movement
        // Vector3 velocity = Vector3.forward * speedForward * Time.deltaTime;
        Vector3 velocity = Vector3.forward * speedForward + Vector3.up * yVel;
        velocity *= Time.deltaTime;
        velocity.x = sideMovement;
        controller.Move(velocity);
    }

    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.gameObject.tag == "Wall")
        {
            Debug.Log("player hit a wall", hit.gameObject);
            GameManager.instance.ChangeStateToDeath();
            controller.enabled = false;
        }
    }

	void OnTriggerEnter(Collider other)
	{
		if (other.tag == "Coin")
		{
            Debug.Log("player hit a wall");
            Destroy(other.gameObject);
            GameManager.instance.CollectCoin();
        }
	}


    public void OnLeft()
	{
        if (pos > -1)
        {
            pos--;
        }
    }

    public void OnRight()
	{
        if (pos < 1)
        {
            pos++;
        }
    }

    public void OnJump()
	{
        if (isActiveAndEnabled && controller.isGrounded)
		{
            jump = true;
		}
	}


	void ResetPlayer()
    {
        Debug.Log("ResetPlayer has been called from the OnStartGame Event");
        pos = 0;
        transform.position = startPos;
        controller.enabled = true;
    }
}
