using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupCoin : MonoBehaviour
{

    [SerializeField] private AudioClip coinPickupSFX;
    [SerializeField] private int coinValue = 1;
    private bool processingCoinCollision = false;
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!processingCoinCollision)
        {
            processingCoinCollision = true;
            AudioSource.PlayClipAtPoint(coinPickupSFX, Camera.main.transform.position);
            FindObjectOfType<GameSession>().ProcessCoinPickup(coinValue);
            Destroy(gameObject);
        }
    }
}
