using UnityEngine;
using System.Collections;

public class EnemyHealth : MonoBehaviour {

	public GameObject enemy;
	public GameObject player;
	
	private int healthpoints;
	public int MAXHEALTHPOINTS;
	
	private GlobalMusicScript globalMusic;
	
	// Use this for initialization
	void Start () {
		globalMusic = GameObject.Find("GlobalSoundEffects").GetComponent<GlobalMusicScript>();
		healthpoints = MAXHEALTHPOINTS;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	public void damagebyHitNum(){
		
		if(player.transform.position.x > transform.position.x && GetComponent<EnemyMove>().isRight
		|| player.transform.position.x < transform.position.x && !GetComponent<EnemyMove>().isRight){
			
			healthpoints--;
			globalMusic.playHitSound();
			
			if(healthpoints <= 0){
				Destroy(enemy);
			}
		}
		else
		{
			globalMusic.playPingSound();
		}
	}
}
