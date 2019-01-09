using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
	[SerializeField] private AudioClip deathScreamSFX;
	[SerializeField] private float moveSpeed = 1f;
	private Rigidbody2D myRigidbody;
//	private bool isFacingRight;
	
	// Use this for initialization
	void Start ()
	{
		myRigidbody = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update ()
	{
		if(IsFacingRight())
			myRigidbody.velocity = new Vector2(moveSpeed, 0f);
		else
			myRigidbody.velocity = new Vector2(-moveSpeed, 0f);
	}

	void OnTriggerExit2D(Collider2D collision)
	{
		transform.localScale = new Vector2(-(Mathf.Sign(myRigidbody.velocity.x)), 1f);
		if(collision.gameObject.name == "HitArea") Destroy(gameObject);
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
