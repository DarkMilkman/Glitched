using UnityEngine;
using System.Collections;

public class EnemyBulletCreation : MonoBehaviour {

    public Rigidbody bulletPrefab;
	public GameObject enemy;
	public GameObject player;

	public bool createBullet;
	public int bulletsCreated;
	public float bulletCreationTimer;
	public float MAXBULLLETTIMER;
	public float SUCCESSIONTIMER;
	public int SUCCESSIONBULLETS;
	
	public bool notChangeDirection = false;
	
	private string direction;
	private string prevDirection;
	
    private const float bulletSpeed = 500.0f;

	// Use this for initialization
	void Start () {
		bulletCreationTimer = SUCCESSIONTIMER;
		bulletsCreated = 0;
		bulletsCreated = 0;
		
		prevDirection = "";
		direction = "";
		
		//playerGO = GetComponent<GameObject> ();
	}

	// Update is called once per frame
	void Update () {
		
		if(!notChangeDirection)
		{
			if(player.transform.position.x > transform.position.x){
				direction = "Left";
			} else if(player.transform.position.x < transform.position.x){
				direction = "Right";
			} 
		}

		if(prevDirection != direction)
			transform.Rotate(0, 180, 0);
			
		prevDirection = direction;
		
		
		if (createBullet){
			bulletCreationTimer -= Time.deltaTime;
			if(bulletCreationTimer <= 0){
				
				if(bulletsCreated < SUCCESSIONBULLETS)
					bulletCreationTimer = SUCCESSIONTIMER;
				else 
				{
					bulletsCreated = -1;
					bulletCreationTimer = MAXBULLLETTIMER;
				}
				
				
				fireBullet ();
			}
		} 
	}

    void fireBullet()
    {
		bulletsCreated++;
		
		Rigidbody bulletClone;
		bulletClone = Instantiate(bulletPrefab, transform.position, transform.rotation) as Rigidbody;
		
		GameObject.Find("GlobalSoundEffects").GetComponent<GlobalMusicScript>().playGunSound();
		//if(player.transform.position.x > transform.position.x)
			bulletClone.AddForce(transform.right * bulletSpeed);
		//else
			//bulletClone.AddForce(-transform.right * bulletSpeed);
    }
	
	public void reset(){
		
		createBullet = false;
		bulletCreationTimer = SUCCESSIONTIMER;
		bulletsCreated = 0;
	}
}
