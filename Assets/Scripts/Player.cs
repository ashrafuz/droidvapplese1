using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Player : MonoBehaviour {

	public float moveSpeed = 0.5f;
	
	[SerializeField]
	private Projectiles bullet,rocket;
	
	[SerializeField]
	private VirtualJoystick joystick;
	
	[SerializeField]
	private Button rocketButton;
	
	private ParticleSystem hitParticle,explosionParticle;
	
	private Rigidbody2D body;
	
	public static bool isAlive, isPaused, isOver;
	public static bool isFireOn,isFireActive;
	public static bool isRocketOn, isRocketActive;
	public static int rocketCount, health = 150;
	
	float bulletInterval,rocketInterval;
	Vector3 leftGunPosition, rightGunPosition,frontGunPosition;
	
	private CameraShake camShake;
	
	void Awake () {
		body = gameObject.GetComponent<Rigidbody2D>();
		isAlive = true;
		isOver = false;
		isFireOn = false;
		isFireActive = true;
		isRocketOn = false;
		isRocketActive = true;
		bulletInterval = 0.2f;
		rocketInterval = 0.5f;
		
		//TODO GET ROCKET COUNT FROM PREFS
		rocketCount = 2;
		health = 150;
		StopAllCoroutines();
	}//awake
	
	void Start(){
		camShake = GameObject.FindGameObjectWithTag("camera_shake").GetComponent<CameraShake>();
		hitParticle = GameObject.FindGameObjectWithTag("hit_particle_player").GetComponent<ParticleSystem>();
		explosionParticle = GameObject.FindGameObjectWithTag("explosion_particle_player").GetComponent<ParticleSystem>();
	}//start
	
	IEnumerator ReloadForFire(){
		isFireActive = false;
		yield return new WaitForSeconds(bulletInterval);
		isFireActive = true;
	}// reloadForFire
	
	IEnumerator ReloadForRocket(){
		isRocketActive = false;
		yield return new WaitForSeconds(rocketInterval);
		isRocketActive = true;
	}//reloadForRocket
	
	
	private void UpdateGunPositions(){
		leftGunPosition = new Vector3(transform.position.x-0.5f,transform.position.y,transform.position.z);
		rightGunPosition = new Vector3(transform.position.x+0.5f,transform.position.y,transform.position.z);
		frontGunPosition = new Vector3(transform.position.x,transform.position.y+0.6f,transform.position.z);
	}//updateGunPositions
	
	
	void LoadFirePower(){
		if(isFireOn && isFireActive){
			Instantiate(bullet, leftGunPosition, Quaternion.identity);
			Instantiate(bullet, rightGunPosition, Quaternion.identity);
			Instantiate(bullet, frontGunPosition, Quaternion.identity);
			StartCoroutine("ReloadForFire");
		}//ifFireOn
	}//fireBullet
	
	void LoadRocketPower(){
		if(rocketCount <= 0) { 
			rocketButton.interactable = false;
			return; 
		}
		rocketButton.interactable = true;
		if(isRocketOn && isRocketActive){
			Instantiate(rocket, transform.position, Quaternion.identity);
			rocketCount--;
			StartCoroutine("ReloadForRocket");
		}//ifFireOn
	}//fireBullet
	
	public static void ChangeHealthBy(int amount){
		if(Player.isAlive && !Player.isOver){
			Player.health += amount;
		}
	}//changeHealthBy
	
	void OnTriggerEnter2D(Collider2D target){
		//Debug.Log("heat taken by " + target.gameObject.name);
		if(target.tag == "enemy_ship"){
			//player collided with the ship itself. so destroy both
			// PLAYER DIED
			Player.health = 0;
			Player.isAlive = false;
			Destroy (gameObject);
			Destroy (target.gameObject);
		} else if (target.tag == "enemy_bullet"){
			Player.ChangeHealthBy(-5);
			Destroy(target.gameObject);
			HitParticleEffect();
			camShake.Shake(0.03f,0.25f);
		} else  if (target.tag == "enemy_rocket"){
			Player.ChangeHealthBy(-20);
			Destroy(target.gameObject);
			HitParticleEffect();
			camShake.Shake(0.06f,0.5f);
		} else if (target.tag == "enemy_fireball"){
			Player.ChangeHealthBy(-40);
			Destroy(target.gameObject);
			HitParticleEffect();
			camShake.Shake(0.08f,0.75f);
		}
	}//ontriggerenter2d
	
	private void HitParticleEffect(){
		if(hitParticle == null){
			hitParticle = GameObject.FindGameObjectWithTag("hit_particle_player").GetComponent<ParticleSystem>();
		}
		hitParticle.transform.position = transform.position;
		hitParticle.Clear();
		hitParticle.Play();
	}//hitParticleSys
	
	void Update () {
		if(Player.health <= 0){
			Player.isAlive = false;
			Explode();
		}
		
		if(Player.isAlive){
			Move(GetInput());
		}
	}//update
	
	void FixedUpdate(){
		if(Player.isAlive){
			LoadFirePower();
			LoadRocketPower();
		}
	}//fixedUpdate
	
	private void Explode(){
		if(explosionParticle == null){
			explosionParticle = GameObject.FindGameObjectWithTag("explosion_particle_player").GetComponent<ParticleSystem>();
		}
		explosionParticle.transform.position = transform.position;
		explosionParticle.Clear();
		explosionParticle.Play();
		camShake.Shake(0.25f,0.75f);
		Destroy(gameObject);
	}//explode
	
	private void Move(Vector3 direction){
		direction.y = 0;
		body.AddForce(direction*moveSpeed);
		UpdateGunPositions();
	}//move
	
	private Vector3 GetInput(){
		Vector3 direction = Vector3.zero;
		direction.x = joystick.Horizontal();
		direction.y = joystick.Vertical();
		return direction;
	}//getInput
	
	
}
