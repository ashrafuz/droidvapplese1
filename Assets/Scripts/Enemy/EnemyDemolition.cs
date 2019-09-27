using UnityEngine;
using System.Collections;

public class EnemyDemolition : MonoBehaviour {

	private float currentHealth = 500f;
	public float fullHealth = 500f;
	
	private GameObject healthIndicator;
	
	[SerializeField]
	private GameObject EnemyBullets , EnemyRockets;
	
	public CameraShake camShake;
	
	private const int ENEMY_TYPE = 1;
	
	private ParticleSystem hitParticle,explosionParticle;
	
	void Start () {
		InitSoldier();
		StartCoroutine("Shoot");
	}//start
	
	void InitSoldier(){
		healthIndicator = gameObject.transform.GetChild(1).gameObject;
		camShake = GameObject.FindGameObjectWithTag("camera_shake").GetComponent<CameraShake>();
		hitParticle = GameObject.FindGameObjectWithTag("hit_particle_enemy").GetComponent<ParticleSystem>();
		explosionParticle = GameObject.FindGameObjectWithTag("explosion_particle_enemy").GetComponent<ParticleSystem>();
		currentHealth = fullHealth ;
	}//InitSoldier
	
	IEnumerator Shoot(){
		yield return new WaitForSeconds(Random.Range(2,5));
		Vector3 leftGunPos = new Vector3 (transform.position.x - 0.40f,transform.position.y-0.35f,transform.position.z);
		Vector3 rightGunPos = new Vector3 (transform.position.x + 0.40f,transform.position.y-0.35f,transform.position.z);
		Instantiate(EnemyBullets, leftGunPos, Quaternion.identity);
		Instantiate(EnemyBullets, rightGunPos, Quaternion.identity);
		Instantiate(EnemyRockets, transform.position, Quaternion.identity);
		StopCoroutine("Shoot");
		StartCoroutine("Shoot");
	}//shoot
	
	void OnTriggerEnter2D(Collider2D target){
		if(target.gameObject.tag == "player_bullet"){
			currentHealth = currentHealth - 25;
			Destroy(target.gameObject);
			HitParticleEffect();
			LevelController.ChangeScoreBy(1);
		} else if(target.gameObject.tag == "player_rocket"){
			currentHealth = currentHealth - 100;
			Destroy(target.gameObject);
			camShake.Shake(0.05f,0.1f);
			HitParticleEffect();
			LevelController.ChangeScoreBy(5);
		}
	}//OnTriggerEnter2D
	
	
	private void UpdateHealthBar(){
		//0.6 is equvalent to scale 1
		float scale = (currentHealth/fullHealth) * 0.7f;
		healthIndicator.transform.localScale =  new Vector3(scale,0.6f,0f);
	}//updateHealthBar
	
	private void HitParticleEffect(){
		hitParticle = GameObject.FindGameObjectWithTag("hit_particle_enemy").GetComponent<ParticleSystem>();
		hitParticle.transform.position = transform.position;
		hitParticle.Clear();
		hitParticle.Play();
	}//hitParticleSys
	
	private void Explode(){
		explosionParticle = GameObject.FindGameObjectWithTag("explosion_particle_enemy").GetComponent<ParticleSystem>();
		explosionParticle.transform.position = transform.position;
		explosionParticle.Clear();
		explosionParticle.Play();
		Destroy(gameObject);
		LevelController.DestroyAnEnemy(ENEMY_TYPE);
		camShake.Shake(0.08f,0.25f);
		LevelController.ChangeScoreBy(15);
	}//explode
	
	void FixedUpdate () {
		UpdateHealthBar();
		if(currentHealth <=0){
			Explode();
		}
		
	}//update
}
