using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

    public float moveSpeed;
    private float moveSpeedStore;
    public float speedMultiplier;
    public float speedMilestone;
    private float speedMilestoneCount;
    private float speedMilestoneCountStore;
    private float speedMilestoneStore;
    public float jumpForce;
    public float jumpTime;
    private float jumpTimeCounter;
    public bool grounded;
    public LayerMask whatIsGround;
    public Transform groundCheck;
    public float groundCheckRadius;
    //private Collider2D myCollider;
    private Animator myAnim;

    public GameManager theGamemanager;

    public AudioSource jumpSound;

    private Rigidbody2D myRigidBody;

	// Use this for initialization
	void Start () {
        myRigidBody = GetComponent<Rigidbody2D>();
       // myCollider = GetComponent<Collider2D>();
        myAnim = GetComponent<Animator>();
        jumpTimeCounter = jumpTime;
        speedMilestoneCount = speedMilestone;
        moveSpeedStore = moveSpeed;
        speedMilestoneCountStore = speedMilestoneCount;
        speedMilestoneStore = speedMilestone;
	
	}
	
	// Update is called once per frame
	void Update () {

        //  grounded = Physics2D.IsTouchingLayers(myCollider, whatIsGround);

        grounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, whatIsGround);

        if (transform.position.x > speedMilestoneCount)
        {
            speedMilestoneCount += speedMilestone ;
            speedMilestone = speedMilestone * speedMultiplier;
            moveSpeed = moveSpeed * speedMultiplier; 
        }

        myRigidBody.velocity = new Vector2(moveSpeed, myRigidBody.velocity.y);

        if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0)) 
        {
            if (grounded)
            {
                myRigidBody.velocity = new Vector2(myRigidBody.velocity.x, jumpForce);
                jumpSound.Play();
            }
        }

        if (Input.GetKey(KeyCode.Space) || Input.GetMouseButton(0))
        {
            if(jumpTimeCounter > 0)
            {
                myRigidBody.velocity = new Vector2(myRigidBody.velocity.x, jumpForce);
                jumpTimeCounter -= Time.deltaTime;
            }
        }

        if(Input.GetKeyUp(KeyCode.Space)|| Input.GetMouseButtonUp(0))
        {
            jumpTimeCounter = 0;
        }

        if (grounded)
        {
            jumpTimeCounter = jumpTime;
        }


        myAnim.SetFloat("Speed", myRigidBody.velocity.x);
        myAnim.SetBool("Grounded", grounded);
	}
    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "killbox")
        {
            theGamemanager.RestartGame();
            moveSpeed = moveSpeedStore;
            speedMilestoneCount = speedMilestoneCountStore;
            speedMilestone = speedMilestoneStore;
        }
    }
}
