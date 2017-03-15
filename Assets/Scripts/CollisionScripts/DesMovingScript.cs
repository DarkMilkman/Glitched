using UnityEngine;
using System.Collections;

public class DesMovingScript : MonoBehaviour {

	void OnTriggerEnter(Collider collider)
	{
		GameObject obj = collider.gameObject;
		if (obj.tag == "Des")
		{
			obj.transform.parent.parent.GetComponent<DesScript>().isRight = !obj.transform.parent.parent.GetComponent<DesScript>().isRight;
		}
	}
}
