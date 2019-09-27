using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class FadeIn : MonoBehaviour {

	[SerializeField]
	Image fadeInPanel;
	
	[SerializeField]
	Button nextButton;
	
	[SerializeField]
	Text nextButtonText;
	
	public int fadeInTime;
	
	private Color fadeColor = Color.black;
	
	void Awake(){
		nextButton.interactable = false;
		Invoke("ActivateNextButton",3f);
		nextButtonText.text = ". . .";
	}//awake
	
	private void ActivateNextButton(){
		nextButton.interactable = true;
		nextButtonText.text = "NEXT";
	}//activateNextButton
	
	// Update is called once per frame
	void Update () {
		if(Time.timeSinceLevelLoad < fadeInTime){
			float alphaChange = Time.deltaTime / fadeInTime;
			fadeColor.a -= alphaChange;
			fadeInPanel.color = fadeColor;
		} else {
			gameObject.SetActive(false);
		}
	}
}
