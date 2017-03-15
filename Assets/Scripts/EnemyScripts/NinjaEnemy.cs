using UnityEngine;
using System.Collections;

public class NinjaEnemy : MonoBehaviour {

	private Rigidbody rb;
	public GameObject enemy;
	public GameObject player;
	public bool isRight;
	public bool respawn = false;
	
	private const int GRAVITY = 20;
	private const int JUMPFORCE = 500;
		
	private float jumpCounter;
	private const float MAXJUMP = 2;
	
	private GlobalsScript globals;
	private GlobalMusicScript music;
	
	private Vector3 respawnLoc = new Vector3(35.0f, 3.98f, 0.0f);
	// Use this for initialization
	void Start () {
		
		rb = GetComponent<Rigidbody>();
		globals = GameObject.Find("_Globals").GetComponent<GlobalsScript>();
		music = GameObject.Find("GlobalSoundEffects").GetComponent<GlobalMusicScript>();
		
		if(respawn && globals.lastCheckpointPos == respawnLoc)
			Destroy(enemy);
		
		jumpCounter = 0;
		isRight = true;
	}
	
	// Update is called once per frame
	void Update () {
		
		if(enemy.GetComponent<Renderer>().isVisible)
		{
			if(player.transform.position.x > enemy.transform.position.x)
			{
				isRight = true;
			}
			else 
			{
				isRight = false;
			}
			
			jumpCount();
		}
	}
	
	void jumpCount(){
		
		jumpCounter += Time.deltaTime;
		
		if(jumpCounter >= MAXJUMP){
			jumpCounter = 0;
			rb.AddForce(Vector3.up * JUMPFORCE);
			GetComponent<NinjaStarCreation>().fireStar = true;
			
			if(isRight)
				enemy.transform.localEulerAngles = new Vector3(0,0,-90);
			else 
				enemy.transform.localEulerAngles = new Vector3(0,0,0);
			
			music.playJumpSound();
				
		} else {
			rb.AddForce(-Vector3.up * GRAVITY);
		}
	}
}
