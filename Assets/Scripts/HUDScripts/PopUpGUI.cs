using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PopUpGUI : MonoBehaviour {

	public GameObject popUpText;

	public bool showText;
	private float counterTillDeath;
	public float MAX_TIME;
	// Use this for initialization
	void Start () {
		
		popUpText.GetComponent<CanvasGroup>().alpha = 0.0f;
		counterTillDeath = MAX_TIME;
		showText = false;	
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		
		if(showText){
			popUpText.GetComponent<CanvasGroup>().alpha = 1.0f;
		
			counterTillDeath -= Time.deltaTime;
			
			if(counterTillDeath <= 0){
				showText = false;
				popUpText.GetComponent<CanvasGroup>().alpha = 0.0f;
				
				counterTillDeath = MAX_TIME;
				//Destroy(PopUpText);
			}
		}
	}
}
