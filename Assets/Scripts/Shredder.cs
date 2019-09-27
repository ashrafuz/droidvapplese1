using UnityEngine;
using System.Collections;

public class Shredder : MonoBehaviour {

	void OnTriggerEnter2D(Collider2D target){
		//print("Shredder Destroyed " + target.gameObject.name);
		if (target.gameObject.tag == "player_rocket"){
			LevelController.ChangeScoreBy(-5);
		}
		//APPLE SHRED IS HANDLED IN APPLE SCRIPT;
		Destroy(target.gameObject);
	}//onTriggerEnter2d
}
