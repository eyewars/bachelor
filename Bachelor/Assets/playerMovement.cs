using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class playerMovement : MonoBehaviour{
    [Header("Movement")]
    public float moveSpeed;

	public float groundDrag;

	public float jumpForce;
	public float jumpCooldown;
	public float airMultiplier;
	bool readyToJump;

	[Header("Keybinds")]
	public KeyCode jumpKey = KeyCode.Space;

	[Header("Ground Check")]
	public float playerHeight;
	public LayerMask whatIsGround;
	bool grounded;

   	public Transform orientation;

   	float horizontalInput;
   	float verticalInput;

   	Vector3 moveDirection;

   	Rigidbody rb;

    void Start(){
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;

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

		if (Input.GetKey(jumpKey) && readyToJump && grounded){
			jump();
		}
    }

    private void movePlayer(){
        moveDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;

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
}