using UnityEngine;
using System.Collections;

public class CreateLevelAttackState : IDesState {

	private readonly DesStateMachine state;
	
	private GameObject boss;
	
	private Vector3 STARTPOS;
	
	private bool firstUpdate;
	
	private string phase;
	
	private float counter;
	
	private float TransitionTime = 7.5f;
	
	private Vector3 pattern1DesPos = new Vector3(0.33f, -4.47f, 0.0f);
	private Vector3 pattern2DesPos = new Vector3(5.4f, 2.57f, 0.0f);
	private Vector3 pattern3DesPos = new Vector3(-0.05f, 2.57f, 0.0f);
	private Vector3 pattern4DesPos = new Vector3(5.36f, 2.57f, 0.0f);
	
	private Vector3 pattern1PlayerPos = new Vector3(-5.42f, -4.9f, 0.0f);
	private Vector3 pattern2PlayerPos = new Vector3(-5.42f, -4.9f, 0.0f);
	private Vector3 pattern3PlayerPos = new Vector3(-5.42f, -4.9f, 0.0f);
	private Vector3 pattern4PlayerPos = new Vector3(-5.54f, 3.03f, 0.0f);
	
	private Vector3 playerResetPos = new Vector3(0.0f, -4.9f, 0.0f);
	
	public CreateLevelAttackState(DesStateMachine desStateMachine)
	{
		state = desStateMachine;
		
		boss = state.desScript.bossObject;
	}
	
	public void UpdateState()
	{
		if(!firstUpdate)
		{
			firstUpdateSettings();
		} 
		else if(phase == "Transition")
		{
			counter += Time.deltaTime;
			
			if(counter >= TransitionTime)
			{
				state.desScript.player.GetComponent<PlayerInput>().canMove = true;
				phase = "Level";
				counter = 0.0f;
			}
		}
		else if(phase == "Level")
		{
			counter += Time.deltaTime;
			if(counter >= TransitionTime)
			{
				phase = "EndTransition";
				counter = 0.0f;
			}
		}
		else if(phase == "EndTransition")
		{
			state.desScript.transitionScreen.GetComponent<MidLevelTransitions>().transitionStart = true;
			state.desScript.player.GetComponent<PlayerInput>().canMove = false;
			
			state.desScript.pattern1.active = false;
			state.desScript.pattern2.active = false;
			state.desScript.pattern3.active = false;
			state.desScript.pattern4.active = false;
			
			phase = "Wait";
			counter = 0.0f;
		}
		else if(phase == "Wait")
		{
			counter += Time.deltaTime;
			if(!state.desScript.transitionScreen.GetComponent<MidLevelTransitions>().transitionStart)
			{
				phase = "Done";
				counter = 0.0f;
			}
		}
		else if(phase == "Done")
		{ 
			Debug.Log(counter);
			state.desScript.player.GetComponent<PlayerInput>().canMove = true;
			state.desScript.player.transform.position = playerResetPos;
			
			Vector3 bossPos = new Vector3(0.0f, state.desScript.STARTING_Y_POS,0.0f);
			boss.transform.position = bossPos;
			
			firstUpdate = false;
			
			ToFloatState();
		}
	}
	
	public void ToFloatState()
	{
		state.currentState = state.floatState;
	}
	
	public void ToLightBulbAttackState()
	{
		state.currentState = state.lightBulbState;
	}

    public void ToDieAttackState()
	{
		state.currentState = state.dieState;
	}

    public void ToCreateLevelAttackState()
	{
		Debug.Log ("Cant change to same state");
	}
	
	public void firstUpdateSettings()
	{
		state.desScript.canTakeDamage = true;
		state.desScript.normalSize = true;
		state.desScript.attackPhase = "CreateLevel";
		
		state.desScript.transitionScreen.GetComponent<MidLevelTransitions>().transitionStart = true;
		state.desScript.player.GetComponent<PlayerInput>().canMove = false;
		state.desScript.globalMusic.playLightFlashSound();
		
		if(state.desScript.phaseNum <= 1)
		{
			if(Random.value < 0.5f)
			{
				state.desScript.pattern1.active = true;
				boss.transform.position = pattern1DesPos;
				state.desScript.player.transform.position = pattern1PlayerPos;
			} 
			else 
			{
				state.desScript.pattern2.active = true;
				boss.transform.position = pattern2DesPos;
				state.desScript.player.transform.position = pattern2PlayerPos;
			}
		} 
		else //if(state.desScript.phaseNum == 2)
		{
			if(Random.value < 0.5f)
			{
				state.desScript.pattern3.active = true;
				boss.transform.position = pattern3DesPos;
				state.desScript.player.transform.position = pattern3PlayerPos;
			}
			else 
			{
				state.desScript.pattern4.active = true;
				boss.transform.position = pattern4DesPos;
				state.desScript.player.transform.position = pattern4PlayerPos;
			}
		}
		
		phase = "Transition";
		firstUpdate = true;
		counter = 0.0f;
	}
}
