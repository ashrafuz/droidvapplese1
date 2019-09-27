using UnityEngine;
using System.Collections;

public class CameraShake : MonoBehaviour {

	public float shakeAmount;
	public Vector3 defaultCamPosition;
	public Camera mainCam;
	
	void Start(){
		if(mainCam == null){
			mainCam = Camera.main;
		}
	}// awake
	
	public void Shake(float amount,float duration){
		shakeAmount = amount;
		if(mainCam == null){
			mainCam = Camera.main;
		}
		defaultCamPosition = mainCam.transform.position;
		InvokeRepeating("BeginShake",0,0.01f);
		Invoke("StopShake",duration);
	}//shake
	
	void BeginShake(){
		if(shakeAmount > 0){
			Vector3 camPos = mainCam.transform.position;
			float offsetX = Random.value * shakeAmount * 2 - shakeAmount;
			float offsetY = Random.value * shakeAmount * 2 - shakeAmount;
			camPos.x += offsetX;
			camPos.y += offsetY;
			
			mainCam.transform.position = camPos;
		}
	}//beginShake
	
	void StopShake(){
		CancelInvoke("BeginShake");
		mainCam.transform.position = defaultCamPosition;
	}//stopShake
	
}
