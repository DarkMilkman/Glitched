using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TransitionsUpdate : MonoBehaviour {
	
	public GameObject TransitionScreen;
	public float MAXTIME;
	private float timeCounter;
	public float ALPHACHANGE;
	private float alpha;
	
	void Start(){
		
		timeCounter = MAXTIME;
		alpha = 1.0f;
		
		TransitionScreen.GetComponent<CanvasGroup>().alpha = alpha;
	}
	
	void Update(){
		
		timeCounter -= Time.deltaTime;
		
		if(timeCounter <= 0){
			
			timeCounter = MAXTIME;
			alpha -= ALPHACHANGE;
			TransitionScreen.GetComponent<CanvasGroup>().alpha = alpha;
			
			if(alpha <= 0){
				Destroy(TransitionScreen);
			}
		}
	}
	
}