using UnityEngine;
using System.Collections;

public class TriggerCollisions : MonoBehaviour {

	void Start () {
	
	}
	
	void OnTriggerEnter(Collider collider)
	{
		GameObject obj = collider.gameObject;//get the object that entered the trigger from the collider parameter
		//check to see if the object that entered the trigger is taged as Player
		//print(obj.tag);
		if (obj.tag == "MovingEnemy")
		{
			obj.GetComponent<EnemyMove>().changeDirection();
			//SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
		}
		
		else if (obj.tag == "Spikes")
		{
			Destroy(obj);
			//SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
		}
	}
}
