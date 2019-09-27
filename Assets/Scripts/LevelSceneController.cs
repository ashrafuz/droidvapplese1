using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class LevelSceneController : MonoBehaviour {

	// Use this for initialization
	void Start () {
		/* DEBUG */
		//PlayerPrefs.DeleteAll();
		/* DEBUG */
		
		Time.timeScale = 1.0f;
		
		if(!PlayerPrefs.HasKey(GamePreferences.GAME_PLAYED_ATLEAST_ONCE)){
			// played for the first time
			GamePreferences.SetSelectedLevel(1);
			GamePreferences.SetHighestUnlockedLevel(1);
			PlayerPrefs.SetInt(GamePreferences.GAME_PLAYED_ATLEAST_ONCE, 1);
		}//if
		DecorateButtons();
	}//start
	
	public void LoadLevel(int level){
		GamePreferences.SetSelectedLevel(level);
		Application.LoadLevel(AllScenes.GAME);
	}//loadLevel
	
	public void GoBack(){
		Application.LoadLevel(AllScenes.MAIN_MENU);
	}//goback
	
	private void DecorateButtons(){
		for(int i=1;i<=15;i++){
			string name = "Level" + i;
			Button btn = GameObject.Find(name).GetComponent<Button>();
			if(btn !=null){
				btn.interactable = false;
			}
		}//for
		
		for(int i=1;i<=GamePreferences.GetHighestUnlockedLevel();i++){
			string name = "Level" + i;
			Button btn = GameObject.Find(name).GetComponent<Button>();
			if(btn !=null){
				btn.interactable = true;
			}
		}//for
	}//DecorateButtons
}
