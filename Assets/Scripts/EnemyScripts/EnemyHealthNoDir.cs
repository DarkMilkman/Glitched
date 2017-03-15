using UnityEngine;
using System.Collections;

public class EnemyHealthNoDir : MonoBehaviour {

	public GameObject enemy;
	
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
			
			//if(enemy.GetComponent<BombThrowerScript>().bombClone != null)
				//Destroy(enemy.GetComponent<BombThrowerScript>().bombClone);
			
			//enemy.SetActive(false);
			Destroy(enemy);
		}
	}
}
