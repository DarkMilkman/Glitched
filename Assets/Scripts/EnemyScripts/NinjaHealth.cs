using UnityEngine;
using System.Collections;

public class NinjaHealth : MonoBehaviour {

	public GameObject enemy;
	public GameObject player;
	
	private int healthpoints;
	public int MAXHEALTHPOINTS;
	
	// Use this for initialization
	void Start () {
		
		healthpoints = MAXHEALTHPOINTS;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	public void damagebyHitNum(){
		
		healthpoints--;
		//print(healthpoints);
		if(healthpoints <= 0){
			Destroy(enemy);
		}
	}
}
