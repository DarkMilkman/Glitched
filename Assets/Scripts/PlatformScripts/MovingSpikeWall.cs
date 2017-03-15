using UnityEngine;
using System.Collections;

public class MovingSpikeWall : MonoBehaviour {

	private Vector3 newPos;
	public GameObject childBox;
	
	public bool isRight = true;
	public bool isUp;
	public bool movingUp;
	public float speed = 0.3f;
	public float lerpValue = 5.0f;	
	
	// Use this for initialization
	void Start () {
		//isRight = true;
		//movingUp = false;
	}
	
	// Update is called once per frame
	void Update () {
		newPos = transform.position;
		
		if(!movingUp){
			if(isRight){
				newPos.x = transform.position.x - speed;
			} else {
				newPos.x = transform.position.x + speed;
			}
		}else{
			if(isUp){
				newPos.y = transform.position.y - speed;
			} else {
				newPos.y = transform.position.y + speed;
			}
		}
		
		Vector3 newPos2 = Vector3.Lerp(transform.position, newPos, lerpValue * Time.deltaTime);
		
		transform.position = newPos2;

		childBox.transform.localPosition = new Vector3(0.0f, 0.0f, 0.0f);		
	}
}
