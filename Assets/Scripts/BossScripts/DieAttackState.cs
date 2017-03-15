using UnityEngine;
using System.Collections;

public class DieAttackState : IDesState {

	private readonly DesStateMachine state;
	
	private GameObject boss;
	
	private Vector3 STARTPOS;
	
	private bool firstUpdate;
	
	private string phase;
	
	private int angle;
	
	private string dir;
	
	private Vector3 FULLSIZE = new Vector3(4.0f, 4.0f, 4.0f);
	private Vector3 NORMALSIZE = new Vector3(1.0f, 1.0f, 1.0f);
	
	private Vector3 WINDUPPOS = new Vector3(-0.28f, 0.90f, 0.27f);
	//private Vector3 SWINGPOS = new Vector3(1.0f, 0.0f, 0.0f);
	
	private float GROW_LERP_VAL = 21.3f;
	private float LERPSPEED = 3.0f;
	private float WINDUP_LERP_VAL = 6.3f;
	private float SWING_LERP_VAL = 14.4f;
	
	private const float WIGGLEROOM = 0.3f;
	
	private float counter;
	
	public DieAttackState(DesStateMachine desStateMachine)
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
		else if(phase == "MovingInPlace")
		{
			Vector3 pos = boss.transform.position;
			
			Vector3 newPos = Vector3.Lerp(pos, STARTPOS, LERPSPEED * Time.deltaTime);
			
			boss.transform.position = newPos;
			
			if(newPos.y > STARTPOS.y - WIGGLEROOM && newPos.y < STARTPOS.y + WIGGLEROOM)
			{
				phase = "Growing";
			}
		}
		else if(phase == "Growing")
		{	
			Vector3 size = boss.transform.localScale;
			
			Vector3 newSize = Vector3.Lerp(size, FULLSIZE, GROW_LERP_VAL * Time.deltaTime);
			
			boss.transform.localScale = newSize;
			
			if(newSize == FULLSIZE)
			{
				phase = "WindUp";
			}
		}
		else if(phase == "WindUp")
		{
			state.desScript.dom.transform.localEulerAngles = new Vector3(20,16,310);
			
			Vector3 lightPos = state.desScript.dom.transform.localPosition;

			Vector3 newlightPos = Vector3.Lerp(lightPos, WINDUPPOS, WINDUP_LERP_VAL * Time.deltaTime);
				
			state.desScript.dom.transform.localPosition = newlightPos;
			
			if(newlightPos == WINDUPPOS)
			{
				phase = "Throw";
			}
		}
		else if(phase == "Throw")
		{
			phase = "Follow";
			
			Quaternion rot = Quaternion.Euler(20,16,310);
			Vector3 pos = state.desScript.dom.transform.position;
			pos.z = 0.0f;
			state.desScript.globalMusic.playDiceThrowSound();
			
			state.desScript.instantiateBomb(pos, dir, rot);
		}
		else if(phase == "Follow")
		{
			if(state.desScript.explosion)
			{
				phase = "Explosion";
				counter = 0.0f;
				
				state.desScript.instantiateExplosion(state.desScript.explosionPos);
				state.desScript.explosion = false;
			}
		} 
		else if(phase == "Explosion")
		{
			counter += Time.deltaTime;
			if(counter >= 0.75)
			{
				state.desScript.DestroyExplosionClone();
				phase = "Shrink";
			}
		}
		else if(phase == "Shrink")
		{
			state.desScript.dom.transform.localPosition = state.desScript.DOMSTARTPOS;
			state.desScript.dom.transform.localEulerAngles = state.desScript.DOMSTARTANGLE;
			
			Vector3 size = boss.transform.localScale;
			
			Vector3 newSize = Vector3.Lerp(size, NORMALSIZE, GROW_LERP_VAL * Time.deltaTime);
			
			boss.transform.localScale = newSize;
			
			if(newSize == NORMALSIZE)
			{
				state.desScript.canTakeDamage = true;
				state.desScript.normalSize = true;
				phase = "Wait";
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
		state.currentState = state.lightBulbState;
	}

    public void ToDieAttackState()
	{
		Debug.Log ("Cant change to same state");
	}

    public void ToCreateLevelAttackState()
	{
		state.currentState = state.createLevelState;
	}
	
	public void firstUpdateSettings()
	{
		state.desScript.canTakeDamage = false;
		state.desScript.normalSize = false;
		state.desScript.attackPhase = "Die";
		
		if(Random.value < 0.5f)
		{
			STARTPOS = new Vector3(-5.0f, -5.43f, 0.0f);
			boss.transform.localEulerAngles = new Vector3(0,0,0);
			dir = "Left";
		} 
		else 
		{
			STARTPOS = new Vector3(4.68f, -5.43f, 0.0f);
			boss.transform.localEulerAngles = new Vector3(0,180,0);
			dir = "Right";
		}
		
		phase = "MovingInPlace";
		firstUpdate = true;
		angle = 0;
		counter = 0.0f;
	}
}

