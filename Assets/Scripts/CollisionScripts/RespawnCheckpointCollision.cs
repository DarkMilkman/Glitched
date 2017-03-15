using UnityEngine;
using System.Collections;

public class RespawnCheckpointCollision : MonoBehaviour {

	public GameObject respawn;
	
	void OnTriggerStay(Collider collider)
	{
		GameObject obj = collider.gameObject;//get the object that entered the trigger from the collider parameter
		//check to see if the object that entered the trigger is taged as Player
		if (obj.tag == "Player")
		{
			Color color = respawn.GetComponent<Renderer>().material.color;
			if(color.a != 0.25f)
			{
				GameObject.Find("GlobalSoundEffects").GetComponent<GlobalMusicScript>().playCheckPointSound();
				GameObject.Find("_Globals").GetComponent<GlobalsScript>().lastCheckpointPos = transform.position;
			}
			
			//Color color = respawn.GetComponent<Renderer>().material.color;
			color.a = 0.25f;
			respawn.GetComponent<Renderer>().material.color = color;
			
			//print(transform.position);
			//GameObject.Find("_Globals").GetComponent<GlobalsScript>().lastCheckpointPos = transform.position;
			//SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
		}
	}
}
