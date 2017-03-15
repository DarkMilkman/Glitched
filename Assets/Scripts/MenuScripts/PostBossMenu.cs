using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PostBossMenu : MonoBehaviour {

	public Transform cursor;
	public GameObject manager;
	public bool isDisplayed;
	private Vector3 nextLevelPos = new Vector3(-72.0f, -64.2f, 0.0f);
	private Vector3 mainMenuPos = new Vector3(-72.0f, -94.0f, 0.0f);
	
	void Start(){
		isDisplayed = false;
		cursor.transform.localPosition = nextLevelPos;
	}
	
	void FixedUpdate(){
		if(isDisplayed){
			GameObject.Find("_Globals").GetComponent<GlobalsScript>().playerInputEnabled = false;
			updateCursor();
			selectOption();
		}
	}
	
	void selectOption(){
		
		if(manager.GetComponent<MenuMovement>().select){
			if(manager.GetComponent<MenuMovement>().optionsNum == 0){
				NextLevel();
				//print("Next Level");
			} 
			else if(manager.GetComponent<MenuMovement>().optionsNum == 1){
				MainMenu();
				//print("Main Menu");
			}
		}
	}
	
	void updateCursor(){
		if(manager.GetComponent<MenuMovement>().optionsNum == 0){
			cursor.transform.localPosition = nextLevelPos;
		} else if(manager.GetComponent<MenuMovement>().optionsNum == 1){
			cursor.transform.localPosition = mainMenuPos;
		} 
	}
	
	public void NextLevel()
	{
		GameObject.Find("_Globals").GetComponent<GlobalsScript>().resetGobalsForNextLevel();
		GameObject.Find("_Globals").GetComponent<GlobalsScript>().lastLevelPlayed++;
		Application.LoadLevel(GameObject.Find("_Globals").GetComponent<GlobalsScript>().lastLevelPlayed);
	}
	
	public void MainMenu()
	{
		GameObject.Find("_Globals").GetComponent<GlobalsScript>().resetGobalsForNextLevel();
		GameObject.Find("_Globals").GetComponent<GlobalsScript>().lastLevelPlayed = 1;
		Application.LoadLevel("MainMenu");
	}
}