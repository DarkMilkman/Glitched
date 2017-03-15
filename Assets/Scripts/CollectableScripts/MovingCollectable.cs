using UnityEngine;
using System.Collections;

public class MovingCollectable : MonoBehaviour {

	public bool isMoving = false;
	public float platformSpeed;
	public bool isMovingRight;
	public bool isMovingUp;
	
	private Rigidbody rb;
	private float lerpValue = 5.0f;
	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {
		if(isMoving) updateMovingPlatform();
	}
	
	void updateMovingPlatform(){
		Vector3 newPos = rb.transform.position;
		if(!isMovingUp){
			if(isMovingRight){
				newPos.x -= platformSpeed;
			} else if(!isMovingRight){
				newPos.x += platformSpeed;
			} 
		}
		
		Vector3 newPos2 = Vector3.Lerp(transform.position, newPos, lerpValue * Time.deltaTime);
			
		rb.transform.position = newPos2;
	}
}
