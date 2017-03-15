using UnityEngine;
using System.Collections;

public class FloatState : IDesState {

	private readonly DesStateMachine state;
	
	private GameObject boss;
	
	private Vector3 STARTPOS;// = new Vector3(0.0f, state.desScript.STARTING_Y_POS, 0.0f);
	
	private bool firstUpdate;
	private bool isMovingUp;
	
	private float movingUpCounter;
	private float MAXMOVINGUPCOUNTER = 1.0f;
	
	private float FLOATHEIGHT = 0.2f;
	
	private float FLOATSPEED = 1.0f;
	private float LERPSPEED = 3.5f;
	
	private float floatTimeCounter;
	private float MAXFLOATTIME;
	
	private string prevAttack;
	
	public FloatState(DesStateMachine desStateMachine)
	{
		state = desStateMachine;
		
		boss = state.desScript.bossObject;
		
		prevAttack = "Die";
	}
	
	public void UpdateState()
	{
		if(!firstUpdate)
		{
			firstUpdateSettings();
		}
		
		Vector3 pos = boss.transform.position; 
		
		if(state.desScript.isRight)
		{
			pos.x += FLOATSPEED;
		} 
		else 
		{
			pos.x -= FLOATSPEED;
		}
		
		if(isMovingUp)
		{
			pos.y += FLOATHEIGHT;
		}
		else
		{
			pos.y -= FLOATHEIGHT;
		}
		
		
		Vector3 newPos2 = Vector3.Lerp(boss.transform.position, pos, LERPSPEED * Time.deltaTime);
		
		boss.transform.position = newPos2;
		
		movingUpCounter += Time.deltaTime;
		if(movingUpCounter >= MAXMOVINGUPCOUNTER)
		{
			movingUpCounter = 0.0f;
			isMovingUp = !isMovingUp;
		}
		
		floatTimeCounter += Time.deltaTime;
		if(floatTimeCounter >= MAXFLOATTIME)
		{
			firstUpdate = false;
			if(state.desScript.phaseNum == 0)
			{
				if(prevAttack == "Light")
				{
					prevAttack = "Die";
					ToDieAttackState();
				}
				else
				{
					prevAttack = "Light";
					ToLightBulbAttackState();
				}
			}
			else if(state.desScript.phaseNum == 1)
			{
				if(prevAttack == "Die")
				{
					prevAttack = "Light";
					ToLightBulbAttackState();
				} 
				else if(prevAttack == "Light")
				{
					prevAttack = "CreateLevel";
					ToCreateLevelAttackState();
				} 
				else 
				{
					prevAttack = "Die";
					ToDieAttackState();
				}
			}
			else if(state.desScript.phaseNum == 2)
			{
				//Debug.Log(prevAttack);
				if(prevAttack == "CreateLevel")
				{
					if(Random.value < 0.5f)
					{
						prevAttack = "Light";
						ToLightBulbAttackState();
					} 
					else
					{
						prevAttack = "Die";
						ToDieAttackState();
					}
				} 
				else //if(prevAttack == "Light" || prevAttack == "Die")
				{
					prevAttack = "CreateLevel";
					ToCreateLevelAttackState();
				} 
			}
			
			//Debug.Log(state.desScript.phaseNum);
		}
	}
	
	public void ToFloatState()
	{
		Debug.Log ("Cant change to same state");
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
		state.currentState = state.createLevelState;
	}
	
	public void firstUpdateSettings()
	{
		STARTPOS = new Vector3(boss.transform.position.x, state.desScript.STARTING_Y_POS, 0.0f);
		boss.transform.position = STARTPOS;
		
		state.desScript.normalSize = true;
		state.desScript.canTakeDamage = true;
		state.desScript.attackPhase = "Float";
		
		if(state.desScript.phaseNum == 0)
		{
			MAXFLOATTIME = state.desScript.PHASE1FLOATTIME;
		} 
		else if(state.desScript.phaseNum == 1)
		{
			MAXFLOATTIME = state.desScript.PHASE2FLOATTIME;
		} 
		else if(state.desScript.phaseNum == 2)
		{
			MAXFLOATTIME = state.desScript.PHASE3FLOATTIME;
		}
		
		floatTimeCounter = 0.0f;
		movingUpCounter = 0.0f;
		isMovingUp = true;
		firstUpdate = true;
	}
}