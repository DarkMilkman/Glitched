using UnityEngine;
using System.Collections;

public class MainMenuButtons : MonoBehaviour {

	public Transform cursor;
	public GameObject manager;
	
	void Start(){
		
	}
	
	void FixedUpdate(){
		updateCursor();
		selectOption();
	}
	
	void selectOption(){
		
		if(manager.GetComponent<MenuMovement>().select){
			
			if(manager.GetComponent<MenuMovement>().optionsNum == 0 && cursor != null){
				
			cursor = null;
			
			print("StartGAme");
			StartGame();
			} 
		}
	}
	
	void updateCursor(){
		if(cursor != null){
			if(manager.GetComponent<MenuMovement>().optionsNum == 0){
				cursor.position = new Vector3(-1.551f, 1.339f, 0.15f);
			} else if(manager.GetComponent<MenuMovement>().optionsNum == 1){
				cursor.position = new Vector3(-0.612f, 0.267f, 0.15f);
			} else if(manager.GetComponent<MenuMovement>().optionsNum == 2){
				cursor.position = new Vector3(0.555f, -1.03f, 0.15f);
			}
		}
	}
	
	public void StartGame()
	{
		//int lastLevel = (int) GameObject.Find("_Globals").GetComponent("GlobalsScript").lastLevelPlayed;
		Application.LoadLevel("Level1");
	}
}
