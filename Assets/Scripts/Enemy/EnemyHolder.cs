using UnityEngine;
using System.Collections;

public class EnemyHolder : MonoBehaviour {

	int direction = 1;
	float leftEdge,rightEdge;
	float speed = 1f;
	
	[SerializeField]
	private GameObject[] enemySolidiers, enemyDemolitions, enemyMasters;
	
	public static int soldiersCount,demolitionCount,mastersCount;
	
	Vector3[] enemyPos;
	private int currentFilledPosition;
	public static bool isPositionAvailable;
	
	void Awake(){
		enemyPos = new Vector3[5];
		int posCounter=0;
		foreach(Transform child in transform){
			enemyPos[posCounter] = child.transform.position;
			posCounter++;
		}
		currentFilledPosition = 0;
		isPositionAvailable = false;
		soldiersCount = LevelController.GetSoldiersCount();
		demolitionCount = LevelController.GetDemolitionCount();
		mastersCount = LevelController.GetMasterCount();
	}
	
	void Start () {
		int totalEnemies= soldiersCount+demolitionCount+mastersCount;
		for(int i=0;i<totalEnemies;i++){
			//TO MAKE SURE WE AT LEAST HAVE ALL THE PLACES
			// FILLED UP WITH ENEMIES
			LoadAnEnemy();
		}
	}//start
	
	
	void LoadAnEnemy(){
		// SPAWN THE ENEMY
		if(soldiersCount + demolitionCount + mastersCount <= 0){
			//Game Over . Do nothing
			return;
		} else if (currentFilledPosition >=5){
			// NO EMPTY PLACE AVAILABLE
			return;		
		} else {
			int randomize = Random.Range(0,100) % 2;
			if (soldiersCount > 0) {
				if (randomize == 1 && demolitionCount > 0) { LoadADemolition(); /*changing it up*/ } 
				else { LoadASoldier(); }
			} else if (demolitionCount > 0) {
				if(randomize == 1 && mastersCount > 0) { LoadAMaster();  /*changing it up*/  } 
				else { LoadADemolition(); }
			} else if (mastersCount > 0) {
				LoadAMaster();
			}
		} 
	}//LoadEnemeies
	
	private void LoadASoldier(){
		int soldierSelection = Random.Range(0, enemySolidiers.Length );
		GameObject enemy = Instantiate(enemySolidiers[soldierSelection], enemyPos[currentFilledPosition], Quaternion.identity) as GameObject;
		enemy.transform.parent = transform.GetChild(currentFilledPosition).transform;
		currentFilledPosition++;
		soldiersCount--;
	}//LoadASoldier
	
	private void LoadADemolition(){
		int demolitionSelection = Random.Range(0, enemyDemolitions.Length );
		GameObject enemy = Instantiate(enemyDemolitions[demolitionSelection], enemyPos[currentFilledPosition], Quaternion.identity) as GameObject;
		enemy.transform.parent = transform.GetChild(currentFilledPosition).transform;
		currentFilledPosition++;
		demolitionCount--;
	}//LoadADemolition
	
	private void LoadAMaster(){
		int mastersSelection = Random.Range(0, enemyMasters.Length );
		GameObject enemy = Instantiate(enemyMasters[mastersSelection], enemyPos[currentFilledPosition], Quaternion.identity) as GameObject;
		enemy.transform.parent = transform.GetChild(currentFilledPosition).transform;
		currentFilledPosition++;
		mastersCount--;
	}//LoadAMaster
	
	void FixedUpdate(){
		int counter = 0;
		foreach(Transform position in transform){
			if(position.childCount <=0){
				currentFilledPosition = counter;
				LoadAnEnemy();
			}
			counter++;
		}
	}//FixedUpate
	
	void Update () {
		if(direction == 1){
			direction = transform.position.x > 0.5f ? -1 : direction;
		} else {
			direction = transform.position.x < -0.5f ? 1 : direction;
		}
		transform.position += new Vector3(speed*Time.deltaTime*direction, 0 , transform.position.z);
	}//update
}//enemyHolder
