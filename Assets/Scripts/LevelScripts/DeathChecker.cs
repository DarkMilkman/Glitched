using UnityEngine;
using System.Collections;

public class DeathChecker : MonoBehaviour {

	void Start()
	{
	}

	void OnTriggerEnter(Collider collider)
	{
		GameObject obj = collider.gameObject;//get the object that entered the trigger from the collider parameter
		//check to see if the object that entered the trigger is taged as Player
		if (obj.tag == "Player")
		{
			Application.LoadLevel("DeathScreen");
			//SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
		}
	}
}