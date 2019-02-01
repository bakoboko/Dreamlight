using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCharacterScript : MonoBehaviour {

    [SerializeField] private FPSViewScript mouseLook;
    private CharacterController player;
    private Camera fpsCamera;
    private Vector3 move = Vector3.zero;
    private Vector2 moveInput;
    private Vector2 look;
    private Texture2D crosshair;
    private bool jumpTick;

    public float fallSpeedMax;
    public float moveSpeed;
    public float jumpForce;
    public float gravity;
    public float playerHealth; 
    public bool dead;

    public static bool paused;
    public static bool deathMenuActive;
    public static bool freeMouse;
    public static bool freezeMove;

    void Start ()
    {
        player = gameObject.GetComponent<CharacterController>();
        fpsCamera = Camera.main;
        mouseLook.Initialisation(transform, fpsCamera.transform);
	}
	
	
	void Update ()
    {
        Movement();
        Rotate();
	}

    void Movement()
    {

        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");


        if (!freezeMove)
        {
            moveSpeed = 5f;
            moveInput = new Vector2(horizontal, vertical);
        }
        else
        {
            moveSpeed = 0;
        }

       

        if(moveInput.sqrMagnitude > 1)
        {
            moveInput.Normalize();
        }

        Vector3 moveDirection = transform.forward * moveInput.y + transform.right * moveInput.x;

        move.x = moveDirection.x * moveSpeed;
        move.z = moveDirection.z * moveSpeed;

        if (player.isGrounded)
        {
            move.y = 0;
            jumpTick = false;
        }
        else
        {
            move.y -= gravity * Time.deltaTime;
        }

        if (Input.GetButton("Jump") && jumpTick == false)
        {
            move.y = jumpForce;
            jumpTick = true;
        }

        player.Move(move * Time.deltaTime);
        mouseLook.UpdateCursorLock();

      
    }

    void Rotate()
    {
        mouseLook.LookRotation(transform, fpsCamera.transform);
    }

    void OnGUI()
    {
      GUI.Box(new Rect(Screen.width / 2, Screen.height / 2, 5, 5), "");
    }
}
