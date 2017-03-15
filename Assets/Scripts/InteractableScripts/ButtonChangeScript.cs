using UnityEngine;
using System.Collections;

public class ButtonChangeScript : MonoBehaviour {

	public GameObject option0;
	public GameObject option1;
	
	private bool isOption0;
	
	
	// Use this for initialization
	void Start () {
		isOption0 = true;
		
		option0.active = true;
		option1.active = false;
		
		Color color = GetComponent<Renderer>().material.color;
		color = new Vector4(1.0f, 1.0f, 1.0f, 1.0f);
		GetComponent<Renderer>().material.color = color;
	}
	
	public void change(){
		isOption0 = !isOption0;
		Color color = GetComponent<Renderer>().material.color;
		
		
		if(isOption0){
			option0.active = true;
			option1.active = false;
			
			color = new Vector4(1.0f, 1.0f, 1.0f, 1.0f);
			GetComponent<Renderer>().material.color = color;
		} else {
			option0.active = false;
			option1.active = true;
			
			color = new Vector4(0.0f, 1.0f, 0.0f, 1.0f);
			GetComponent<Renderer>().material.color = color;
		}
	}
}
