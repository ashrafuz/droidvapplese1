using UnityEngine;
using System.Collections;

public class CameraProp : MonoBehaviour {
	
	public static float GetCameraHeight(Camera cam){
		float camHeight = 2f * Camera.main.orthographicSize + 0.4f; 
		return camHeight;
	}
	
	public static float GetCameraWidth(Camera cam){
		float camHeight = 2f * cam.orthographicSize + 0.4f; 
		float camWidth = camHeight * cam.aspect + 0.4f;
		return camWidth;
	}
}

