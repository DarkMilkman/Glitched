using UnityEngine;
using System.Collections;

public class BombThrowerScript : MonoBehaviour {

	//public GameObject bomb;
	public GameObject enemy;
	
	public bool isRight = true;
	
	private Vector3 newRD = new Vector3(0.5f, 1.0f, 0.0f);
	private Vector3 newLD = new Vector3(-0.5f, 1.0f, 0.0f);
	
	private float timeCounter;
	private const float MAXTIME = 2.5f;
	
	private const int FORCE = 375;
	
	private BombCreation bombCreation;
	private GlobalMusicScript music;
	
	// Use this for initialization
	void Start () {
		timeCounter = 0.0f;
		
		bombCreation = GetComponent<BombCreation>();
		music = GameObject.Find("GlobalSoundEffects").GetComponent<GlobalMusicScript>();
	}
	
	// Update is called once per frame
	void Update () {
		
		if(enemy.GetComponent<Renderer>().isVisible)
		{
			//print("visible");
			timeCounter += Time.deltaTime;
			if(timeCounter >= MAXTIME){
				timeCounter = 0.0f;

				music.playBombThrowSound();
				bombCreation.instaniateBomb(isRight, newRD * FORCE, newLD * FORCE);
			}
		}
		//else
		//{
			//print("not visible");
		//}
	}
}
