using UnityEngine;
using System.Collections;

public class BossLv1Collision : MonoBehaviour {

	void OnTriggerEnter(Collider collider)
	{	
		GameObject obj = collider.gameObject;
		if (obj.tag == "Boss")
		{
			obj.GetComponent<BossScript>().isRight = !obj.GetComponent<BossScript>().isRight;
			//obj.GetComponent<BossScript>().bossObject.GetComponent<Transform>().Rotate(0, 180, 0);
			//SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
		}
	}
}
