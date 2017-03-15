using UnityEngine;
using System.Collections;

public class StationaryEnemyCollision : MonoBehaviour {

	// Use this for initialization
	public GameObject enemy;
	
	void Start () {
	
	}
	
	void OnTriggerEnter(Collider collider)
	{
		GameObject obj = collider.gameObject;
		if (obj.tag == "Player")
		{
			enemy.GetComponent<EnemyBulletCreation>().createBullet = true;
		}
	}
	
	void OnTriggerExit(Collider collider)
	{
		GameObject obj = collider.gameObject;
		if (obj.tag == "Player")
		{
			enemy.GetComponent<EnemyBulletCreation>().reset();
		}
	}
}