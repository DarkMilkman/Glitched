using UnityEngine;
using System.Collections;

public class DeleteObjectCollision : MonoBehaviour {

	public GameObject obj;
	
	void OnTriggerEnter(Collider collider)
	{
		GameObject obj = collider.gameObject;
	
		if (obj.tag == "Player")
		{
			Destroy(obj);
		} 
	}
}
