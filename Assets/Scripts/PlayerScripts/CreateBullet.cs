using UnityEngine;
using System.Collections;

public class CreateBullet : MonoBehaviour {

    public Rigidbody bulletPrefab;
	public Rigidbody player;
	public GameObject playerGO;

    private const float bulletSpeed = 750.0f;

	// Use this for initialization
	void Start () {
		//playerGO = GetComponent<GameObject> ();
	}

	// Update is called once per frame
	void Update () {
		if (playerGO.GetComponent<PlayerInput>().isFiring){
			playerGO.GetComponent<PlayerInput>().isFiring = false;
			fireBullet ();
		} 
	}

    void fireBullet()
    {
		Rigidbody bulletClone;
		bulletClone = Instantiate(bulletPrefab, transform.position, transform.rotation) as Rigidbody;
		bulletClone.AddForce(transform.forward * bulletSpeed);
    }
	
	
}

