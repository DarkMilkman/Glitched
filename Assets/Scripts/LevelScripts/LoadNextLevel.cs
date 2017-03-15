using UnityEngine;
using System.Collections;

public class LoadNextLevel : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	void OnTriggerEnter(Collider collider)
	{
		GameObject obj = collider.gameObject;//get the object that entered the trigger from the collider parameter
		//check to see if the object that entered the trigger is taged as Player
		if (obj.tag == "Player")
		{
			GameObject.Find("_Globals").GetComponent<GlobalsScript>().lastLevelPlayed++;
			GameObject.Find("_Globals").GetComponent<GlobalsScript>().resetCheckpointPos();
			Application.LoadLevel(Application.loadedLevel + 1);
			//SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
		}
	}
}
