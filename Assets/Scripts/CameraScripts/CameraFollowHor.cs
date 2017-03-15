using UnityEngine;
using System.Collections;

public class CameraFollowHor : MonoBehaviour {

	public Transform target;
	private float lookAheadFactor = 0.1f;
	private float lookAheadReturnSpeed = 0.1f;
	private float lookAheadMoveThreshold = 0.1f;
	private float lerpValue = 5.0f;

	private float offsetZ;
	private Vector3 offsetY = new Vector3(0.0f, 5.0f, 0.0f);
	private Vector3 lastTargetPosition;
	private Vector3 currentVelocity;
	private Vector3 lookAheadPos;
	
	public float minX;
	public float maxX;
	public float valY;

	// Use this for initialization
	private void Start()
	{
		transform.localEulerAngles = new Vector3(20.0f, 0.0f, 0.0f);
		
		lastTargetPosition = target.position;
		offsetZ = (transform.position - target.position).z;
		transform.parent = null;
	}

	// Update is called once per frame
	private void FixedUpdate()
	{

		// only update lookahead pos if accelerating or changed direction
		float xMoveDelta = (target.position - lastTargetPosition).x;

		bool updateLookAheadTarget = Mathf.Abs(xMoveDelta) > lookAheadMoveThreshold;

		if (updateLookAheadTarget)
		{
			lookAheadPos = lookAheadFactor*Vector3.right*Mathf.Sign(xMoveDelta);
		}
		else
		{
			lookAheadPos = Vector3.MoveTowards(lookAheadPos, Vector3.zero, Time.deltaTime*lookAheadReturnSpeed);
		}

		
		Vector3 aheadTargetPos = target.position + lookAheadPos + Vector3.forward*offsetZ + offsetY;
		//Vector3 newPos = Vector3.SmoothDamp(transform.position, aheadTargetPos, ref currentVelocity, damping);
		Vector3 newPos = Vector3.Lerp(transform.position, aheadTargetPos, lerpValue * Time.deltaTime);
		
		if(newPos.x <= minX)
			newPos.x = minX;
		if(newPos.x >= maxX)
			newPos.x = maxX;
		
			newPos.y = valY;
		
		transform.position = newPos;

		lastTargetPosition = target.position;
	}
}
