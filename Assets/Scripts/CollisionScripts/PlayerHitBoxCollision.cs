using UnityEngine;
using System.Collections;

public class PlayerHitBoxCollision : MonoBehaviour {

	public GameObject hitbox;
	public GameObject player;
	public bool isFrontHitbox;
	
	// Use this for initialization
	void Start () {
	
	}
	
	void OnTriggerEnter(Collider collider)
	{
		GameObject obj = collider.gameObject;
		
		if (obj.tag == "Enemy")
		{
			if(isFrontHitbox){
				player.GetComponent<PlayerHUD>().frontHit = true;
			}else {
				player.GetComponent<PlayerHUD>().backHit = true;
			}
		}
		else if (obj.tag == "EnemyBullet")
		{
			if(isFrontHitbox){
				player.GetComponent<PlayerHUD>().frontHit = true;
			}else {
				player.GetComponent<PlayerHUD>().backHit = true;
			}
		}
		else if (obj.tag == "NinjaStar")
		{
			if(isFrontHitbox){
				player.GetComponent<PlayerHUD>().frontHit = true;
			}else {
				player.GetComponent<PlayerHUD>().backHit = true;
			}
		}
		else if (obj.tag == "Boss")
		{
			if(isFrontHitbox){
				player.GetComponent<PlayerHUD>().frontHit = true;
			}else {
				player.GetComponent<PlayerHUD>().backHit = true;
			}
		}
		else if (obj.tag == "MovingEnemy")
		{
			if(isFrontHitbox){
				player.GetComponent<PlayerHUD>().frontHit = true;
			}else {
				player.GetComponent<PlayerHUD>().backHit = true;
			}
		}
		else if (obj.tag == "Ninja")
		{
			if(isFrontHitbox){
				player.GetComponent<PlayerHUD>().frontHit = true;
			}else {
				player.GetComponent<PlayerHUD>().backHit = true;
			}
		}
		else if (obj.tag == "Bomb")
		{
			if(isFrontHitbox){
				player.GetComponent<PlayerHUD>().frontHit = true;
			}else {
				player.GetComponent<PlayerHUD>().backHit = true;
			}
		}
		else if (obj.tag == "Des")
		{
			if(isFrontHitbox){
				player.GetComponent<PlayerHUD>().frontHit = true;
			}else {
				player.GetComponent<PlayerHUD>().backHit = true;
			}
		}
		
	}
}
