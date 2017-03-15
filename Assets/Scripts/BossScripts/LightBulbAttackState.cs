using UnityEngine;
using System.Collections;

public class LightBulbAttackState : IDesState {

	private readonly DesStateMachine state;
	
	private GameObject boss;
	
	private Vector3 STARTPOS;
	
	private bool firstUpdate;
	
	private string phase;
	
	private int angle;
	
	private Vector3 FULLSIZE = new Vector3(4.0f, 4.0f, 4.0f);
	private Vector3 NORMALSIZE = new Vector3(1.0f, 1.0f, 1.0f);
	
	private Vector3 WINDUPPOS = new Vector3(0.05f, 2.04f, 0.0f);
	private Vector3 SWINGPOS = new Vector3(1.0f, 0.0f, 0.0f);
	
	private float GROW_LERP_VAL = 21.3f;
	private float LERPSPEED = 3.0f;
	private float WINDUP_LERP_VAL = 6.3f;
	private float SWING_LERP_VAL = 10.0f;
	
	private const float WIGGLEROOM = 0.3f;
	
	private float counter;
	
	
	public LightBulbAttackState(DesStateMachine desStateMachine)
	{
		state = desStateMachine;
			
		boss = state.desScript.bossObject;
	}
	
	public void UpdateState()
	{
		if(!firstUpdate)
		{
			counter = 0.0f;
			firstUpdateSettings();
		} 
		else if(phase == "MovingInPlace")
		{
			Vector3 pos = boss.transform.position;
			
			Vector3 newPos = Vector3.Lerp(pos, STARTPOS, LERPSPEED * Time.deltaTime);
			
			boss.transform.position = newPos;
			
			//nPos.y > bossPos.y - WIGGLEROOM && nPos.y < bossPos.y + WIGGLEROOM
			if(newPos.y > STARTPOS.y - WIGGLEROOM && newPos.y < STARTPOS.y + WIGGLEROOM)
			{
				phase = "Growing";
			}
		}
		else if(phase == "Growing")
		{
			counter += Time.deltaTime;
			
			Vector3 size = boss.transform.localScale;
			
			Vector3 newSize = Vector3.Lerp(size, FULLSIZE, GROW_LERP_VAL * Time.deltaTime);
			
			boss.transform.localScale = newSize;
			
			if(newSize == FULLSIZE)
			{
				//Debug.Log("Grow");
				//Debug.Log(counter);
				//counter = 0.0f;
				phase = "WindUp";
			}
		}
		else if(phase == "WindUp")
		{
			//counter += Time.deltaTime;
			
			Vector3 lightPos = state.desScript.bulb.transform.localPosition;
			state.desScript.bulb.transform.localEulerAngles = new Vector3(0,0,0);
			
			Vector3 newlightPos = Vector3.Lerp(lightPos, WINDUPPOS, WINDUP_LERP_VAL * Time.deltaTime);
				
			state.desScript.bulb.transform.localPosition = newlightPos;
			
			if(newlightPos == WINDUPPOS)
			{
				//Debug.Log("WindUP");
				//Debug.Log(counter);
				counter = 0.0f;
				phase = "SWING";
			}
		}
		else if(phase == "SWING")
		{
			//counter ++;
			
			Vector3 lightPos = state.desScript.bulb.transform.localPosition;
			
			angle -= 10;
			
			state.desScript.bulb.transform.localEulerAngles = new Vector3(0,0,angle);
			
			Vector3 newlightPos = Vector3.Lerp(lightPos, SWINGPOS, SWING_LERP_VAL * Time.deltaTime);
				
			state.desScript.bulb.transform.localPosition = newlightPos;
			
			if(newlightPos.y > SWINGPOS.y - WIGGLEROOM && newlightPos.y < SWINGPOS.y + WIGGLEROOM)
			{
				//Debug.Log("Swing");
				//Debug.Log(counter);
				counter = 0.0f;
				phase = "DoneSwing";
			}
		}
		else if(phase == "DoneSwing")
		{
			counter += Time.deltaTime;
			
			if(counter >= 0.5f)
			{
				counter = 0;
				phase = "Shrink";
			}
		}
		else if(phase == "Shrink")
		{
			//counter += Time.deltaTime;
			
			state.desScript.bulb.transform.localPosition = state.desScript.BULBSTARTPOS;
			state.desScript.bulb.transform.localEulerAngles = state.desScript.BULBSTARTANGLE;
			
			Vector3 size = boss.transform.localScale;
			
			Vector3 newSize = Vector3.Lerp(size, NORMALSIZE, GROW_LERP_VAL * Time.deltaTime);
			
			boss.transform.localScale = newSize;
			
			if(newSize == NORMALSIZE)
			{
				//Debug.Log("Shrink");
				//Debug.Log(counter);
				counter = 0.0f;
				phase = "Wait";
				state.desScript.canTakeDamage = true;
				state.desScript.normalSize = true;
			}
		}
		else if(phase == "Wait")
		{
			counter += Time.deltaTime;
			
			if(counter >= 1.0f)
			{
				phase = "Done";
				counter = 0.0f;
			}
		}
		else if(phase == "Done")
		{
			Vector3 pos = new Vector3(boss.transform.position.x, state.desScript.STARTING_Y_POS, 0.0f);
			
			Vector3 newPos = Vector3.Lerp(boss.transform.position, pos, LERPSPEED * Time.deltaTime);
		
			boss.transform.position = newPos;
			
			if(newPos.y > pos.y - WIGGLEROOM && newPos.y < pos.y + WIGGLEROOM)
			{
				firstUpdate = false;
				ToFloatState();
			}
			
		}
		
	}
	
	public void ToFloatState()
	{
		state.currentState = state.floatState;
	}
	
	public void ToLightBulbAttackState()
	{
		Debug.Log ("Cant change to same state");
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
		state.desScript.canTakeDamage = false;
		state.desScript.normalSize = false;
		state.desScript.attackPhase = "LightBulb";
		
		if(Random.value < 0.5f)
		{
			STARTPOS = new Vector3(-5.0f, -5.43f, 0.0f);
			boss.transform.localEulerAngles = new Vector3(0,0,0);
		} 
		else 
		{
			STARTPOS = new Vector3(4.68f, -5.43f, 0.0f);
			boss.transform.localEulerAngles = new Vector3(0,180,0);
		}
		
		phase = "MovingInPlace";
		firstUpdate = true;
		angle = 0;
	}
}
