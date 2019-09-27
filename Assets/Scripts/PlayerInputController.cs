using UnityEngine;
using System.Collections;

public class PlayerInputController : MonoBehaviour {

	public void StartFire(){
		Player.isFireOn = true;
	}//StartFire
	
	public void EndFire(){
		Player.isFireOn = false;
	}//endFire
	
	public void StartRocket(){
		Player.isRocketOn = true;
	}//StartRocket
	
	public void EndRocket(){
		Player.isRocketOn = false;
	}//EndRocket
}
