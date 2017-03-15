using UnityEngine;
using System.Collections;

public class DesDamageCollision : MonoBehaviour {

	public float floatDamageNum = 12.5f;
	public float phaseOneDamageNum = 25.0f;
	public float phaseTwoDamageNum = 20.0f;
	
	private DesScript desScript;
	
	// Use this for initialization
	void Start () {
		
		desScript = GetComponent<DesScript>();
	}
	
	void OnTriggerEnter(Collider collider)
	{
		GameObject obj = collider.gameObject;//get the object that entered the trigger from the collider parameter
		//check to see if the object that entered the trigger is taged as Player
		
		if (obj.tag == "Player")
		{
			if(desScript.normalSize)
				obj.transform.parent.GetComponent<PlayerHUD>().damagedPlayer(floatDamageNum);
			
			else if(desScript.attackPhase == "LightBulb")
			{
				//print(phaseOneDamageNum);
				obj.transform.parent.GetComponent<PlayerHUD>().damagedPlayer(phaseOneDamageNum);
			}	
			else if(desScript.attackPhase == "Die")
			{
				//print(phaseOneDamageNum);
				obj.transform.parent.GetComponent<PlayerHUD>().damagedPlayer(floatDamageNum);
			}	
		}
	}
}
