using UnityEngine;
using System.Collections;

public class GamePreferences : MonoBehaviour {
	
	GamePreferences instance;
	
	//level info
	public static string HIGHEST_UNLOCKED_LEVEL = "highest_unlocked_level";
	public static string SELECTED_LEVEL = "selected_level";
	
	//gameplay elements
	public static string GAME_PLAYED_ATLEAST_ONCE = "game_played_atleast_once";
	
	//GETTERS
	public static int GetHighestUnlockedLevel(){
		return PlayerPrefs.GetInt(GamePreferences.HIGHEST_UNLOCKED_LEVEL);
	}
	
	public static void SetHighestUnlockedLevel(int value){
		PlayerPrefs.SetInt(GamePreferences.HIGHEST_UNLOCKED_LEVEL, value);
	}
	
	public static int GetSelectedLevel(){
		return PlayerPrefs.GetInt(GamePreferences.SELECTED_LEVEL);
	}
	
	public static void SetSelectedLevel(int value){
		PlayerPrefs.SetInt(GamePreferences.SELECTED_LEVEL, value);
	}
		
	void Awake(){
		MakeSingleton();
	}//awake
	
	void MakeSingleton(){
		if(instance != null){
			Destroy(gameObject);
		} else {
			instance = this;
			DontDestroyOnLoad(gameObject);
		}
	}//makesingleton
}
