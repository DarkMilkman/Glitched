using UnityEngine;
using System.Collections;
using XInputDotNetPure;

public class RestartLevel : MonoBehaviour {
	
	public Transform cursor;
	public GameObject manager;

	void Start(){
		cursor.position = new Vector3(-1.4f, -0.65f, -1.19f);
		//GameObject.Find("Player").GetComponent<PlayerControllerLv1>().stopRumble();
	}
	
	void FixedUpdate(){
		updateCursor();
		selectOption();
	}
	
	void updateCursor(){
		if(manager.GetComponent<MenuMovement>().optionsNum == 0){
			cursor.position = new Vector3(-1.4f, -0.65f, -1.19f);
		} else if(manager.GetComponent<MenuMovement>().optionsNum == 1){
			cursor.position = new Vector3(-1.2f, -1.4f, -1.19f);
		} 
	}
	
	void selectOption(){
		
		if(manager.GetComponent<MenuMovement>().select){
			if(manager.GetComponent<MenuMovement>().optionsNum == 0){
				ChangeScene();
			} else if(manager.GetComponent<MenuMovement>().optionsNum == 1){
				MainMenu();
			}
		}
	}
	
	public void ChangeScene()
	{
		Application.LoadLevel(GameObject.Find("_Globals").GetComponent<GlobalsScript>().lastLevelPlayed);
	}
	public void MainMenu()
	{
		GameObject.Find("_Globals").GetComponent<GlobalsScript>().resetGobalsForNextLevel();
		GameObject.Find("_Globals").GetComponent<GlobalsScript>().lastLevelPlayed = 1;
		Application.LoadLevel("MainMenu");
	}
	
}
