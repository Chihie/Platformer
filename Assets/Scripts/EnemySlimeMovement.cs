using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySlimeMovement : MonoBehaviour {

	[SerializeField] private AudioClip deathScreamSFX;
	[SerializeField] private float moveSpeedNormal = 1f;
	[SerializeField] private float moveSpeedTriggered = 4f;
	[SerializeField] private float moveSpeed = 1f;

	private Animator myAnimator;
	private CircleCollider2D myCircleCollider2D;
	private Rigidbody2D myRigidbody;
//	private bool isFacingRight;
	
	// Use this for initialization
	void Start ()
	{
		myRigidbody = GetComponent<Rigidbody2D>();
		myAnimator = GetComponent<Animator>();
		myCircleCollider2D = GetComponent<CircleCollider2D>();
	}
	
	// Update is called once per frame
	void Update ()
	{
		if(IsFacingRight())
			myRigidbody.velocity = new Vector2(moveSpeed, 0f);
		else
			myRigidbody.velocity = new Vector2(-moveSpeed, 0f);
	}

	void OnTriggerEnter2D(Collider2D collision)
	{
		transform.localScale = new Vector2(-(Mathf.Sign(myRigidbody.velocity.x)), 1f);
		if (collision.gameObject.name == "HitArea") Destroy(gameObject);
		if (collision.gameObject.name == "MobTerritory")
		{
			myAnimator.SetBool("Triggered", true);
			moveSpeed = moveSpeedTriggered;
		}
	}

	void OnTriggerExit2D(Collider2D collision)
	{
		if (collision.gameObject.name == "MobTerritory")
		{
			myAnimator.SetBool("Triggered", false);
			moveSpeed = moveSpeedNormal;
		}
	}

	bool IsFacingRight()
	{
		return transform.localScale.x > 0;
	}

	public void DefeatEnemy() 
	{
		AudioSource.PlayClipAtPoint(deathScreamSFX, Camera.main.transform.position);
		Destroy(gameObject);
	}
}
