using UnityEngine;
using System.Collections;

public class Projectiles : MonoBehaviour {

	Vector3 startPosition, currentPosition;
	
	public static float speedOfBullets = 6f;
	public int type;
	
	void OnTriggerEnter2D(Collider2D target){
		if(target.gameObject.tag == "shredder"){
			Destroy(gameObject);
		}
	}//onTriggerEnter2d
	
	GameObject bulletHolder;
	void Start(){
		bulletHolder = GameObject.FindGameObjectWithTag("bullet_holder");
		transform.parent = bulletHolder.transform;
		
		startPosition = transform.position;
		currentPosition = startPosition;
	}//start
	
	void FixedUpdate(){
		if(type == 1){ // self
			currentPosition.y += Time.deltaTime* speedOfBullets;
			transform.position = currentPosition;
		} else {
			currentPosition.y -= Time.deltaTime* speedOfBullets;
			transform.position = currentPosition;
		}
		
	}//update
}//bullets
