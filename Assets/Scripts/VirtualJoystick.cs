using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;

public class VirtualJoystick : MonoBehaviour, IPointerUpHandler,IPointerDownHandler,IDragHandler {

	Image bgImage, circleImage;
	Vector3 inputPosition;
	float centerX = 5.4f;
	float centerY = -3.0f;
	
	void Start(){
		bgImage = GetComponent<Image>();
		circleImage = transform.GetChild(0).GetComponent<Image>();
	}
	
	public virtual void OnDrag(PointerEventData eData){
		
		Vector3 position;
		
		if(RectTransformUtility.ScreenPointToWorldPointInRectangle( bgImage.rectTransform,
			eData.position, eData.pressEventCamera, out position )){
			
			position.x = position.x - centerX;
			position.y = position.y - centerY;
			inputPosition = new Vector3(position.x,position.y,0);
			inputPosition = (inputPosition.magnitude > 1.0f) ? inputPosition.normalized : inputPosition;
			
			circleImage.rectTransform.anchoredPosition = new Vector3(
				inputPosition.x * (bgImage.rectTransform.sizeDelta.x/3),
				inputPosition.y * (bgImage.rectTransform.sizeDelta.y/3)
			);
			//Debug.Log(inputPosition);
		
		}//if
	}//OnDrag
	
	public virtual void OnPointerUp(PointerEventData eData){
		inputPosition = Vector3.zero;
		circleImage.rectTransform.anchoredPosition = Vector3.zero;
	}//OnPointerUp
	
	public virtual void OnPointerDown(PointerEventData eData){
		OnDrag(eData);
	}//OnPointerDown
	
	public float Horizontal(){
		if(inputPosition.x != 0) { return inputPosition.x;}
		else  {return Input.GetAxis("Horizontal");}
	}//horinzontal
	
	public float Vertical(){
		if(inputPosition.y != 0){ return inputPosition.y;}
		else {return Input.GetAxis("Vertical");}
	}//vertical
}//class
