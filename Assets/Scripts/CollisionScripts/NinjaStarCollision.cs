using UnityEngine;
using System.Collections;

public class NinjaStarCollision : MonoBehaviour {

	public GameObject bulletObject;
	public float damageNum = 12.5f;
	
	// Use this for initialization
	void Start () {
	
	}
	
	void OnTriggerEnter(Collider collider)
	{
		GameObject obj = collider.gameObject;

		if (obj.tag == "Ground")
		{
			Destroy(bulletObject);
		}
		
		else if (obj.tag == "SpikeWall")
		{
			Destroy(bulletObject);
		}
		
		else if (obj.tag == "Player")
		{
			obj.transform.parent.GetComponent<PlayerHUD>().damagedPlayer(damageNum);
			Destroy(bulletObject);
		}
	}
}
