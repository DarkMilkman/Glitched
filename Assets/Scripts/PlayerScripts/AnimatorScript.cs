using UnityEngine;
using System.Collections;

public class AnimatorScript : MonoBehaviour {

	public GameObject defaultGO;

	//public GameObject jump;
	//public GameObject land;
	public GameObject idle;
	public GameObject run;
	public GameObject shoot;
	
	private bool isShooting;
	private float counter;
	private const float MAX_SHOOT_TIME = 0.25f;
	
	// Use this for initialization
	void Start () {
		defaultGO.active = false;
		//jump.active = false;
		//land.active = false;
		run.active = false;
		idle.active = true;
		shoot.active = false;
		
		isShooting = false;
		counter = 0.0f;
	}
	
	void Update(){
		
		if(isShooting)
		{
			counter += Time.deltaTime;
			if(counter >= MAX_SHOOT_TIME)
			{
				counter = 0.0f;
				isShooting = false;
				playIdle();
			}
		}
	}
	
	public void playRun()
	{
		if(!isShooting){
		
			defaultGO.active = false;
			//jump.active = false;
			//land.active = false;
			run.active = true;
			idle.active = false;
			shoot.active = false;
		}
	}
	
	public void playIdle()
	{
		if(!isShooting){
			
			defaultGO.active = false;
			//jump.active = false;
			//land.active = false;
			run.active = false;
			idle.active = true;
			shoot.active = false;
		}
	}
	
	public void playDefault()
	{
		if(!isShooting){
			defaultGO.active = true;
			//jump.active = false;
			//land.active = false;
			run.active = false;
			idle.active = false;
			shoot.active = false;
		}
	}
	
	public void playShoot()
	{
		defaultGO.active = false;
		//jump.active = false;
		//land.active = false;
		run.active = false;
		idle.active = false;
		shoot.active = true;
		
		isShooting = true;
		counter = 0.0f;
	}
}
