using UnityEngine;
using System.Collections;
using XInputDotNetPure;

public class MenuMovement : MonoBehaviour {

	public bool select = false;
	bool canMove;
	float moveCounter;
	const float MAX_MOVE = 3.0f;
	
	bool playerIndexSet = false;
    PlayerIndex playerIndex;
    GamePadState state;
    GamePadState prevState;
	
	public int MAX_OPTIONS; 
	public int optionsNum;
	
	// Use this for initialization
	void Start () {
		optionsNum = 0;
		moveCounter = 0.0f;
		canMove = true;
	}
	
	void Update(){
		checkInputController();
	}
	
	void FixedUpdate(){
		updateMove();
		//updateCursor();
	}
	
	void updateMove()
	{
		if(!canMove){
			moveCounter += 0.2f;
			
			if(moveCounter >= MAX_MOVE){
				moveCounter = 0.0f;
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
		
        if (prevState.Buttons.A == ButtonState.Released && state.Buttons.A == ButtonState.Pressed)
        {
            select = true;
        } 
		else if (prevState.Buttons.A == ButtonState.Pressed && state.Buttons.A == ButtonState.Released)
        {
            select = false;
        }
		
		if ((prevState.DPad.Up == ButtonState.Released && state.DPad.Up == ButtonState.Pressed) || (state.ThumbSticks.Left.Y > 0 && canMove))
		{
			canMove = false;
			optionsNum--;
			
			if(optionsNum < 0)
				optionsNum = 0;
		} 
		else if ((prevState.DPad.Down == ButtonState.Released && state.DPad.Down == ButtonState.Pressed) || (state.ThumbSticks.Left.Y < 0 && canMove))
		{
			canMove = false;
			optionsNum++;
			
			if(optionsNum > MAX_OPTIONS)
				optionsNum = MAX_OPTIONS;
		}

	}
}
