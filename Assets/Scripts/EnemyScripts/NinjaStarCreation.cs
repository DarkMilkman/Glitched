using UnityEngine;
using System.Collections;

public class NinjaStarCreation : MonoBehaviour {

    public Rigidbody bulletPrefab;
	public GameObject enemy;
	public GameObject player;
	
	private float timer;
	private const float MAXTIME = 0.35f;
	
	public bool fireStar;
    private const float bulletSpeed = 750.0f;
	
	// Use this for initialization
	void Start () {
		fireStar = false;
		timer = 0.0f;
	}
	
	void Update(){
		if(fireStar){
			timer += Time.deltaTime;
			
			if(timer >= MAXTIME){
				fireBullet();
				fireStar = false;
				timer = 0.0f;
			}
		}
	}
	
	void fireBullet() {
		Rigidbody bulletClone;
		//bulletClone = Instantiate(bulletPrefab, transform.position, transform.rotation) as Rigidbody;
		
		if(enemy.GetComponent<NinjaEnemy>().isRight)
		{	
			enemy.transform.localEulerAngles = new Vector3(0,0,-180);
			Quaternion rot = Quaternion.Euler(0, 0, 0);
			bulletClone = Instantiate(bulletPrefab, transform.position, rot) as Rigidbody;
			bulletClone.AddForce(new Vector3(1, -1, 0) * bulletSpeed);
		}
		else 
		{
			enemy.transform.localEulerAngles = new Vector3(0,0,90);
			Quaternion rot = Quaternion.Euler(0, 0, -90);
			bulletClone = Instantiate(bulletPrefab, transform.position, rot) as Rigidbody;
			bulletClone.AddForce(new Vector3(-1, -1, 0) * bulletSpeed);
		}
	}
}
