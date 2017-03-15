using UnityEngine;
using System.Collections;

public class KanimiCodeMusic : MonoBehaviour {

	public AudioSource audio;
	
	private GlobalsScript globals;
	
	void Start(){
		audio = GetComponent<AudioSource>();
		globals = GameObject.Find("_Globals").GetComponent<GlobalsScript>();
	}
	
	void Update(){
		if(globals.kanimiCodeActivated){
			playSounds();
		}
	}
	
	void playSounds(){
		globals.kanimiCodeActivated = false;
		
		audio.Play();
	}
}
