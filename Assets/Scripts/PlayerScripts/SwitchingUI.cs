using UnityEngine;
using System.Collections;

public class SwitchingUI : MonoBehaviour {

	public GameObject prePowersUI;
	public GameObject postPowersUI;
	
	public GameObject powerSlider;
	
	// Use this for initialization
	void Start () {
		prePowersUI.GetComponent<CanvasGroup>().alpha = 1.0f;
		postPowersUI.GetComponent<CanvasGroup>().alpha = 0.0f;
		powerSlider.GetComponent<CanvasGroup>().alpha = 0.0f;
	}
	
	public void switchUI(){
		prePowersUI.GetComponent<CanvasGroup>().alpha = 0.0f;
		postPowersUI.GetComponent<CanvasGroup>().alpha = 1.0f;
		powerSlider.GetComponent<CanvasGroup>().alpha = 1.0f;
	}
}
