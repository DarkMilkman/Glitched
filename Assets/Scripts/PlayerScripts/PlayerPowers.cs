using UnityEngine;
using System.Collections;

public class PlayerPowers : MonoBehaviour {

	public bool canPhase = true;
	public bool canUsePowers;
	public bool isUsingPowers;
	
	private const float PHASE_NUM = 3.0f;
	public float GLIDE_VEL = 0.5f;
	public int GLIDE_VALUE = 1;
	
	private float phaseCounter = 0.0f;
	private float MAX_PHASE = 1.0f;
	private const float POWER_REGEN = 0.2f;
	private const float POWER_REGEN_FAST = 1.5f;
	
	private PlayerInput input;
	private PlayerHUD _HUD;
	private PlayerPhysics physics; 
	
	private ChangeVolumeGlitchMusic changeMusic;
	
	void Start () {
		//canUsePowers = true;
		canPhase = true;
		isUsingPowers = false;
		
		physics = GetComponent<PlayerPhysics>();
		input = GetComponent<PlayerInput>();
		_HUD = GetComponent<PlayerHUD>();
		
		changeMusic = GameObject.Find("GlobalMusicChanger").GetComponent<ChangeVolumeGlitchMusic>();
	}
	
	
	void FixedUpdate () {
		if(input.phase)
			phasePlayer();
		
		if(!canPhase)
			phaseTimer();
		
		if(!isUsingPowers){
			if(physics.isGrounded)
				_HUD.regenPower(POWER_REGEN_FAST);
			else 
				_HUD.regenPower(POWER_REGEN);
		}
	}
	
	void phaseTimer()
	{
		phaseCounter += Time.deltaTime;
		if(phaseCounter >= MAX_PHASE / 5)
		{
			changeMusic.isTeleport = false;
		}
		if(phaseCounter >= MAX_PHASE)
		{
			phaseCounter = 0.0f;
			canPhase = true;
			
			_HUD.normalPic.GetComponent<CanvasGroup>().alpha = 1.0f;
			_HUD.glitchedPic.GetComponent<CanvasGroup>().alpha = 0.0f;
		}
	}
	
	public void phasePlayer(){

		input.phase = false;
		
		if(canPhase){//} && playerController.GetComponent<PlayerController>().usePower(GLITCH_POWER)){
			
			_HUD.normalPic.GetComponent<CanvasGroup>().alpha = 0.0f;
			_HUD.glitchedPic.GetComponent<CanvasGroup>().alpha = 1.0f;
		
			phaseCounter = 0.0f;
			input.isWallJump = false;
			changeMusic.isTeleport = true;
			
			float newX = transform.position.x;
			float newY = transform.position.y;
			float newZ = transform.position.z;
			
			if(input.isFacingRight)
				newX += PHASE_NUM;
			else 
				newX -= PHASE_NUM;
			
			Vector3 newPos = new Vector3(newX, newY, newZ);	
			
			//Vector3 newPos2 = Vector3.Lerp(transform.position, newPos, 25S * Time.deltaTime);
			
			transform.position = newPos;
			
			canPhase = false;
		}
	}
}
