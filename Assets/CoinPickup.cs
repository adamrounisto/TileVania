using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinPickup : MonoBehaviour {
    [SerializeField] AudioClip coinGrab;
    [SerializeField] int pointForCoinPickup = 100;
    
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        FindObjectOfType<GameSession>().AddToScore(pointForCoinPickup);
        AudioSource.PlayClipAtPoint(coinGrab, Camera.main.transform.position);
        Destroy(gameObject);
    }
}
