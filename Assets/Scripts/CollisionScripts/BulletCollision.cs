using UnityEngine;
using System.Collections;

public class BulletCollision : MonoBehaviour {

	public GameObject bulletObject;
	public float damageNum = 6.25f;
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
		
		else if (obj.tag == "SpikeWall")
		{
			globalMusic.playPingSound();
			Destroy(bulletObject);
		}
		
		else if (obj.tag == "MovingEnemy")
		{
			obj.GetComponent<EnemyHealth>().damagebyHitNum();
			Destroy(bulletObject);
		}
		
		else if (obj.tag == "Ninja")
		{
			globalMusic.playHitSound();
			obj.GetComponent<NinjaHealth>().damagebyHitNum();
			Destroy(bulletObject);
		}
		
		else if (obj.tag == "BombThrower")
		{
			globalMusic.playHitSound();
			obj.GetComponent<EnemyHealthNoDir>().damagebyHitNum();
			Destroy(bulletObject);
		}
		
		else if (obj.tag == "Enemy")
		{
			globalMusic.playHitSound();
			Destroy(obj);
			Destroy(bulletObject);
		}
		
		else if(obj.tag == "BossWeakSpot")
		{
			globalMusic.playHitSound();
			Destroy(bulletObject);
			obj.transform.parent.GetComponent<BossScript>().takeDamage(damageNum);
		}
		
		else if(obj.tag == "Boss")
		{
			globalMusic.playPingSound();
			Destroy(bulletObject);
		}
		
		else if(obj.tag == "Des")
		{
			Destroy(bulletObject);
			obj.transform.parent.parent.GetComponent<DesScript>().takeDamage(damageNum);
		}
	}
}
