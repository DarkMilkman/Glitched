using UnityEngine;
using System.Collections;

public class DesBombCollision : MonoBehaviour {

	private int force = 600;	
	public bool isRight;

	void OnTriggerEnter(Collider collider)
	{
		GameObject obj = collider.gameObject;//get the object that entered the trigger from the collider parameter
		//check to see if the object that entered the trigger is taged as Player
		
		if (obj.tag == "DesBomb")
		{
			GameObject.Find("GlobalSoundEffects").GetComponent<GlobalMusicScript>().playLandSound();
			if(isRight)
			{
				Vector3 dir = new Vector3(-1.0f, 0.35f, 0.0f);
				obj.GetComponent<Rigidbody>().AddForce(dir * force);
			} 
			else
			{
				Vector3 dir = new Vector3(1.0f, 0.35f, 0.0f);
				obj.GetComponent<Rigidbody>().AddForce(dir * force);
			}
			
		}
	}
}
