using UnityEngine;
using System.Collections;
using System.Collections.Generic;
 
public class GlobalsScript : MonoBehaviour {

	public int lastLevelPlayed;
	public Vector3 lastCheckpointPos;
	public bool kanimiCodeActivated;
	public bool playerInputEnabled;
	//private List<GameObject> collectablesToBeDestroyed;
	
	// Use this for initialization
	void Start () {
		lastLevelPlayed = 1;
		playerInputEnabled = true;
		lastCheckpointPos = new Vector3(0.0f, 0.0f, 0.0f);
		//collectablesToBeDestroyed = new List<GameObject>();
	}
	
	void Awake() {
		DontDestroyOnLoad(transform.gameObject);
	}
	
	/*public void addToCollectableList(GameObject aObject){
		collectablesToBeDestroyed.Add(aObject);
		print("added collectable");
	}
	
	public void deleteCollectablesOnLoad(){
		//List<GameObject> temp = new List<GameObject>(collectablesToBeDestroyed);
		
		print("in delete");
		for(int i = collectablesToBeDestroyed.Count - 1; i > 0; i--){
			Destroy(collectablesToBeDestroyed[i]);
			print("Deleted the collectable");
		}
		//collectablesToBeDestroyed.Clear();
		//collectablesToBeDestroyed = new List<GameObject>(temp);
		
		//temp.Clear();
	}*/
	
	public void resetCheckpointPos(){
		lastCheckpointPos = new Vector3(0.0f, 0.0f, 0.0f);
	}
	
	public void resetGobalsForNextLevel(){
		resetCheckpointPos();
		//collectablesToBeDestroyed.Clear();
	}
	
	// Update is called once per frame
	void Update () {
		//print(lastLevelPlayed);
	}
}
