using UnityEngine;
using System.Collections;

public class MovingPlatformCollision : MonoBehaviour {

	void OnTriggerEnter(Collider collider)
	{
		GameObject obj = collider.gameObject;
		
		if (obj.tag == "Ground")
		{
			obj.GetComponent<MovingPlatform>().isRight = !obj.GetComponent<MovingPlatform>().isRight;
			obj.GetComponent<MovingPlatform>().isUp = !obj.GetComponent<MovingPlatform>().isUp;
			//SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
		}
		
		else if (obj.tag == "MovingSpikeWall")
		{
			obj.GetComponent<MovingSpikeWall>().isRight = !obj.GetComponent<MovingSpikeWall>().isRight;
			obj.GetComponent<MovingSpikeWall>().isUp = !obj.GetComponent<MovingSpikeWall>().isUp;
			//SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
		}
	}
}
