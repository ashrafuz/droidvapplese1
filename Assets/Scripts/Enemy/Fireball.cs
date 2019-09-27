using UnityEngine;
using System.Collections;

public class Fireball : MonoBehaviour {

	public static float speed = 3f;
	private float forceX = 100f;
	
	private Rigidbody2D body;
	
	void OnTriggerEnter2D(Collider2D target){
		if(target.gameObject.tag == "shredder"){
			Destroy(gameObject);
		}
	}//onTriggerEnter2d
	
	private GameObject bulletHolder;
	
	void Awake(){
		body = gameObject.GetComponent<Rigidbody2D>();
	}//awake
	
	void Start(){
		bulletHolder = GameObject.FindGameObjectWithTag("bullet_holder");
		transform.parent = bulletHolder.transform;
		
		int randomzie = Random.Range(0,100) % 2 ;
		forceX = 35f;
		if(randomzie == 0){
			body.AddForce(new Vector2(forceX,50f));
		} else {
			body.AddForce(new Vector2(-forceX,50f));
		}
	}//start
	
	void FixedUpdate(){
		
	}//update
}
