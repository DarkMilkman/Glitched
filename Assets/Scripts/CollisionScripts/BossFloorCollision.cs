using UnityEngine;
using System.Collections;

public class BossFloorCollision : MonoBehaviour {

	void OnTriggerEnter(Collider collider)
	{
		GameObject obj = collider.gameObject;
		if (obj.tag == "Boss")
		{
			obj.GetComponent<BossScript>().gravityOn = false;
		}
	}
	
	/*void OnTriggerStay(Collider collider)
	{
		GameObject obj = collider.gameObject;
		if (obj.tag == "Boss")
		{
			obj.GetComponent<BossScript>().gravityOn = false;
		}
	}*/
	
	/*void OnTriggerExit(Collider collider)
	{
		GameObject obj = collider.gameObject;
		if (obj.tag == "Boss")
		{
			//obj.GetComponent<BossScript>().gravityOn = true;
		}
	}*/
}
