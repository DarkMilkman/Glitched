using UnityEngine;
using System.Collections;

public class PlayerPhysics : MonoBehaviour {

	public bool isGrounded;
	public bool justLanded;
	 
	public float xMove;
	public int xSpeed;
	
	public bool gravityOn;
	
	private float jumpFloatTime;
	private const float MAXJUMPFLOATTIME = 0.08f;
	private float jumpTime;
	private const float MAXJUMPTIME = 0.3f;
	
	private float jumpIncrease;
	private const float MAXJUMPINC = 7.5f;
	private float jumpHeight;
	private const float MAXY = 4.24f;
	private const float DecreaseValue = 3.0f;
	private const float STARTINGJUMPHEIGHT = 0.5f;

	private const float GRAVITY = 45.0f;
	public float lerpValue = 5.0f;
	private float groundBufferLimit = 0.5f;
	private float isGroundedCounter;
	private const float MAX_GROUNDED_COUNTER = 0.7f;
	private bool isSomewhatGrounded;
	
	public bool ifOnMovingPlatform;
	public bool isPlatformMovingUp;
	public bool platformDirectionRight;
	public float platformSpeed;
	
	private bool firstUpdate;
	
	private Vector3 gunOffset;
	private Vector3 prevPos;
	
	private int firstUpdateCounter;
	
	public Rigidbody rb;
	public Rigidbody phaseCollider;
	public GameObject gun;
	private GameObject groundCheck;
	public LayerMask ground;
	
	//scripts
	private PlayerInput input;
	private PlayerPowers powers;
	private PlayerHUD _HUD;
	private GlobalsScript globals;
	private WallJumping wallJumping;
	private AnimatorScript anim;
	
	private GlobalMusicScript globalMusic;
	private ChangeVolumeGlitchMusic changeMusic;
	
	// Use this for initialization
	void Start () {
		
		isGrounded = false;
		justLanded = true;
		gravityOn = true;
		firstUpdate = false;
		
		gunOffset = new Vector3(0.0f, 0.75f, 0.50f);//.02
		xSpeed = 5;
		xMove = 0.0f;
		
		jumpHeight = STARTINGJUMPHEIGHT;
		jumpIncrease = MAXJUMPINC;
		jumpFloatTime = MAXJUMPFLOATTIME;
		jumpTime = MAXJUMPTIME;
		
		firstUpdateCounter = 0;
		
		input = GetComponent<PlayerInput>();
		powers = GetComponent<PlayerPowers>();
		wallJumping = GetComponent<WallJumping>();
		_HUD = GetComponent<PlayerHUD>();
		anim = GetComponent<AnimatorScript>();
		
		globals = GameObject.Find("_Globals").GetComponent<GlobalsScript>();
		globalMusic = GameObject.Find("GlobalSoundEffects").GetComponent<GlobalMusicScript>();
		changeMusic = GameObject.Find("GlobalMusicChanger").GetComponent<ChangeVolumeGlitchMusic>();

		rb = GetComponent<Rigidbody>();
		rb.angularVelocity = Vector3.zero; 
		rb.freezeRotation = true;
	}
	
	// Update is called once per frame
	void Update () {
		checkGround();
		checkPlayerRotation();
		if(firstUpdateCounter == 0) startPlayerPos();
	}
	
	void FixedUpdate(){
		movePlayer();
		groundCheckLimit();
	}
	
	void LateUpdate () {
		updatePlayerPos();
	}
	
	void startPlayerPos(){
		if(globals.lastCheckpointPos != Vector3.zero){
			transform.position = globals.lastCheckpointPos;
			//print(rb.position);
		}
		
		firstUpdateCounter++;
	}
	
	void groundCheckLimit(){
		if(isSomewhatGrounded)
			isGroundedCounter += 0.2f;
	}
	
	void checkPlayerRotation(){
		if(input.isFacingRight){
			transform.localEulerAngles = new Vector3(0.0f, 90.0f, 0.0f);
			phaseCollider.transform.localPosition = new Vector3(0.0f, 0.7f, 3.0f);
		} else {
			transform.localEulerAngles = new Vector3(0.0f, 270.0f, 0.0f);
			phaseCollider.transform.localPosition = new Vector3(0.0f, 0.7f, 3.0f);
		}
	}
	
	void checkGround(){
		Vector3 adjustPos = transform.position + new Vector3(0.0f, 0.5f, 0.0f); 
		bool hit = Physics.Raycast (adjustPos, -Vector3.up, groundBufferLimit, ground);
		Debug.DrawRay(adjustPos,-Vector3.up*groundBufferLimit,Color.black);	
		
		if (hit){
			if(!justLanded)
				globalMusic.playLandSound();
			
			justLanded = true;
			
			input.isWallJump = false;
			
			powers.isUsingPowers = false;
			isSomewhatGrounded = false;
			isGroundedCounter = 0.0f;
			
			isGrounded = true;
		}else{
			isSomewhatGrounded = true;
		}
		
		if(isGroundedCounter >= MAX_GROUNDED_COUNTER){
			isGrounded = false;
			isSomewhatGrounded = false;
		}
	}
	
