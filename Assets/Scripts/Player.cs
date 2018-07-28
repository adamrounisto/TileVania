using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class Player : MonoBehaviour {
    //config

    [SerializeField] float runSpeed = 5f;
    [SerializeField] float jumpSpeed = 5f;
    [SerializeField] float climbSpeed = 5f;
    [SerializeField] Vector2 deathKick = new Vector2(25f, 25f);

    //states

    bool isAlive = true;
   

    //cahed component refs

    Rigidbody2D myRigidBody;
    Animator myanimator;
    CapsuleCollider2D myBodyCollider2D;
    BoxCollider2D myFeet;

    LayerMask myLayermask;
    float gravityScaleAtStart;


	// Messages then methods
	void Start () {
        myRigidBody = GetComponent<Rigidbody2D>();
        myanimator = GetComponent<Animator>();
        myBodyCollider2D = GetComponent<CapsuleCollider2D>();
        gravityScaleAtStart = myRigidBody.gravityScale;
        myFeet = GetComponent<BoxCollider2D>();
                       
	}
	
	// Update is called once per frame
	void Update () {

        if (!isAlive) { return;}
        

            Run();
            FlipSprite();
            Jump();
            ClimbLadder();
            Die();
        
        
    }

    private void Run()
    {
        float controlThrow = CrossPlatformInputManager.GetAxis("Horizontal"); // -1 to +1
        Vector2 playerVelocity = new Vector2(controlThrow* runSpeed, myRigidBody.velocity.y);
        myRigidBody.velocity = playerVelocity;
        bool playerHorizonatalSpeed = Mathf.Abs(myRigidBody.velocity.x) > Mathf.Epsilon;
        myanimator.SetBool("isRunning", playerHorizonatalSpeed);
        
    }

    private void ClimbLadder()
    {
        if (!myFeet.IsTouchingLayers(LayerMask.GetMask("Climbing")))

        {
            myanimator.SetBool("isClimbing", false);
            myRigidBody.gravityScale = gravityScaleAtStart;
            return;
        }

         
        float controlThrow = CrossPlatformInputManager.GetAxis("Vertical");

        Vector2 climbVelocity = new Vector2(myRigidBody.velocity.x, controlThrow * climbSpeed);
        myRigidBody.velocity = climbVelocity;
        myRigidBody.gravityScale = 0f;

        bool playerHasVerticalSpeed = Mathf.Abs(myRigidBody.velocity.y) > Mathf.Epsilon;
        myanimator.SetBool("isClimbing",playerHasVerticalSpeed);
    }
    

    private void Jump()
    {
        if (!myFeet.IsTouchingLayers(LayerMask.GetMask("Ground"))) { return; }

        if (CrossPlatformInputManager.GetButtonDown("Jump"))
            {
                Vector2 jumpVelocityToAdd = new Vector2(0f, jumpSpeed);
                myRigidBody.velocity += jumpVelocityToAdd;
            }
        
    }

    private void FlipSprite()
    {
        bool playerHorizonatalSpeed = Mathf.Abs(myRigidBody.velocity.x) > Mathf.Epsilon;
        if (playerHorizonatalSpeed)
        {
            transform.localScale = new Vector2(Mathf.Sign(myRigidBody.velocity.x), 1f);
        }
    }

    public void Die()
    {
        if (myBodyCollider2D.IsTouchingLayers(LayerMask.GetMask("enemy")))
        {
            myanimator.SetTrigger("Dying");
            isAlive = false;
            GetComponent<Rigidbody2D>().velocity = deathKick;
        }
    }



   
}
