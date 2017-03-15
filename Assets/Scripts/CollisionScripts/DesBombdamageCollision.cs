using UnityEngine;
using System.Collections;

public class DesBombdamageCollision : MonoBehaviour {

	public GameObject player;
	public float bombDamage = 20.0f;
	public float explosionDamage = 25.0f;
	
	void OnTriggerEnter(Collider collider)
	{
		GameObject obj = collider.gameObject;//get the object that entered the trigger from the collider parameter
		//check to see if the object that entered the trigger is taged as Player
		
		if (obj.tag == "DesBomb")
		{
			player.GetComponent<PlayerHUD>().damagedPlayer(bombDamage);
		} 
		else if(obj.tag == "DesBombExplosion")
		{
			player.GetComponent<PlayerHUD>().damagedPlayer(explosionDamage);
		}
	}
}
