using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerController : MonoBehaviour
{
    public float playerSpeed = 5f;
    public float waterSpeed = 1f;
    public bool inWater = false;
    public Rigidbody2D rb;
    public BoxCollider2D coll;
    public Animator anim;
    private float moveX = 0f;
    private float moveY = 0f;
    private bool isStrafing = false;
    public bool isSprinting = false;
    public static PlayerController instance;
    [SerializeField] CombatMenu combatMenu;

    private void Awake() //Keeps player instantiated across scenes
    {
        /*if (instance != null)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);*/
    }
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        coll = GetComponent<BoxCollider2D>();
        anim = GetComponent<Animator>();
        rb.bodyType = RigidbodyType2D.Dynamic;
    }
    void Update()
    {
        moveX = Input.GetAxisRaw("Horizontal"); //Horizontal movement
        moveY = Input.GetAxisRaw("Vertical"); //Vertical movement
        if (combatMenu.InCombat() == false)
        {
            rb.velocity = new Vector2(moveX * playerSpeed * waterSpeed, moveY * playerSpeed * waterSpeed); //total movement
        }

        if ((Input.GetKey(KeyCode.S) == true || Input.GetKey(KeyCode.W) == true) && (Input.GetKey(KeyCode.A) == true || Input.GetKey(KeyCode.D))) //Keeps player speed equal when strafing
        {
            isStrafing = true;
            playerSpeed = 4f;
        }
        else
        {
            playerSpeed = 5f;
            isStrafing = false;
        }

        if (Input.GetKey(KeyCode.LeftShift) && isStrafing == false) //Sprinting speed
        {
            playerSpeed = 10f;
            isSprinting = true;
        }
        else if (Input.GetKey(KeyCode.LeftShift) && isStrafing == true) //Strafe sprint speed
        {
            playerSpeed = 7f;
            isSprinting = true;
        }
        else 
        { 
            isSprinting = false;
        }

        if (inWater)
        {
            waterSpeed = 0.5f;
        }
        else
        {
            waterSpeed = 1f;
        }

        if (!PauseMenu.gameIsPaused) //Pauses animations on pause menu
        {
            UpdateAnimationState();
        }
    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "water")
        {
            inWater = true;
        }
    }
    public void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "water")
        {
            inWater = false;
        }
    }
    public void UpdateAnimationState() //Changes animation movement and idle bools for state machine
    {
        if (moveX > 0f) //Left - Right animations
        {
            anim.SetBool("walkingRight", true);
        }
        else if (moveX < 0f)
        {
            anim.SetBool("walkingLeft", true);
        }
        else
        {
            anim.SetBool("walkingLeft", false);
            anim.SetBool("walkingRight", false);
        }

        if (moveY < 0f) //Up - Down animations
        {
            anim.SetBool("walkingDown", true);
        }
        else if (moveY > 0f)
        {
           anim.SetBool("walkingUp", true);
        }
        else
        {
           anim.SetBool("walkingUp", false);
           anim.SetBool("walkingDown", false);
        }

        if (moveX == 0f && moveY == 0f) //idle animation
        {
            anim.SetBool("idle", true);
        }
        else
        {
            anim.SetBool("idle", false);
        }

        if (isSprinting) //Increases animation speed if sprinting
        {
            anim.SetFloat("sprintMultiplier", 2f);
        }
        else
        {
            anim.SetFloat("sprintMultiplier", 1f);
        }
    }
}