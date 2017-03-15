using UnityEngine;
using System.Collections;

public class SpikesVertAttackState : IBossState 
{
	private Vector3 bossPos = new Vector3(0.0f, -4.0f, 3.0f);//-4.66
	private Vector3 pos = new Vector3(0.0f, -4.6f, 0.0f);
	private const int MAXTIMEBEFORESPIKES = 2;//3
	private const int MAXTIMETOSWITCH = 8;
	private float switchCounter;
	private float spikesBeginCounter;
	
	private bool firstUpdate;
	private bool initSpikes;
	private bool firstSpikesUpdate;
	
	private bool changeStates;
	private float timer;
	private const float CHANGEY = 0.2f;
	private const float CHANGEYMULT = 2.0f;
	private const float MAXTIME = 1.2f;
	private const float WIGGLEROOM = 0.5f;
	
	private bool isRight = false;
	
	private readonly AttackStateMachine state;
	private bool firstJumpSF;
	
    public SpikesVertAttackState (AttackStateMachine attackStateMachine)
    {
        state = attackStateMachine;
		firstUpdate = true;
		initSpikes = false;
		firstJumpSF = true;
		firstSpikesUpdate = true;
		changeStates = false;
		
		switchCounter = MAXTIMETOSWITCH;
		spikesBeginCounter = MAXTIMEBEFORESPIKES;
    }
	
	public void UpdateState()
	{
		if(firstUpdate)
		{
			Vector3 nPos = state.bossScript.transform.position;
			nPos.x = 0.0f;
			nPos.y -= CHANGEY;
			nPos.z = 3.0f;
			state.bossScript.transform.position = nPos;
			
			if(nPos.y > bossPos.y - WIGGLEROOM && nPos.y < bossPos.y + WIGGLEROOM){
				isRight = false;
				
				state.bossScript.globalMusic.playSpikeKingWarningSound();
			
				if(Random.value < 0.5f){
					state.bossScript.popUpZipR = true;
					isRight = true;
				} else {
					state.bossScript.popUpZipL = true;
				}
			
				firstUpdate = false;
			}
		}
		
		if(!firstUpdate)
			spikesBeginCounter -= Time.deltaTime;
		
		if(spikesBeginCounter <= 0)
		{
			initSpikes = true;
		}
	
		if(initSpikes && firstSpikesUpdate)
		{
			firstSpikesUpdate = false;
			
			Quaternion rot = Quaternion.Euler(0, 0, 0);
			string dir;
			Vector3 pos;
			
			for(int i = 0; i < 18; i++)
			{
				if(isRight)
				{
					dir = "RightV";
					rot = Quaternion.Euler(0, 270, 0);
					pos = new Vector3(9.0f, 8f - i, -0.5f);
				}
				else 
				{
					dir = "LeftV";
					rot = Quaternion.Euler(0, 90, 0);
					pos = new Vector3(-8.0f, 8f - i, -0.5f);
				}
				
				state.bossScript.instantiateSpike(pos, dir, rot);
			}	
			
			rot = Quaternion.Euler(0, 0, 90);
			if(isRight)
			{
				dir = "RightV";
				pos = new Vector3(8.5f, 0f, -0.5f);
			}
			else
			{
				dir = "LeftV";
				pos = new Vector3(-7.5f, 0f, -0.5f);
			}
			state.bossScript.instantiateSpikeHitBox(pos, dir, rot);
		}
		
		else if(initSpikes && !firstSpikesUpdate)
		{
			switchCounter -= Time.deltaTime;
			
			if(switchCounter <= 0)
			{	
				timer += Time.deltaTime;
				if(timer >= MAXTIME){
					timer = 0.0f;
					changeStates = true;
				}
				else 
				{
					if(firstJumpSF){
						firstJumpSF = false;
						state.bossScript.globalMusic.playSpikeKingJumpSound();
					}
				
					Vector3 nPos = state.bossScript.transform.position;
					nPos.y += CHANGEY;
					state.bossScript.transform.position = nPos;
				}
			}
			
			if(changeStates)
			{
			
				Vector3 nPos = state.bossScript.transform.position;
				nPos.x = 0.0f;
				nPos.y -= CHANGEY * CHANGEYMULT;
				nPos.z = 0.0f;
				state.bossScript.transform.position = nPos;
				
				if(nPos.y > pos.y - WIGGLEROOM && nPos.y < pos.y + WIGGLEROOM){
					resetValues();
					ToJumpAttackState();
				}
			}
		}
	}

	public void ToJumpAttackState()
	{
		state.currentState = state.jumpState;
	}

    public void ToSpikesHorAttackState()
	{
		state.currentState = state.horState;
	}

    public void ToSpikesVertAttackState()
	{
		Debug.Log ("Cant change to same state");
	}
	
	private void resetValues()
	{
		firstJumpSF = true;
		
		changeStates = false;
		firstUpdate = true;
		initSpikes = false;
		firstSpikesUpdate = true;
		
		switchCounter = MAXTIMETOSWITCH;
		spikesBeginCounter = MAXTIMEBEFORESPIKES;
	}
}