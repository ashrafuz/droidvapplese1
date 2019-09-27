using UnityEngine;
using System.Collections;

public class PowerBank : MonoBehaviour {

	//1 for life power
	//0 for rocket
	public int POWER_TYPE = 0 ; 
	
	private int speed = 2;
	
	Vector3 currentPosition;
	
	void Start () {
		currentPosition = transform.position;
		if(gameObject.tag == "power_life"){
			POWER_TYPE = 1;
		} else if (gameObject.tag == "power_rocket"){
			POWER_TYPE = 0;
		}
	}//start
	
	void OnTriggerEnter2D(Collider2D target){
		if(target.gameObject.tag == "Player"){
			Destroy(gameObject);
			if(POWER_TYPE==0){
				Player.rocketCount += 3;
			} else if (POWER_TYPE == 1){
				Player.ChangeHealthBy(50);
				//contain if exceeded
				Player.health = (Player.health > 150) ? 150 : Player.health;
			}
		}
	}//ontriggerEnter2d
	
	void Update(){
		currentPosition.y -= Time.deltaTime * speed;
		transform.position = currentPosition;
	}//update
}
