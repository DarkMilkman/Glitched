using UnityEngine;
using System.Collections;

public class WallJumping : MonoBehaviour {

	
	private float wallBufferLimit = 0.25f;
	private const int MINLEVEL = 4;
	public bool onWall;
	
	private float isWallJumpCounter;
	
	private const float MAXTIME = 0.01f;
	public string lastWallJump;
	public bool isJumped;
	public bool wallIsRight;
	
	public LayerMask ground;
	
	private GlobalsScript globals;
	private PlayerInput input;	
	

	// Use this for initialization
	void Start () {
	
		isWallJumpCounter = 0.0f;
		
		onWall = false;
		wallIsRight = false;
		isJumped = false;
		
		lastWallJump = "";
		
		input = GetComponent<PlayerInput>();
		globals = GameObject.Find("_Globals").GetComponent<GlobalsScript>();
	}
	
	// Update is called once per frame
	void Update () {
		
		if(globals.lastLevelPlayed >= MINLEVEL && !isJumped)
			checkWalls();
		
		if(isJumped)
			counterJumped();
	}
	
	void counterJumped(){
		isWallJumpCounter += Time.deltaTime;
		
		//print(isWallJumpCounter);
		if(isWallJumpCounter >= MAXTIME){
			isWallJumpCounter = 0;
			
			//print("can cling");
			isJumped = false;
		}
	}
	//
	void checkWalls(){
		Vector3 adjustPos = transform.position + new Vector3(0.0f, 0.5f, 0.0f);
		bool hit = false;
		bool hit2 = false;
		
		//if(!isJumped){
			hit = Physics.Raycast (adjustPos, Vector3.right, wallBufferLimit, ground);
			Debug.DrawRay(adjustPos,Vector3.right*wallBufferLimit,Color.black);	
			
			hit2 = Physics.Raycast (adjustPos, -Vector3.right, wallBufferLimit, ground);
			Debug.DrawRay(adjustPos,-Vector3.right*wallBufferLimit,Color.black);
		//}		
		
		if(hit || hit2){
			if(hit){
				//print("hit1");
				wallIsRight = true;
			}
			else if(hit2){
				//print("hit2");
				wallIsRight = false;
			}
			
			//print("collide hit on wall");
			input.isWallJump = false;
			onWall = true;
			//isJumped = true;
		} else {
			onWall = false;
			isJumped = false;
		}	
	}
}
