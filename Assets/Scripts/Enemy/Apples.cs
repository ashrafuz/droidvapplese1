using UnityEngine;
using System.Collections;

public class Apples : MonoBehaviour {

	Vector3 startPosition, currentPosition;
	
	ParticleSystem hitParticle;
	
	public static float speedOfApples = 3f;
	
	void OnTriggerEnter2D(Collider2D target){
		if(target.gameObject.tag == "shredder"){
			//print("apples destroyed");
			Player.health -= 25;
			LevelController.ChangeScoreBy(-10);
			Destroy(gameObject);
		} else if (target.gameObject.tag == "player_bullet" || target.gameObject.tag == "player_rocket"){
			Destroy(gameObject);
			HitParticleEffect();
			LevelController.ChangeScoreBy(25);
		} else if (target.gameObject.tag == "Player"){
			HitParticleEffect();
			Destroy(gameObject);
			LevelController.ChangeScoreBy(10);
		}
	}//onTriggerEnter2d
	
	GameObject appleHolder;
	
	void Awake(){
		speedOfApples = Random.Range(1,4);
	}//awake
	
	void Start(){
		appleHolder = GameObject.FindGameObjectWithTag("apple_holder");
		transform.parent = appleHolder.transform;
		
		hitParticle = GameObject.FindGameObjectWithTag("hit_particle_apple").GetComponent<ParticleSystem>();
		startPosition = transform.position;
		currentPosition = startPosition;
	}//start
	
	private void HitParticleEffect(){
		hitParticle.transform.position = transform.position;
		hitParticle.Clear();
		hitParticle.Play();
	}//hitParticleSys
	
	void FixedUpdate(){
		currentPosition.y -= Time.deltaTime* speedOfApples;
		transform.position = currentPosition;
	}//update
}
