using UnityEngine;
using System.Collections;
using XInputDotNetPure;
using System.Collections.Generic;

public class PlayerInput : MonoBehaviour {

	public bool isFiring;
	public bool isMovingLeft;
	public bool isMovingRight;
	public bool isFacingRight;
	
	public bool phase;
	public bool glide;
	public bool jump;

	public bool canMove;
	private float canMoveCounter;
	private float MAXCANMOVE = 0.5f;
	
	private const int MINLEVEL = 4;
	public bool isWallJump;
	public bool onWallClinging;
	public int firstClingUpdate;
	
	private bool differentShootingButton; 
	
	private float rumbleCounter = 0.0f;
	private const float MAX_RUMBLE = 0.25f;
	private bool isRumble;
	
	private bool debugOptions;
	private bool startingKanimiCode;
	public bool kanimiCodeActivated;
	private List<string> kanimiCodeInput = new List<string>();
	private string[] kanimiCode = {"Up", "Up", "Down", "Down", "Left", "Right", "Left", "Right", "B", "A", "Start"};
	
	bool playerIndexSet = false;
    PlayerIndex playerIndex;
    GamePadState state;
    GamePadState prevState;
	
	private PlayerPhysics physics;
	private PlayerPowers powers;
	private WallJumping wallJumping;
	private GlobalsScript globals;
	private GlobalMusicScript globalMusic;
	private AnimatorScript anim;
		
	// Use this for initialization
	void Start () {
		canMove = true;
		phase = false;
		glide = false;
		jump = false;
		isWallJump = false;
		debugOptions = true;
		startingKanimiCode = false;
		differentShootingButton = false;
		firstClingUpdate = 0;
		canMoveCounter = 0.0f;
		
		stopRumble();
		
		physics = GetComponent<PlayerPhysics>();
		powers = GetComponent<PlayerPowers>();
		wallJumping = GetComponent<WallJumping>();
		anim = GetComponent<AnimatorScript>();
		globals = GameObject.Find("_Globals").GetComponent<GlobalsScript>();
		globalMusic = GameObject.Find("GlobalSoundEffects").GetComponent<GlobalMusicScript>();
		
		globals.playerInputEnabled = true;
	}
	
	// Update is called once per frame
	void Update () {
	
		if(globals.playerInputEnabled && canMove)
			checkInputController();
		
		if(isRumble)
			vibrateController();
		
		if(!canMove){
			canMoveCounter += Time.deltaTime;
			
			if(canMoveCounter >= MAXCANMOVE){
				canMoveCounter = 0.0f;
				canMove = true;
			}
		}
	}
	
