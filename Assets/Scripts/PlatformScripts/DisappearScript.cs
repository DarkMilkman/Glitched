using UnityEngine;
using System.Collections;

public class DisappearScript : MonoBehaviour {

	public GameObject platform;
	public GameObject platform2;
	
	public bool start;
	public bool startDisappear;
	
	private bool isDisappeared;
	private float isDiappearedCounter;
	private const float MAXDISAPPEARCOUNTER = 3.0f;
	
	public float startTime = 1.0f;
	public float disappearAmount = 0.1f;
	public float disappearTimeStep = 0.5f;
	private float timer;
	private float alpha;
	
	private GlobalMusicScript music;
	
	// Use this for initialization
	void Start () {
		start = false;
		startDisappear = false;
		isDisappeared = false;
		isDiappearedCounter = 0.0f;
		timer = 0.0f;
		alpha = 1.0f;
		
		music = GameObject.Find("GlobalSoundEffects").GetComponent<GlobalMusicScript>();
	}
	
	// Update is called once per frame
	void Update () {
		if(start){
			timer += Time.deltaTime;
			if(timer >= startTime){
				startDisappear = true;
				start = false;
				
				timer = 0.0f;
			}
		} 
		if(startDisappear){
			timer += Time.deltaTime;
			if(timer >= disappearTimeStep){
				if(alpha == 1.0f)
					music.playVanishingPlatformSound();
				
				//print("alphaChange");
				alpha -= disappearAmount;
				timer = 0.0f;
				
				Color color = platform.GetComponent<Renderer>().material.color;
				color.a = alpha;
				platform.GetComponent<Renderer>().material.color = color;
				

				if(alpha <= 0){
					isDisappeared = true;
					//platform.SetActive(false);
					platform2.SetActive(false);
				}
			}
		}
		
		if(isDisappeared){
			isDiappearedCounter += Time.deltaTime;
			if(isDiappearedCounter >= MAXDISAPPEARCOUNTER){
				
				//platform.SetActive(true);
				platform2.SetActive(true);
				
				start = false;
				startDisappear = false;
				timer = 0.0f;
				
				alpha = 1.0f;
				
				isDisappeared = false;
				isDiappearedCounter = 0.0f;
				
				Color color = platform.GetComponent<Renderer>().material.color;
				color.a = alpha;
				platform.GetComponent<Renderer>().material.color = color;
			}
		}
	}
}
