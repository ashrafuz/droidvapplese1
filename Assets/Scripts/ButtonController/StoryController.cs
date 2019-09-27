using UnityEngine;
using System.Collections;

public class StoryController : MonoBehaviour {

	public void LoadNextScene(int sceneNum){
		if(sceneNum == 2){
			Application.LoadLevel(AllScenes.STORY_2);
		} else if (sceneNum == 3){
			Application.LoadLevel(AllScenes.STORY_3);
		} else if (sceneNum == 4){
			Application.LoadLevel(AllScenes.STORY_4);
		} else if (sceneNum == 5){
			Application.LoadLevel(AllScenes.LEVELS);
		} 
	}//loadNextScene
}
