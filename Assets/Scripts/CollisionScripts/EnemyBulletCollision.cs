using UnityEngine;
using System.Collections;

public class EnemyBulletCollision : MonoBehaviour {

	public GameObject bulletObject;
	public float damageNum = 10.0f;
	private GlobalMusicScript globalMusic;
	
	// Use this for initialization
	void Start () {
		globalMusic = GameObject.Find("GlobalSoundEffects").GetComponent<GlobalMusicScript>();
	}
	
	void OnTriggerEnter(Collider collider)
	{
		GameObject obj = collider.gameObject;

		if (obj.tag == "Ground")
		{
			globalMusic.playPingSound();
			Destroy(bulletObject);
		}
		else if (obj.tag == "MovingSpikeWall")
		{
			globalMusic.playPingSound();
			Destroy(bulletObject);
		}
		else if (obj.tag == "SpikeWall")
		{
			globalMusic.playPingSound();
			Destroy(bulletObject);
		}
		
		else if (obj.tag == "Player")
		{
			globalMusic.playHitSound();
			obj.transform.parent.GetComponent<PlayerHUD>().damagedPlayer(damageNum);
			Destroy(bulletObject);
		}
	}
}
