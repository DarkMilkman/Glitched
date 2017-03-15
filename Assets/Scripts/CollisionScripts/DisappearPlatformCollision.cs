using UnityEngine;
using System.Collections;

public class DisappearPlatformCollision : MonoBehaviour {

	public GameObject platform; 
	
	void OnTriggerEnter(Collider collider)
	{
		GameObject obj = collider.gameObject;//get the object that entered the trigger from the collider parameter
		//check to see if the object that entered the trigger is taged as Player
		//print(obj.tag);
		if (obj.tag == "Player")
		{	
			//print("collide");
			platform.GetComponent<DisappearScript>().start = true;
		}
	}
}
