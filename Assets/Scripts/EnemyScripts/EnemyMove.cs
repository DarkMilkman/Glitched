using UnityEngine;
using System.Collections;

public class EnemyMove : MonoBehaviour {

	private Vector3 newPos;
	public bool isRight;
	public Animator animator;
	
	public float lerpValue = 5.0f;	
	private const float speed = 0.5f;
	
	// Use this for initialization
	void Start () {
		isRight = true;
		animator.transform.Rotate(0, 180, 0);
	}
	
	public void changeDirection(){
		isRight = !isRight;
		FlipHorizontal();
	}
	
	// Update is called once per frame
	void Update () {
		newPos = transform.position;
		
		if(isRight){
			newPos.x = transform.position.x - speed;
		} else {
			newPos.x = transform.position.x + speed;
		}
		
		Vector3 newPos2 = Vector3.Lerp(transform.position, newPos, lerpValue * Time.deltaTime);
		
		transform.position = newPos2;	
	}
	
	void FlipHorizontal()
	{
		animator.transform.Rotate(0, 180, 0);
	}
}
