using UnityEngine;
using System.Collections;

public class MidLevelTransitions : MonoBehaviour {

	public GameObject TransitionScreen;
	private float MAXTIME = 0.75f;
	private float timeCounter;
	private float ALPHACHANGE = 0.1f;
	private float alpha;
	
	public bool transitionStart;
	
	void Start(){
		
		timeCounter =0.0f;
		alpha = 0.0f;
		
		TransitionScreen.GetComponent<CanvasGroup>().alpha = alpha;
	}
	
	void Update(){
		
		if(transitionStart)
		{
			timeCounter += Time.deltaTime;
			
			if(timeCounter <= MAXTIME/2){
				
				alpha += ALPHACHANGE;
				if(alpha >= 1.0f) 
					alpha = 1.0f;
				
				TransitionScreen.GetComponent<CanvasGroup>().alpha = alpha;
			}
			else if(timeCounter < MAXTIME)
			{
				alpha -= ALPHACHANGE;
				if(alpha >= 0.0f) 
					alpha = 0.0f;
				
				TransitionScreen.GetComponent<CanvasGroup>().alpha = alpha;
			} 
			else 
			{
				alpha = 0.0f;
				timeCounter = 0.0f;
				transitionStart = false;
				
				TransitionScreen.GetComponent<CanvasGroup>().alpha = alpha;
			}
		}
	}
}
