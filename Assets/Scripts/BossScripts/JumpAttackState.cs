using UnityEngine;
using System.Collections;
	
public class JumpAttackState : IBossState 
{
	private const int JUMPFORCE = 610;
	private float jumpTimeCounter;//1/4 of a sec
	private const float JUMPTIME = 0.25f;
	
	private bool startAttacking;
	private float startAttackingTimer;
	private const int STARTATTACKTIME = 2;
	
	private const int MAXJUMPCOUNTER = 3;	
	private int jumpCounter;
	
	private bool changeStates;
	private float timer;
	private const float CHANGEY = 0.2f;
	private const float MAXTIME = 1.2f;
	
	private Vector3 newPos;	
	private const float SPEED = 110f;
	
	private string prevState;
		
	private readonly AttackStateMachine state;
	
	private bool firstJumpSF;

    public JumpAttackState (AttackStateMachine attackStateMachine)
    {
        state = attackStateMachine;
		jumpTimeCounter = JUMPTIME;
		startAttackingTimer = STARTATTACKTIME;
		startAttacking = false;
		changeStates = false;
		firstJumpSF = true;
		
		jumpCounter = 0;
		timer = 0;
		
		prevState = "Hor";
    }
	
	public void UpdateState()
	{		
		if(!startAttacking)
		{
			startAttackingTimer -= Time.deltaTime;
			
			if(startAttackingTimer <= 0)
			{
				startAttackingTimer = STARTATTACKTIME;
				startAttacking = true;
			}
		}
		
		else 
		{
			if(state.bossScript.gravityOn && jumpCounter <= MAXJUMPCOUNTER)
			{
				state.bossScript.rb.AddForce(-Vector3.up * state.bossScript.GRAVITY);
			}
			else if(changeStates)
			{
				if(firstJumpSF){
					firstJumpSF = false;
					state.bossScript.globalMusic.playSpikeKingJumpSound();
				}
				
				timer += Time.deltaTime;
				if(timer >= MAXTIME){
					timer = 0.0f;
					changeStates = false;
					firstJumpSF = true;
					
					if(state.bossScript.healthbarSlider.value < state.bossScript.MAXHEALTH * .75 && state.bossScript.healthbarSlider.value > state.bossScript.MAXHEALTH / 2 + 1)
					{
						ToSpikesHorAttackState();
					}
					else if(state.bossScript.healthbarSlider.value < state.bossScript.MAXHEALTH / 2 + 1 && prevState == "Hor")
					{
						prevState = "Vert";
						ToSpikesVertAttackState();
					}
					else if(state.bossScript.healthbarSlider.value < state.bossScript.MAXHEALTH / 2 + 1 && prevState == "Vert")
					{
						prevState = "Hor";
						ToSpikesHorAttackState();
					}
				}
				else 
				{
					Vector3 pos = state.bossScript.transform.position;
					pos.y += CHANGEY;
					state.bossScript.transform.position = pos;
				}
					
			}
			else if(jumpCounter <= MAXJUMPCOUNTER)
			{
				state.bossScript.rb.velocity = Vector3.zero;
				jumpTimeCounter -= Time.deltaTime;
				if(jumpTimeCounter <= 0)
					jump();
			}
			else 
			{ 
				state.bossScript.rb.velocity = Vector3.zero;
				
				if(state.bossScript.weakSpotTimer == state.bossScript.WEAKSPOTTIME)
				{
					state.bossScript.weakSpotSeen = true;
				}
				else if (!state.bossScript.weakSpotSeen)
				{
					state.bossScript.gravityOn = false;
					state.bossScript.resetWeakSpot();
					jumpCounter = 0;
					startAttacking = false;
					
					if(state.bossScript.healthbarSlider.value < state.bossScript.MAXHEALTH * .75)
					{
						changeStates = true;
					}
						/*if(state.bossScript.healthbarSlider.value < state.bossScript.MAXHEALTH * .75 && state.bossScript.healthbarSlider.value > state.bossScript.MAXHEALTH / 2 + 1)
					{
						changeStates = true;
					}
					
					//Something like this should work
					else if(state.bossScript.healthbarSlider.value < state.bossScript.MAXHEALTH / 2 + 1 && prevState == "Hor")
					{
						prevState = "Vert";
						ToSpikesVertAttackState();
					}
					else if(state.bossScript.healthbarSlider.value < state.bossScript.MAXHEALTH / 2 + 1 && prevState == "Vert")
					{
						prevState = "Hor";
						ToSpikesHorAttackState();
					}*/
				}
				
			}
		}
	}

    public void ToJumpAttackState()
	{
		Debug.Log ("Cant change to same state");
	}

    public void ToSpikesHorAttackState()
	{
		state.bossScript.bossObject.transform.localEulerAngles = new Vector3(0,0,0);
		state.currentState = state.horState;
	}

    public void ToSpikesVertAttackState()
	{
		state.bossScript.bossObject.transform.localEulerAngles = new Vector3(0,0,0);
		state.currentState = state.vertState;
	}
	
	public void jump()
	{
		jumpCounter++;
		jumpTimeCounter = JUMPTIME;
		state.bossScript.rb.AddForce(Vector3.up * JUMPFORCE);
		state.bossScript.globalMusic.playSpikeKingJumpSound();
		
		if(!state.bossScript.isRight){
			state.bossScript.rb.AddForce(Vector3.right * SPEED);
			state.bossScript.bossObject.transform.localEulerAngles = new Vector3(0,270,0);
		} else {
			state.bossScript.rb.AddForce(-Vector3.right * SPEED);
			state.bossScript.bossObject.transform.localEulerAngles = new Vector3(0,90,0);
		}
		
		state.bossScript.gravityOn = true;
	}
}
