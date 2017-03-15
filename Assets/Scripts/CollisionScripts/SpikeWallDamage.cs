using UnityEngine;
using System.Collections;

public class SpikeWallDamage : MonoBehaviour {

	public float damageNum = 25.0f;
	// Use this for initialization
	void Start () {
	
	}
	
	void OnTriggerEnter(Collider collider)
	{
		GameObject obj = collider.gameObject;//get the object that entered the trigger from the collider parameter
		//check to see if the object that entered the trigger is taged as Player
		
		if (obj.tag == "Player")
		{
			obj.transform.parent.GetComponent<PlayerHUD>().damagedPlayer(damageNum);
			//obj.transform.parent.GetComponent<PlayerInput>().rumble();
		}
	}
}
