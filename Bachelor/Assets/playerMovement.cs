using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class playerMovement : MonoBehaviour{
    [Header("Movement")] private float moveSpeed;
    public float baseMoveSpeed;

    public float groundDrag;

    public float jumpForce;
    public float jumpCooldown;
    public float airMultiplier;
    bool readyToJump;

    public int movementType = 1;

    [Range(0, 0.02f)] public float amp;
    [Range(0, 20)] public float freq;

    [Header("Keybinds")] public KeyCode jumpKey = KeyCode.Space;

    [Header("Ground Check")] public float playerHeight;
    public LayerMask whatIsGround;
    bool grounded;

    public Transform head;
    public Transform camera;
    public Animator cameraBob;
    private Vector3 startPos;

    public float bobSpeedCrouch;
    public float bobSpeedWalk;
    public float bobSpeedRun;

    float horizontalInput;
    float verticalInput;

    Vector3 moveDirection;

    Rigidbody rb;

    public AudioSource source;

    public AudioClip[] crouchSounds;
    public AudioClip[] walkSounds;
    public AudioClip[] runSounds;

    void Start() {
        startPos = Vector3.zero;

        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;

        resetSpeed();
        resetJump();
    }

    void FixedUpdate() {
        if (!playerManager.instance.hasLost) {
            movePlayer();
        }
    }

    void Update() {
        if (!playerManager.instance.hasLost) {
           // +0.2f er for å ha litt å gå på (tror vi lol), hvis noe fucker seg senere kanskje den burde endres litt på!!
            grounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.2f, whatIsGround);
            myInput();
            speedControl();
           
            if (grounded) {
                rb.drag = groundDrag;
            } else {
                rb.drag = 0;
            }
           
            //movePlayer(); 
        }
    }

    private void myInput() {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

        if ((horizontalInput != 0) || (verticalInput != 0)) {
            cameraBob.Play("Bobbing");
        } else {
            resetCameraPos();
            cameraBob.Play("None");
        }

        if (Input.GetKeyDown(KeyCode.LeftControl)) {
            Vector3 tempPos = new Vector3(head.localPosition.x, head.localPosition.y, head.localPosition.z);
            tempPos.y -= 0.3f;
            //head.localPosition = Vector3.Lerp(head.localPosition, tempPos, 5 * Time.deltaTime);
            head.localPosition = tempPos;
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
            resetHeadPos();
        }

        if (Input.GetKey(jumpKey) && readyToJump && grounded) {
            jump();
        }
    }

    private void movePlayer() {

            moveDirection = camera.forward * verticalInput + camera.right * horizontalInput;
            moveDirection = new Vector3(moveDirection.x, 0f, moveDirection.z);
            // HUSK Time.deltaTime hvis du bruker update()!!!!!!!
            if (grounded) {
                rb.AddForce(moveDirection.normalized * moveSpeed, ForceMode.Force);
            } else {
                rb.AddForce(moveDirection.normalized * moveSpeed * airMultiplier, ForceMode.Force);
            }

            if (rb.velocity.magnitude < 0.01) {
                movementType = 3;
            } else if (moveSpeed == baseMoveSpeed) {
                movementType = 1;
            }
       
            if (movementType == 0){
                cameraBob.SetFloat("bobSpeed", bobSpeedCrouch);
                if (!source.isPlaying){
                    int randomNum = (int)Random.Range(0, crouchSounds.Length - 1);
                    source.clip = crouchSounds[randomNum];

                    source.Play();
                }
            } else if (movementType == 1){
                cameraBob.SetFloat("bobSpeed", bobSpeedWalk);
                if (!source.isPlaying){
                    int randomNum = (int)Random.Range(0, walkSounds.Length - 1);
                    source.clip = walkSounds[randomNum];

                    source.Play();
                }
            } else if (movementType == 2){
                cameraBob.SetFloat("bobSpeed", bobSpeedRun);
                if (!source.isPlaying){
                    int randomNum = (int)Random.Range(0, runSounds.Length - 1);
                    source.clip = runSounds[randomNum];

                    source.Play();
                }
            }
    }

    private void speedControl() {
        Vector3 flatVelocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        if (flatVelocity.magnitude > moveSpeed) {
            Vector3 limitedVelocity = flatVelocity.normalized * moveSpeed;
            rb.velocity = new Vector3(limitedVelocity.x, rb.velocity.y, limitedVelocity.z);
        }
    }

    private void jump() {
        readyToJump = false;

        rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);

        Invoke(nameof(resetJump), jumpCooldown);
    }

    private void resetJump() {
        readyToJump = true;
    }

    private void sprint() {
        moveSpeed = baseMoveSpeed * 1.3f;
        if (rb.velocity.magnitude > 0.01) {
            movementType = 2;
        }
    }

    private void crouch() {
        moveSpeed = baseMoveSpeed * 0.5f;
        if (rb.velocity.magnitude > 0.01) {
            movementType = 0;
        }
    }

    private void resetSpeed() {
        moveSpeed = baseMoveSpeed;
        if (rb.velocity.magnitude > 0.01) {
            movementType = 1;
        }
    }

    private void resetCameraPos() {
        camera.localPosition = Vector3.Lerp(camera.localPosition, startPos, 5 * Time.deltaTime);
        //camera.localPosition = startPos;
    }

    private void resetHeadPos() {
        head.localPosition = startPos;
    }
}