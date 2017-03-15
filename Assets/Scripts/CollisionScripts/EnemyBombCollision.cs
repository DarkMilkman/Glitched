using UnityEngine;
using System.Collections;

public class EnemyBombCollision : MonoBehaviour {

	public GameObject bulletObject;
	public GameObject enemy;
	public float damageNum = 16.7f;
	
	// Use this for initialization
	void Start () {
	
	}
	
	void OnTriggerEnter(Collider collider)
	{
		GameObject obj = collider.gameObject;
	
		if (obj.tag == "Player")
		{
			GameObject.Find("GlobalSoundEffects").GetComponent<GlobalMusicScript>().playBombExplodeSound();
			obj.transform.parent.GetComponent<PlayerHUD>().damagedPlayer(damageNum);
			Destroy(bulletObject);
		} 
		else if (obj.tag == "Bomb")
		{
			GameObject.Find("GlobalSoundEffects").GetComponent<GlobalMusicScript>().playBombExplodeSound();
			Destroy(obj);
			//enemy.GetComponent<BombCreation>().bombCreated = false;
			Destroy(bulletObject);
			//enemy.GetComponent<BombThrowerScript>().bounceBomb();
			//Destroy(bulletObject);
		}
	}
}
