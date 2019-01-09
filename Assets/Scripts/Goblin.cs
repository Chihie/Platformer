using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goblin : MonoBehaviour
{

	[SerializeField] private AudioClip voiceSFX;
	[SerializeField] private GameObject ConversationBubble;
	[SerializeField] private Sprite BubbleDistance;
	[SerializeField] private Sprite BubbleClose;
	[SerializeField] private Sprite BubbleBought;
	[SerializeField] private Sprite BubbleFailCoins;
	[SerializeField] private Sprite BubbleFailHearts;

	private SpriteRenderer spriteRenderer;
	private CapsuleCollider2D goblinBodyCollider2D;
	private bool collidedClose = false;
	
	// Use this for initialization
	void Start ()
	{
//		ConversationBubble.Sprite
		spriteRenderer = ConversationBubble.GetComponent<SpriteRenderer>();
		spriteRenderer.sprite = BubbleDistance;
		goblinBodyCollider2D = GetComponent<CapsuleCollider2D>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision is CircleCollider2D && !collidedClose)
			spriteRenderer.sprite = BubbleClose;
		else if (collision is BoxCollider2D && !collidedClose)
			spriteRenderer.sprite = BubbleClose;
		else if ((collision is CapsuleCollider2D) && (goblinBodyCollider2D.IsTouchingLayers(LayerMask.GetMask("Player"))))
		{
			collidedClose = true;
			string tradeResult = FindObjectOfType<GameSession>().ProcessTrade(3, 1);
			if(tradeResult == "success")
				spriteRenderer.sprite = BubbleBought;
			else if (tradeResult == "fail_hearts")
				spriteRenderer.sprite = BubbleFailHearts;
			else spriteRenderer.sprite = BubbleFailCoins;
		}
		AudioSource.PlayClipAtPoint(voiceSFX, Camera.main.transform.position);
	}
}
