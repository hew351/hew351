﻿using UnityEngine;
using System.Collections;

public class FireBallExplo : MonoBehaviour {

	public StatCollectionClass enemyStat;
	
	GameObject player;

	StatCollectionClass playerStat;

	public GameObject explosion;
	
	

	void onExplosion()
	{
		Instantiate (explosion, transform.position,transform.rotation);
	}
	//AudioSource audio;
	
	void Start () 
	{
		player = GameObject.FindWithTag ("Player");
		
		playerStat = player.GetComponent<StatCollectionClass >();
		
		Destroy(gameObject, 2f);


		
	}
	
	
	void OnTriggerEnter2D(Collider2D col)
	{
		
		
		if (col.gameObject.tag == "Enemy") {
			
			enemyStat = col.GetComponent<StatCollectionClass>();
			
			enemyStat.health-=playerStat.EnergyBalldamage;
			
			if(enemyStat.health <= 0)
			{
				Destroy(col.gameObject);
			}

			this.onExplosion();

			Destroy (gameObject);
			
			
			
			
		} 
		
		if (col.gameObject.tag == "wallTop"||col.gameObject.tag == "wallBottom"
		    ||col.gameObject.tag == "wallLeft"|| col.gameObject.tag == "wallRight"
		    ||col.gameObject.tag == "Rock1"||col.gameObject.tag == "Rock2") {

			this.onExplosion();

			Destroy (gameObject);
			
			
		}
		
	}

}
