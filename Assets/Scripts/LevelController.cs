using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class LevelController : MonoBehaviour {

	public static int LEVEL_NUM;
	public static int SCORE = 0;
	
	[SerializeField]
	private Text scoreText, soldierRemainText,demolitionRemainText,masterRemainText,
		rocketRemainText, gameOverBodyText,levelCompleteText;
	
	[SerializeField]
	private Image healthMeter;
	
	public static int soldierRemain;
	public static int demolitionRemain;
	public static int masterRemain;
	
	[SerializeField]
	GameObject gameOverPanel,enemyHolder,levelCompletePanel,pausePanel;
	
	void Awake () {
		LEVEL_NUM = GamePreferences.GetSelectedLevel();
		SCORE = 0;
		soldierRemain = GetSoldiersCount();
		demolitionRemain = GetDemolitionCount();
		masterRemain = GetMasterCount();
		
		enemyHolder.SetActive(true);
		gameOverPanel.SetActive(false);
		levelCompletePanel.SetActive(false);
		pausePanel.SetActive(false);
	}//awake
	
	void Start(){
		// health is the meter on which the damage scale is controlled
		// by default its 150
		healthMeter.rectTransform.sizeDelta = new Vector2(Player.health,30);
	}//start
	
	private void UpdateUI(){
		scoreText.text = "SCORE : " + SCORE;
		soldierRemainText.text =  "x" + soldierRemain;
		demolitionRemainText.text =  "x" + demolitionRemain;
		masterRemainText.text =  "x" + masterRemain;
		rocketRemainText.text = "x" + Player.rocketCount;
		
		healthMeter.rectTransform.sizeDelta = new Vector2(Player.health,30);
		
		if(soldierRemain+demolitionRemain+masterRemain <= 0){
			EndLevelSuccess();
		}
		
		if(!Player.isAlive){
			enemyHolder.SetActive(false);
			gameOverPanel.SetActive(true);
			gameOverBodyText.text= "you had " + (soldierRemain + demolitionRemain + masterRemain) + " enemies left to kill.";
		}
		
	}//updateUI
	
	IEnumerator WaitForFinish(float time){
		yield return new WaitForSeconds(time);
		levelCompletePanel.SetActive(true);
		levelCompleteText.text = "Level " + LEVEL_NUM + "  Complete! ";
		levelCompleteText.text += "\nScore : " + LevelController.SCORE;
		GamePreferences.SetHighestUnlockedLevel(LEVEL_NUM + 1);
	}//waitFor
	
	private void EndLevelSuccess(){
		Player.isOver = true;
		StartCoroutine("WaitForFinish",1f);
	}//End Game Success
	
	public static void DestroyAnEnemy(int type){
		if (type == 0) soldierRemain--;
		else if (type == 1) demolitionRemain--;
		else if (type == 2) masterRemain--;
	}//DestroyAnEnemy
	
	void FixedUpdate () {
		UpdateUI();
	}//update
	
	public void PauseGame(){
		pausePanel.SetActive(true);
		Time.timeScale = 0.0f;
	}//pauseGame
	
	public void ResumeGame(){
		pausePanel.SetActive(false);
		Time.timeScale = 1.0f;
	}//ResumeGame
	
	public static void ChangeScoreBy(int point){
		if(Player.isAlive && !Player.isOver){
			SCORE += point;
		}
	}//increaseScore
	
	/* BUTTON CONTROLLERS */
	
	public void PlayAgain(){
		Application.LoadLevel(Application.loadedLevel);
	}//playAgain
	
	public void GoBackToMainMenu(){
		Application.LoadLevel(AllScenes.MAIN_MENU);
	}//playAgain
	
	public void Leaderboard(){
		print("should show leader board now");
	}//leaderboard
	
	public void NextLevel(){
		Application.LoadLevel(AllScenes.LEVELS);
	}//leaderboard
	
	
	public void ShareButton(){
		string message  = "I Scored "+LevelController.SCORE+" on #DroidVsApplesE1 !! Get It Here: https://play.google.com/store/apps/details?id=com.thebinarywolf.droidvsapplese1 !";
		string subject  = "Droid Vs Apples";
		if(!Application.isEditor){
			AndroidJavaClass intentClass = new AndroidJavaClass("android.content.Intent");
			AndroidJavaObject intentObject = new AndroidJavaObject("android.content.Intent");
			intentObject.Call<AndroidJavaObject>("setAction", intentClass.GetStatic<string>("ACTION_SEND"));
			AndroidJavaClass uriClass = new AndroidJavaClass("android.net.Uri");
			
			intentObject.Call<AndroidJavaObject> ("setType", "text/plain");
			intentObject.Call<AndroidJavaObject>("putExtra", intentClass.GetStatic<string>("EXTRA_TEXT"), ""+ message);
			intentObject.Call<AndroidJavaObject>("putExtra", intentClass.GetStatic<string>("EXTRA_SUBJECT"), subject);
			
			AndroidJavaClass unity = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
			AndroidJavaObject currentActivity = unity.GetStatic<AndroidJavaObject>("currentActivity");
			
			currentActivity.Call("startActivity", intentObject);
		}//editor
	}//share
	
	
	public static int GetSoldiersCount(){
		switch(LEVEL_NUM){
			case 1: return 2; break;
			case 2: return 4; break;
			case 3: return 6; break; 
			case 4: return 7; break;
			case 5: return 10; break; 
			case 6: return 12; break; 
			case 7: return 15; break; 
			case 8: return 17; break;
			case 9: return 18; break;
			case 10: return 20; break;
			case 11: return 22; break;
			case 12: return 24; break;
			case 13: return 26; break;
			case 14: return 28; break;
			case 15: return 30; break;
			default: return 2; break;
		}
	}//GetSoldiersCOunt
	
	public static int GetDemolitionCount(){
		switch(LEVEL_NUM){
			case 1: return 0; break;
			case 2: return 1; break;
			case 3: return 2; break;
			case 4: return 3; break;
			case 5: return 4; break;
			case 6: return 5; break;
			case 7: return 6; break;
			case 8: return 7; break;
			case 9: return 8; break;
			case 10: return 9; break;
			case 11: return 10; break;
			case 12: return 11; break;
			case 13: return 12; break;
			case 14: return 13; break;
			case 15: return 15; break;
			default: return 0; break;
		}
	}//GetDemolitionCOunt
	
	public static int GetMasterCount(){
		switch(LEVEL_NUM){
			case 1: return 0; break;
			case 2: return 0; break;
			case 3: return 0; break;
			case 4: return 0; break;
			case 5: return 1; break;
			case 6: return 2; break;
			case 7: return 3; break;
			case 8: return 4; break;
			case 9: return 5; break;
			case 10: return 6; break;
			case 11: return 7; break;
			case 12: return 8; break;
			case 13: return 9; break;
			case 14: return 10; break;
			case 15: return 11; break;
			default: return 0; break;
		}
	}//GetMasterCOunt
	
	
}
