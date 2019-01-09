using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class Player : MonoBehaviour {

	//Config
	[SerializeField] float runSpeed = 5f;
	[SerializeField] float jumpSpeed = 5f;
	[SerializeField] float climbSpeed = 5f;
	[SerializeField] private Vector2 deathKick = new Vector2(10f, 10f);

	//States
	bool isAlive = true;

	//Cached component references
	private Rigidbody2D myRigidbody;
	private Animator myAnimator;
	private CapsuleCollider2D myBodyCollider2D;
	private BoxCollider2D myFeetCollider2D;
	float gravityScaleAtStart;

	// Use this for initialization
	void Start () {
		myRigidbody = GetComponent<Rigidbody2D>();
		myAnimator = GetComponent<Animator>();
		myBodyCollider2D = GetComponent<CapsuleCollider2D>();
		myFeetCollider2D = GetComponent<BoxCollider2D>();
		gravityScaleAtStart = myRigidbody.gravityScale;
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (!isAlive) return;
		Run();
		Jump();
		FlipSprite();
		ClimbLadder();
		Attack();
		Die();
	}

	private void Run() {
		float controlThrow = CrossPlatformInputManager.GetAxis("Horizontal"); //val <-1;1>
		Vector2 playerVelocity = new Vector2(controlThrow*runSpeed, myRigidbody.velocity.y);
		myRigidbody.velocity = playerVelocity;
		//print(playerVelocity);

		bool playerHasHorizontalSpeed = Mathf.Abs(myRigidbody.velocity.x) > Mathf.Epsilon;
		myAnimator.SetBool("Running", playerHasHorizontalSpeed);
	}

	private void ClimbLadder() {
		if(!myFeetCollider2D.IsTouchingLayers(LayerMask.GetMask("Climbing"))) {
			myAnimator.SetBool("Climbing", false);
			myRigidbody.gravityScale = gravityScaleAtStart;
			return;
		}
		float controlThrow = CrossPlatformInputManager.GetAxis("Vertical");
		Vector2 climbVelocity = new Vector2(myRigidbody.velocity.x, controlThrow*climbSpeed);
		myRigidbody.velocity = climbVelocity;
		myRigidbody.gravityScale = 0f;

		bool playerHasHorizontalSpeed = Mathf.Abs(myRigidbody.velocity.y) > Mathf.Epsilon;
		myAnimator.SetBool("Climbing", playerHasHorizontalSpeed);
	}

	private void Jump() {
		if(!myFeetCollider2D.IsTouchingLayers(LayerMask.GetMask("Ground"))) return;

		if(CrossPlatformInputManager.GetButtonDown("Jump")) {
			Vector2 jumpVelocityToAdd = new Vector2(0f, jumpSpeed);
			myRigidbody.velocity = jumpVelocityToAdd;
		}
	}

	private void FlipSprite() {
		bool playerHasHorizontalSpeed = Mathf.Abs(myRigidbody.velocity.x) > Mathf.Epsilon;
		if (playerHasHorizontalSpeed)
			transform.localScale = new Vector2(Mathf.Sign(myRigidbody.velocity.x), 1f);
	}

	void Die()
	{
		if (myBodyCollider2D.IsTouchingLayers(LayerMask.GetMask("Enemy", "Hazards")) || myFeetCollider2D.IsTouchingLayers(LayerMask.GetMask("Enemy", "Hazards")))
		{
			isAlive = false;
			myAnimator.SetTrigger("Dying");
			GetComponent<Rigidbody2D>().velocity = deathKick;
			FindObjectOfType<GameSession>().ProcessPlayerDeath();
		}
	}

	void Attack()
	{
		if (myFeetCollider2D.IsTouchingLayers(LayerMask.GetMask("HitArea")))
			FindObjectOfType<EnemyMovement>().DefeatEnemy();
	}
}
