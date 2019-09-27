using UnityEngine;
using System.Collections;

public class ObjectSpawner : MonoBehaviour {

	[SerializeField]
	private Apples apple;
	
	[SerializeField]
	private PowerBank rocket,life;
	
	Vector3 position;

	void Start () {
		Invoke("SpawnApples",2f);
	}//start
	
	private void SpawnApples(){
		float xPos = Random.Range(-4,4);
		float yPos = transform.position.y;
		int randomize = Random.Range(0,530) % 9;
		if(randomize == 0){
			Instantiate(life,new Vector3(xPos,yPos,0),Quaternion.identity);
		} else if (randomize == 1 || randomize == 2){
			Instantiate(rocket,new Vector3(xPos,yPos,0),Quaternion.identity);
		} else {
			Instantiate(apple,new Vector3(xPos,yPos,0),Quaternion.identity);
		}
		StartCoroutine("SpawnAppleThread");
	}//startSpawning
	
	IEnumerator SpawnAppleThread(){
		yield return new WaitForSeconds(Random.Range(4,10));
		if(Player.isAlive && !Player.isOver){
			SpawnApples();
		}
	}//SpawnApple
}
