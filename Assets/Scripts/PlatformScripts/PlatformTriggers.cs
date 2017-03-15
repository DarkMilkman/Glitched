using UnityEngine;
using System.Collections;

public class PlatformTriggers : MonoBehaviour {

	public GameObject aObj;
	
	void OnTriggerEnter(Collider collider)
	{
		GameObject obj = collider.gameObject;//get the object that entered the trigger from the collider parameter
		//check to see if the object that entered the trigger is taged as Player
		
		if (obj.tag == "Player")
		{
			obj.transform.parent.GetComponent<PlayerPhysics>().ifOnMovingPlatform = true;
			obj.transform.parent.GetComponent<PlayerPhysics>().isPlatformMovingUp = aObj.GetComponent<MovingPlatform>().movingUp;
			obj.transform.parent.GetComponent<PlayerPhysics>().platformDirectionRight = aObj.GetComponent<MovingPlatform>().isRight;
			obj.transform.parent.GetComponent<PlayerPhysics>().platformSpeed = aObj.GetComponent<MovingPlatform>().speed;
		}
	}
	
	void OnTriggerStay(Collider collider)
	{
		GameObject obj = collider.gameObject;//get the object that entered the trigger from the collider parameter
		//check to see if the object that entered the trigger is taged as Player
		
		if (obj.tag == "Player")
		{
			obj.transform.parent.GetComponent<PlayerPhysics>().platformDirectionRight = aObj.GetComponent<MovingPlatform>().isRight;
		}
		
		if(obj.tag == "Collectable")
		{
			obj.transform.GetComponent<MovingCollectable>().isMoving = true;
			obj.transform.GetComponent<MovingCollectable>().isMovingUp = aObj.GetComponent<MovingPlatform>().movingUp;
			obj.transform.GetComponent<MovingCollectable>().isMovingRight = aObj.GetComponent<MovingPlatform>().isRight;
			obj.transform.GetComponent<MovingCollectable>().platformSpeed = aObj.GetComponent<MovingPlatform>().speed;
		}
	}
}
