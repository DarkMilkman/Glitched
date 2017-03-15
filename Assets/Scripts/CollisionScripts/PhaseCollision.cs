using UnityEngine;
using System.Collections;

public class PhaseCollision : MonoBehaviour {

	public GameObject player;
	private PlayerPowers powers;
	
	void Start () {
		powers = player.GetComponent<PlayerPowers>();
	}
	
	void OnTriggerStay(Collider col)
	{
		//print("enter");
		GameObject obj = col.gameObject;
		if (obj.tag == "Ground")
		{
			//print("can't phase");
			powers.canPhase = false;
		}
		if (obj.tag == "SpikeWall")
		{
			//print("can't phase");
			powers.canPhase = false;
		}
	}
	
	void OnTriggerExit(Collider col)
	{
		//print("exit");
		GameObject obj = col.gameObject;
		if (obj.tag == "Ground")
		{
			//print("can phase");
			powers.canPhase = true;
		}
		
		if (obj.tag == "SpikeWall")
		{
			//print("can't phase");
			powers.canPhase = true;
		}
	}
}
