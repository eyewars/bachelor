using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class playerMovement : MonoBehaviour{
    [Header("Movement")]
    private float moveSpeed;
    public float baseMoveSpeed;

	public float groundDrag;

	public float jumpForce;
	public float jumpCooldown;
	public float airMultiplier;
	bool readyToJump;

	public Transform cameraPos; 

	[Header("Keybinds")]
	public KeyCode jumpKey = KeyCode.Space;

	[Header("Ground Check")]
	public float playerHeight;
	public LayerMask whatIsGround;
	bool grounded;

   	public Transform camera;

   	float horizontalInput;
   	float verticalInput;

   	Vector3 moveDirection;

   	Rigidbody rb;

    void Start(){
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;

        resetSpeed();
		resetJump();
    }

    void FixedUpdate(){
        movePlayer();
    }

    void Update(){
	    // +0.2f er for å ha litt å gå på (tror vi lol), hvis noe fucker seg senere kanskje den burde endres litt på!!
		grounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.2f, whatIsGround);
		myInput();
		speedControl();

		if (grounded){
			rb.drag = groundDrag;
		}
		else {
			rb.drag = 0;
		}
    }

    private void myInput(){
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");
        
        if (Input.GetKeyDown(KeyCode.LeftControl)) {
	        Vector3 tempPos = new Vector3(cameraPos.position.x, cameraPos.position.y, cameraPos.position.z);
	        tempPos.y -= 0.3f;
	        cameraPos.position = tempPos;
        }
        
        if (Input.GetKey(KeyCode.LeftShift)) {
			sprint();
		} else if (Input.GetKey(KeyCode.LeftControl)) {
			crouch();
		} 

        if (Input.GetKeyUp(KeyCode.LeftShift) || Input.GetKeyUp(KeyCode.LeftControl)) {
	        resetSpeed();
        }

        if (Input.GetKeyUp(KeyCode.LeftControl)) {
	        resetCrouchCamera();
        }
        
        if (Input.GetKey(jumpKey) && readyToJump && grounded){
	        jump();
        }
    }

    private void movePlayer(){
        moveDirection = camera.forward * verticalInput + camera.right * horizontalInput;

		if (grounded){
			rb.AddForce(moveDirection.normalized * moveSpeed, ForceMode.Force);
		}
		else{
			rb.AddForce(moveDirection.normalized * moveSpeed * airMultiplier, ForceMode.Force);
		}
    }

	private void speedControl(){
		Vector3 flatVelocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

		if (flatVelocity.magnitude > moveSpeed){
			Vector3 limitedVelocity = flatVelocity.normalized * moveSpeed;
			rb.velocity = new Vector3(limitedVelocity.x, rb.velocity.y, limitedVelocity.z);
		}
	}

	private void jump(){
		readyToJump = false;

		rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

		rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);

		Invoke(nameof(resetJump), jumpCooldown);
	}

	private void resetJump(){
		readyToJump = true;
	}

	private void sprint() {
		moveSpeed = baseMoveSpeed * 1.5f;
	}

	private void crouch() {
		moveSpeed = baseMoveSpeed * 0.3f;
	}

	private void resetSpeed() {
		moveSpeed = baseMoveSpeed;
	}

	private void resetCrouchCamera() {
		Vector3 tempPos = new Vector3(cameraPos.position.x, cameraPos.position.y, cameraPos.position.z);
		tempPos.y += 0.3f;
		cameraPos.position = tempPos;
	}
}