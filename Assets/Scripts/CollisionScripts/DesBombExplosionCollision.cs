using UnityEngine;
using System.Collections;

public class DesBombExplosionCollision : MonoBehaviour {

	private float counter = 0.0f;
	public GameObject des;
	
	void OnTriggerEnter(Collider collider)
	{
		GameObject obj = collider.gameObject;//get the object that entered the trigger from the collider parameter
		//check to see if the object that entered the trigger is taged as Player
		
		if (obj.tag == "DesBomb")
		{
			GameObject.Find("GlobalSoundEffects").GetComponent<GlobalMusicScript>().playBombExplodeSound();
			des.GetComponent<DesScript>().explosion = true;
			des.GetComponent<DesScript>().explosionPos = obj.transform.position;
			Destroy(obj);
		}
	}

}