	void checkInputController(){
		
		// Find a PlayerIndex, for a single player game
        // Will find the first controller that is connected and use it
        if (!playerIndexSet || !prevState.IsConnected)
        {
            for (int i = 0; i < 4; ++i)
            {
                PlayerIndex testPlayerIndex = (PlayerIndex)i;
                GamePadState testState = GamePad.GetState(testPlayerIndex);
                if (testState.IsConnected)
                {
                    //Debug.Log(string.Format("GamePad found {0}", testPlayerIndex));
                    playerIndex = testPlayerIndex;
                    playerIndexSet = true;
                }
            }
        }

        prevState = state;
        state = GamePad.GetState(playerIndex);
		
		physics.xMove = state.ThumbSticks.Left.X * physics.xSpeed;
		//right
		if (state.ThumbSticks.Left.X > 0)
        {
            isFacingRight = true;
			isMovingLeft = false;
			isMovingRight = true;
			
			if(physics.isGrounded)
				anim.playRun();
			
        }
		//left
		else if (state.ThumbSticks.Left.X < 0)
        {
			isFacingRight = false;
			isMovingLeft = true;
			isMovingRight = false;
			
			if(physics.isGrounded)
				anim.playRun();
			
        }else
		{
			isMovingLeft = false;
			isMovingRight = false;
			
			if(physics.isGrounded)
				anim.playIdle();
		}
		
		if((prevState.Buttons.A == ButtonState.Released && state.Buttons.A == ButtonState.Pressed) && (onWallClinging))
		{
			if(wallJumping.onWall){
				isWallJump = true;
				jump = true;
				wallJumping.onWall = false;
				globalMusic.playJumpSound();
				
			} else{
				isWallJump = false;
			} 
		}
		
		if((state.Buttons.A == ButtonState.Pressed)  && (physics.isGrounded))//(state.Buttons.A == ButtonState.Pressed && physics.isGrounded)
		{
			globalMusic.playJumpSound();
			jump = true;
		}
		else if(prevState.Buttons.A == ButtonState.Pressed && state.Buttons.A == ButtonState.Released) //(state.Buttons.A == ButtonState.Released)
		{
			//isWallJump = false;
			jump = false;
		} 
		
		if(globals.lastLevelPlayed >= MINLEVEL){
			
			if(!wallJumping.onWall){
				firstClingUpdate = 0;
				onWallClinging = false;
			}
			
			if (state.Buttons.LeftShoulder == ButtonState.Pressed  && !physics.isGrounded){//(prevState.Buttons.LeftShoulder == ButtonState.Released && state.Buttons.LeftShoulder == ButtonState.Pressed){
					
					if(wallJumping.onWall){
						//print("on wall");
						onWallClinging = true;
						
						if(firstClingUpdate == 0){
							globalMusic.playLandSound();
						}
						firstClingUpdate++;
					}
			}
			else if (prevState.Buttons.LeftShoulder == ButtonState.Pressed && state.Buttons.LeftShoulder == ButtonState.Released){
					
					onWallClinging = false;
			}
		}
		
		//Powers
		if(!differentShootingButton){
			if (prevState.Buttons.X == ButtonState.Released && state.Buttons.X == ButtonState.Pressed){
				globalMusic.playGunSound();
				anim.playShoot();
				isFiring = true;
			}
			else if (prevState.Buttons.X == ButtonState.Pressed && state.Buttons.X == ButtonState.Released){
				isFiring = false;
			}
			
			if (prevState.Buttons.B == ButtonState.Released && state.Buttons.B == ButtonState.Pressed){
				globalMusic.playGunSound();
				anim.playShoot();
				isFiring = true;
			}
			else if (prevState.Buttons.B == ButtonState.Pressed && state.Buttons.B == ButtonState.Released){
				isFiring = false;
			}
		}
		else if(differentShootingButton){
			if (prevState.Buttons.RightShoulder == ButtonState.Released && state.Buttons.RightShoulder == ButtonState.Pressed){
				globalMusic.playGunSound();
				anim.playShoot();
				isFiring = true;
			}
			else if (prevState.Buttons.RightShoulder == ButtonState.Pressed && state.Buttons.RightShoulder == ButtonState.Released){
				isFiring = false;
			}
		}
		
		if (state.Triggers.Left == 1){
			if(powers.canUsePowers){
				//physics.prePhase();
				powers.isUsingPowers = true;
				if(powers.canPhase)
					phase = true;
			}
		}
		else if (state.Triggers.Left == 0){
			phase = false;
		}

		if (state.Triggers.Right == 1 && !physics.isGrounded){
			if(powers.canUsePowers)
				glide = true;
		}
		if (state.Triggers.Right == 0){
				glide = false;
		}
		
		//Kanimi code
		if (prevState.Buttons.Back == ButtonState.Released && state.Buttons.Back == ButtonState.Pressed){
			debugOptions = !debugOptions;
			startingKanimiCode = !startingKanimiCode;
		}
		
		if(startingKanimiCode){
			if (prevState.Buttons.Start == ButtonState.Released && state.Buttons.Start == ButtonState.Pressed){
				
				kanimiCodeInput.Add("Start");
				if(kanimiCodeInput.Count == kanimiCode.Length){
					for(int i = 0; i < kanimiCodeInput.Count; i++){
						if(kanimiCodeInput[i] != kanimiCodeInput[i]){
							break;
						} 
						if(i == kanimiCodeInput.Count - 1){
							kanimiCodeActivated = true;
							globals.kanimiCodeActivated = kanimiCodeActivated;
							print("Activated Kanimi code");
						}
					}

				}
				kanimiCodeInput.Clear();
			}
	
			else if (prevState.Buttons.A == ButtonState.Released && state.Buttons.A == ButtonState.Pressed  && physics.isGrounded){
				kanimiCodeInput.Add("A");
			}
			else if (prevState.Buttons.B == ButtonState.Released && state.Buttons.B == ButtonState.Pressed  && physics.isGrounded){
				kanimiCodeInput.Add("A");
			}
			else if (prevState.DPad.Up == ButtonState.Released && state.DPad.Up == ButtonState.Pressed) {
				kanimiCodeInput.Add("Up");
			}
			else if (prevState.DPad.Down == ButtonState.Released && state.DPad.Down == ButtonState.Pressed) {
				kanimiCodeInput.Add("Down");
			}
			else if (prevState.DPad.Right == ButtonState.Released && state.DPad.Right == ButtonState.Pressed) {
				kanimiCodeInput.Add("Right");
			}
			else if (prevState.DPad.Left == ButtonState.Released && state.DPad.Left == ButtonState.Pressed) {
				kanimiCodeInput.Add("Left");
			}
			
		}
		
		//Debug
		if(debugOptions){
			if (prevState.DPad.Up == ButtonState.Released && state.DPad.Up == ButtonState.Pressed) {
				globals.resetGobalsForNextLevel();
				Application.LoadLevel(Application.loadedLevel);
			}
			else if (prevState.DPad.Right == ButtonState.Released && state.DPad.Right == ButtonState.Pressed) {
				globals.lastLevelPlayed++;
				globals.resetGobalsForNextLevel();
				Application.LoadLevel(globals.lastLevelPlayed);
			}
			else if (prevState.DPad.Left == ButtonState.Released && state.DPad.Left == ButtonState.Pressed) {
				globals.lastLevelPlayed--;
				globals.resetGobalsForNextLevel();
				Application.LoadLevel(globals.lastLevelPlayed);
			}
			else if (prevState.DPad.Down == ButtonState.Released && state.DPad.Down == ButtonState.Pressed) {
				differentShootingButton = !differentShootingButton;
			}
		}
	}
	
	public void rumble()
    {
        GamePad.SetVibration(PlayerIndex.One, 1.0f, 1.0f);
		isRumble = true;
    }
	
	public void vibrateController(){
		
		rumbleCounter += Time.deltaTime;
		if(rumbleCounter >= MAX_RUMBLE)
			stopRumble();
	}
	
	public void stopRumble()
    {
		//print("stop rumble");
        GamePad.SetVibration(PlayerIndex.One, 0.0f, 0.0f);
		isRumble = false;
		rumbleCounter = 0.0f;
    }
}
