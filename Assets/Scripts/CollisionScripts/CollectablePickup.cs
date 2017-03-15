using UnityEngine;
using System.Collections;

public class CollectablePickup : MonoBehaviour {

	public GameObject collectable;
	public float regainHealthNum;
	
	// Use this for initialization
	void Start () {
	
	}
	
	void OnTriggerEnter(Collider collider)
	{
		GameObject obj = collider.gameObject;

		if (obj.tag == "Player")
		{
			GameObject.Find("GlobalSoundEffects").GetComponent<GlobalMusicScript>().playHealthSound();
			//GameObject.Find("_Globals").GetComponent<GlobalsScript>().addToCollectableList(collectable);
			obj.transform.parent.GetComponent<PlayerHUD>().regainHealth(regainHealthNum);
			Destroy(collectable);
		}
	}
}
