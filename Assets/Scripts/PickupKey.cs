using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupKey : MonoBehaviour {

	[SerializeField] private AudioClip keyPickupSFX;

	private void OnTriggerEnter2D(Collider2D collider)
	{
		AudioSource.PlayClipAtPoint(keyPickupSFX, Camera.main.transform.position);
		FindObjectOfType<GameSession>().ProcessKeyPickup();
		Destroy(gameObject);
	}
}
