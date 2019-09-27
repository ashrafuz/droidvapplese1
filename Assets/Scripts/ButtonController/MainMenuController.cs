using UnityEngine;
using System.Collections;

public class MainMenuController : MonoBehaviour {

	void Awake(){
		Time.timeScale = 1f;
	}//awake
	
	public void ResumeGame(){
		Application.LoadLevel(AllScenes.LEVELS);
	}//resumeGame
	
	public void NewGame(){
		//NEW GAME, SO SET STORY FROM THE FIRST
		GamePreferences.SetSelectedLevel(1);
		GamePreferences.SetHighestUnlockedLevel(1);
		Application.LoadLevel(AllScenes.STORY_1);
	}//newgame
	
	public void Exit(){
		Application.Quit();
	}//exit
	
	public void ShareButton(){
		string message  = "I am playing #DroidVsApplesE1 !! Get It Here: https://play.google.com/store/apps/details?id=com.thebinarywolf.droidvsapplese1 !";
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
	
}//mainmenucontroller
