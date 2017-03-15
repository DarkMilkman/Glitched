using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class BossScript : MonoBehaviour {

	private Vector3 newPos;
	public bool isRight;
	public Slider healthbarSlider;
	public GameObject bossObject;
	public Rigidbody rb;
	
	public GameObject popUpZipLeft;
	public GameObject popUpZipRight;
	public GameObject popUpExeLeft;
	public GameObject popUpExeRight;
	
	public GameObject popUpPostMenu;
	public Rigidbody spikePrefab;
	public Rigidbody spikeHitboxPrefab;
	public GameObject player;
	
	public Rigidbody weakSpot;
	public float weakSpotTimer;
	public float WEAKSPOTTIME = 3.0f;
	public bool weakSpotSeen;
	private float popUPExeRTime;
	private float popUPExeLTime;
	private float popUPZipRTime;
	private float popUPZipLTime;
	public float MAXPOPUPTEXTTIME = 3.0f;
	
	public bool popUpTextActive;
	public bool popUpZipL;
	public bool popUpZipR;
	public bool popUpExeL;
	public bool popUpExeR;
	
	private Vector3 weakSpotPos = new Vector3(0.0f, 0.0f, -0.3f);
	
	public float GRAVITY;
	public bool gravityOn;
	
	public float lerpValue = 5.0f;
	
	public float MAXHEALTH = 200;
	private const int HORSPIKESPEED = 325;
	private const int VERTSPIKESPEED = 150;
	
	public GlobalMusicScript globalMusic;
	
	// Use this for initialization
	void Start () {
		isRight = true;
		gravityOn = true;
		
		popUpExeR = false;
		popUpExeL = false;
		popUpZipR = false;
		popUpZipL = false;
		
		weakSpotTimer = WEAKSPOTTIME;
		
		rb = GetComponent<Rigidbody>();

		popUpTextActive = false;
		
		popUPExeRTime = popUPExeLTime = popUPZipRTime = popUPZipLTime = MAXPOPUPTEXTTIME;
		
		//popUpText.GetComponent<CanvasGroup>().alpha = 0.0f;
		popUpZipLeft.GetComponent<CanvasGroup>().alpha = 0.0f;
        popUpZipRight.GetComponent<CanvasGroup>().alpha = 0.0f;
		popUpExeRight.GetComponent<CanvasGroup>().alpha = 0.0f;
		popUpExeLeft.GetComponent<CanvasGroup>().alpha = 0.0f;
		popUpPostMenu.GetComponent<CanvasGroup>().alpha = 0.0f;
		
		rb.transform.position = new Vector3(4.0f, -3.88f, 0.0f);
		weakSpot.transform.localPosition = Vector3.zero;
		
		globalMusic = GameObject.Find("GlobalSoundEffects").GetComponent<GlobalMusicScript>();
	}
		
	public void takeDamage(float num){
		
		healthbarSlider.value -= num;
		
		//print(healthbarSlider.value);
		
		if(healthbarSlider.value <= 0)
		{
			popUpPostMenu.GetComponent<CanvasGroup>().alpha = 1.0f;
			popUpPostMenu.GetComponent<PostBossMenu>().isDisplayed = true;
			Destroy(bossObject);
		}
	}
	
	void Update(){
			
		if(weakSpotSeen)
			updateWeakSpot();
		else
		{
			weakSpot.transform.localPosition = Vector3.zero;
		}
		
		if(popUpExeR)
			popUpExeRightTimer();
		
		else if(popUpExeL)
			popUpExeLeftTimer();
		
		else if(popUpZipR)
			popUpZipRightTimer();
		
		else if(popUpZipL)
			popUpZipLeftTimer();
		
	}
	
	private void popUpExeRightTimer(){
		
		popUPExeRTime -= Time.deltaTime;
		if(popUPExeRTime >= 0){
			popUpExeRight.GetComponent<CanvasGroup>().alpha = 1.0f;
		}else{
			popUpExeRight.GetComponent<CanvasGroup>().alpha = 0.0f;
			popUPExeRTime = MAXPOPUPTEXTTIME;
			popUpExeR = false;
		}
	}
	
	private void popUpExeLeftTimer(){
		
		popUPExeLTime -= Time.deltaTime;
		if(popUPExeLTime >= 0){
			popUpExeLeft.GetComponent<CanvasGroup>().alpha = 1.0f;
		}else{
			popUpExeLeft.GetComponent<CanvasGroup>().alpha = 0.0f;
			popUPExeLTime = MAXPOPUPTEXTTIME;
			popUpExeL = false;
		}
	}
	
	private void popUpZipLeftTimer(){
		
		popUPZipLTime -= Time.deltaTime;
		if(popUPZipLTime >= 0){
			popUpZipLeft.GetComponent<CanvasGroup>().alpha = 1.0f;
		}else{
			popUpZipLeft.GetComponent<CanvasGroup>().alpha = 0.0f;
			popUPZipLTime = MAXPOPUPTEXTTIME;
			popUpZipL = false;
		}
	}
	
	private void popUpZipRightTimer(){
		
		popUPZipRTime -= Time.deltaTime;
		if(popUPZipRTime >= 0){
			popUpZipRight.GetComponent<CanvasGroup>().alpha = 1.0f;
		}else{
			popUpZipRight.GetComponent<CanvasGroup>().alpha = 0.0f;
			popUPZipRTime = MAXPOPUPTEXTTIME;
			popUpZipR = false;
		}
	}
	
	private void updateWeakSpot(){
		
		//if(!isRight)
		if(player.transform.position.x > transform.position.x){
				weakSpot.transform.localPosition = weakSpotPos;
				transform.localEulerAngles = new Vector3(0,270,0);
		} else {
				weakSpot.transform.localPosition = weakSpotPos;
				transform.localEulerAngles = new Vector3(0,90,0);
		}
		
		weakSpotTimer -= Time.deltaTime;
		
		if(weakSpotTimer <= 0)
		{
			weakSpotSeen = false;
		}
	}
	
	public void instantiateSpike(Vector3 pos, string dir, Quaternion rot)
	{
		Rigidbody clone;
		clone = Instantiate(spikePrefab, pos, rot) as Rigidbody;
		if(dir == "Right")
			clone.AddForce(-Vector3.right * HORSPIKESPEED);
		else if(dir == "Left")
			clone.AddForce(Vector3.right * HORSPIKESPEED);
		
		else if(dir == "RightV")
			clone.AddForce(-Vector3.right * VERTSPIKESPEED);
		else if(dir == "LeftV")
			clone.AddForce(Vector3.right * VERTSPIKESPEED);
	}
	
	public void instantiateSpikeHitBox(Vector3 pos, string dir, Quaternion rot)
	{
		Rigidbody clone;
		clone = Instantiate(spikeHitboxPrefab, pos, rot) as Rigidbody;
		if(dir == "Right")
			clone.AddForce(-Vector3.right * HORSPIKESPEED);
		else if(dir == "Left")
			clone.AddForce(Vector3.right * HORSPIKESPEED);
		
		else if(dir == "RightV")
			clone.AddForce(-Vector3.right * VERTSPIKESPEED);
		else if(dir == "LeftV")
			clone.AddForce(Vector3.right * VERTSPIKESPEED);
	}
	
	public void resetWeakSpot(){
		weakSpot.transform.localPosition = Vector3.zero;
		weakSpotTimer = WEAKSPOTTIME;
	}
}
