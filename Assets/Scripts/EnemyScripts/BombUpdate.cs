using UnityEngine;
using System.Collections;

public class BombUpdate : MonoBehaviour {

	//public GameObject enemy;
	public GameObject bomb;
	
	public bool isRight;
	private bool hitGround; 
	
	private Vector3 newRD2Bounce = new Vector3(0.7f, 2.0f, 0.0f);
	private Vector3 newLD2Bounce = new Vector3(-0.7f, 2.0f, 0.0f);
	
	private float RAYCASTLIMIT = 0.25f;
	
	private float bounceCounter;
	private const float MAXBOUNCETIME = 0.25f;
	
	private float groundCounter;
	private const float MAXGROUNDCOUNTER = 0.25f;
	
	public int MAXBOUNCES = 1;
	private int bounces;
	
	private const int FORCE2 = 340;//335
	
	private const int GRAVITY = 15;
	private const int GRAVITY2 = 20;//20;
	
	public LayerMask ground;
	private GlobalMusicScript music;
	
	// Use this for initialization
	void Start () {
		
		bounces = 0;
		bounceCounter = 0.0f;
		groundCounter = 0.0f;
		
		hitGround = false;
		music = GameObject.Find("GlobalSoundEffects").GetComponent<GlobalMusicScript>();
	}
	
	void Update(){
		
		checkCollision();
	}
	
	void FixedUpdate(){
		
		if(hitGround){
			groundCounter += Time.deltaTime;
			
			if(groundCounter >= MAXGROUNDCOUNTER){
				groundCounter = 0.0f;
				hitGround = false;
			}
		}
		
		//checkCollision();
		updateBomb();
	}
	
	void checkCollision(){
		
		Vector3 adjustPos = bomb.transform.position; 
		
		bool groundHit = false;
		if(!hitGround)
			groundHit = Physics.Raycast (adjustPos, -Vector3.up, RAYCASTLIMIT, ground);
		
		bool wallHitR = Physics.Raycast (adjustPos, Vector3.right, RAYCASTLIMIT, ground);
		bool wallHitL = Physics.Raycast (adjustPos, -Vector3.right, RAYCASTLIMIT, ground);
		
		Debug.DrawRay(adjustPos,-Vector3.up*RAYCASTLIMIT,Color.black);	
		Debug.DrawRay(adjustPos,Vector3.right*RAYCASTLIMIT,Color.black);	
		Debug.DrawRay(adjustPos,-Vector3.right*RAYCASTLIMIT,Color.black);	
		
		
		if(groundHit){
			hitGround = true;
			bounceBomb();
		}
		
		if(wallHitR || wallHitL){
			music.playBombExplodeSound();
			Destroy(bomb);
		}
	}
	
	public void bounceBomb(){
		
		if(bounces < MAXBOUNCES){
			
			Vector3 newForce;
			if(isRight)
				newForce = newRD2Bounce * FORCE2;
			else 
				newForce = newLD2Bounce * FORCE2;
			
				bomb.GetComponent<Rigidbody>().AddForce(newForce);
	
			bounces++;
			
		} else {
			music.playBombExplodeSound();
			Destroy(bomb);
		}
	}
	
	void updateBomb(){
		
		if(bounces == 0)
			bomb.GetComponent<Rigidbody>().AddForce(-Vector3.up * GRAVITY);
		else 
			bomb.GetComponent<Rigidbody>().AddForce(-Vector3.up * GRAVITY2);
	}
}
