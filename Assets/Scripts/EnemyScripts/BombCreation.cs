using UnityEngine;
using System.Collections;

public class BombCreation : MonoBehaviour {

	public GameObject bomb;
	private GameObject bombClone;
	
	public void instaniateBomb(bool isFacingRight, Vector3 force, Vector3 force2){
		
		bombClone = Instantiate(bomb, transform.position, transform.rotation) as GameObject;
		bombClone.GetComponent<BombUpdate>().isRight = isFacingRight;
		
		if(isFacingRight)
			bombClone.GetComponent<Rigidbody>().AddForce(force);
		else 
			bombClone.GetComponent<Rigidbody>().AddForce(force2);
	}
}