	void updatePlayerPos(){
		if(!isGrounded) {
			ifOnMovingPlatform = false;
			isPlatformMovingUp = false;
		}
		
		if(ifOnMovingPlatform && !isPlatformMovingUp){
			Vector3 newPos = rb.transform.position;
			if(platformDirectionRight && !input.isMovingRight){
				newPos.x -= platformSpeed;//*1.2f;
			} else if(!platformDirectionRight && !input.isMovingLeft){
				newPos.x += platformSpeed;//*1.2f;
			} 
			//traveling in the opposite direction;
			if(platformDirectionRight && input.isMovingRight){
				newPos.x -= 3.5f * platformSpeed;
			} else if(!platformDirectionRight && input.isMovingLeft){
				newPos.x += 3.5f * platformSpeed;
			}
			
			//traveling the same way
			if(platformDirectionRight && input.isMovingRight){
				newPos.x += platformSpeed * 2.0f;
			} else if(!platformDirectionRight && input.isMovingLeft){
				newPos.x -= platformSpeed * 2.0f;
			}
			
			Vector3 newPos2 = Vector3.Lerp(transform.position, newPos, lerpValue * Time.deltaTime);
			
			rb.transform.position = newPos2;
		}
	}
	
	void movePlayer(){
		
		if(input.glide && _HUD.usePower(powers.GLIDE_VALUE))
		{
			anim.playDefault();
			
			powers.isUsingPowers = true;
			input.isWallJump = false;
			
			if(input.canMove)
				rb.velocity = new Vector3(xMove, 0.0f, 0.0f);
			
			gravityOn = false;
			
			changeMusic.isNormalMusic = false;
			changeMusic.isTeleport = false;
		} 
		else if(input.onWallClinging)
		{
			changeMusic.isNormalMusic = true;
			
			rb.velocity = new Vector3(xMove, 0.0f, 0.0f);
				
		}
		
		else if(input.isWallJump)
		{
			changeMusic.isNormalMusic = true;
			
			justLanded = false;
			input.glide = false;
			wallJumping.isJumped = true;
			
			if(wallJumping.wallIsRight){
				rb.velocity = new Vector3(-1.5f * xSpeed, rb.velocity.y, 0.0f);
			}
			else if(!wallJumping.wallIsRight){
				rb.velocity = new Vector3(1.5f * xSpeed, rb.velocity.y, 0.0f);
			}
		
			if(gravityOn)
				rb.AddForce (-Vector3.up * GRAVITY);
		}
		else 
		{
			changeMusic.isNormalMusic = true;
			
			input.glide = false;
			rb.velocity = new Vector3(xMove, rb.velocity.y, 0.0f);
			if(gravityOn)
				rb.AddForce (-Vector3.up * GRAVITY);
		}
		
		if (input.jump) {
			
			justLanded = false;
			
			if(jumpHeight >= MAXY && jumpTime <= 0)
			{	
				jumpFloatTime -= Time.deltaTime;
				
				rb.velocity = new Vector3(xMove, 0.0f, 0.0f);
				
				gravityOn = false;
				
				if(jumpFloatTime <= 0)
				{
					//print("End Float");
					input.jump = false;
				}
			} 
			else if (jumpHeight < MAXY)
			{
				isGrounded = false;
				isSomewhatGrounded = false;
				
				rb.velocity += new Vector3(0,jumpHeight,0);
				jumpIncrease = jumpIncrease / DecreaseValue;
				jumpHeight += jumpIncrease;
				
				if(jumpHeight <= 0)
				{
					jumpHeight = 0.0f;
				}
				
				//print(jumpHeight);
			}
			else
			{
				jumpTime -= Time.deltaTime;
			}
			
		} 
		else if(!input.jump)
		{
			gravityOn = true;
			
			jumpHeight = STARTINGJUMPHEIGHT;
			jumpIncrease = MAXJUMPINC;
			jumpFloatTime = MAXJUMPFLOATTIME;
			jumpTime = MAXJUMPTIME;
		}
		
		if(_HUD.knockBack){
			
			Vector3 newPos = transform.position;
			
			if(!firstUpdate){
				if(_HUD.frontHit)
					newPos.x -= 2.0f;
				else if(_HUD.backHit)
					newPos.x += 2.0f;
				
				//print(_HUD.frontHit);
				//print(_HUD.backHit);
				_HUD.frontHit = false;
				_HUD.backHit = false;	
			}
				
			Vector3 newPos2 = Vector3.Lerp(transform.position, newPos, 5 * Time.deltaTime);
				
			transform.position = newPos2;
			
			firstUpdate = true;
			
			if(gravityOn)
				rb.AddForce (-Vector3.up * GRAVITY);
			
			if(newPos2 == newPos){
				firstUpdate = false;
				_HUD.knockBack = false;
			}
		}
		
		Vector3 pos = transform.position;
		pos.z = 0.0f;
		transform.position = pos;
		
		gun.transform.localPosition = gunOffset;
		
		
	}
}
