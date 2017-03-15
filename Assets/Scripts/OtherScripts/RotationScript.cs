using UnityEngine;
using System.Collections;

public class RotationScript : MonoBehaviour {

	public float ROTATIONSPEED;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		transform.Rotate(0, Time.deltaTime * ROTATIONSPEED, 0);
	}
}
