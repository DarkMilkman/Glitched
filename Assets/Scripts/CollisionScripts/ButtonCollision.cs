using UnityEngine;
using System.Collections;

public class ButtonCollision : MonoBehaviour {

	public GameObject button;
	private GlobalMusicScript music;
	
	void Start ()
	{
		music = GameObject.Find("GlobalSoundEffects").GetComponent<GlobalMusicScript>();
	}
	
	void OnTriggerEnter(Collider collider)
	{
		GameObject obj = collider.gameObject;
		
		if (obj.tag == "Player")
		{
			music.playSwitchSound();
			button.GetComponent<ButtonChangeScript>().change();
		}
	}
}
