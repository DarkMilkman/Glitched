using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class DesScript : MonoBehaviour {

	public Slider healthbarSlider;
	public GameObject bossObject;
	
	public GameObject dom;
	public GameObject pawn;
	public GameObject bulb;
	
	public GameObject dieBombPrefab;
	public GameObject explosionPrefab;
	public GameObject explosionClone;
	
	public Vector3 DOMSTARTPOS = new Vector3(0.0f, 0.0f, 0.0f);
	public Vector3 PAWNSTARTPOS = new Vector3(0.06f, 0.46f, 0.0f);
	public Vector3 BULBSTARTPOS = new Vector3(-0.38f, -0.53f, 0.0f);
	
	public Vector3 explosionPos;
	
	public Vector3 BULBSTARTANGLE = new Vector3(0, 0, 19);
	public Vector3 DOMSTARTANGLE = new Vector3(354, 31, 331);
	
	public float STARTING_Y_POS = -3.5f;
	
	public GlobalMusicScript globalMusic;
	
	public bool canTakeDamage;
	public bool normalSize;
	public int phaseNum;
	public string attackPhase;
	
	public GameObject pattern1;
	public GameObject pattern2;
	public GameObject pattern3;
	public GameObject pattern4;
	
	public GameObject player;
	
	public GameObject transitionScreen;
	
	public bool isRight;
	
	public bool explosion;
	
	private const int MAXHEALTH = 400;
	
	public float PHASE1FLOATTIME = 5.0f;
	public float PHASE2FLOATTIME = 3.5f;
	public float PHASE3FLOATTIME = 2.0f;
	
	private Vector3 bombAngle = new Vector3(1.0f, 0.75f, 0.0f);
	private const int BOMBFORCE = 475;
	
	// Use this for initialization
	void Start () {
		
		normalSize = true;
		canTakeDamage = false;
		explosion = false;
		
		phaseNum = 0;
		
		dom.transform.localPosition = DOMSTARTPOS;
		pawn.transform.localPosition = PAWNSTARTPOS;
		bulb.transform.localPosition = BULBSTARTPOS;
		//bulb.transform.localEulerAngles = BULBSTARTANGLE;
		
		pattern1.active = false;
		pattern2.active = false;
		pattern3.active = false;
		pattern4.active = false;
		
		globalMusic = GameObject.Find("GlobalSoundEffects").GetComponent<GlobalMusicScript>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	public void instantiateExplosion(Vector3 pos)
	{
		Quaternion rot = Quaternion.Euler(0,0,0);
		explosionClone = Instantiate(explosionPrefab, pos, rot) as GameObject;
	}
	
	public void instantiateBomb(Vector3 pos, string dir, Quaternion rot)
	{
		GameObject clone;
		clone = Instantiate(dieBombPrefab, pos, rot) as GameObject;
		
		if(dir == "Right")
		{
			bombAngle.x = -1.0f;
			clone.GetComponent<Rigidbody>().AddForce(bombAngle * BOMBFORCE);
		}
		else if(dir == "Left")
		{
			bombAngle.x = 1.0f;
			clone.GetComponent<Rigidbody>().AddForce(bombAngle * BOMBFORCE);
		}
	}
	
	public void DestroyExplosionClone()
	{
		if(explosionClone != null)
			Destroy(explosionClone);
	}
	
	public void takeDamage(float num){
		
		if(canTakeDamage){
			globalMusic.playHitSound();
			healthbarSlider.value -= num;
		} else {
			globalMusic.playPingSound();
		}
		
		if(healthbarSlider.value >= (MAXHEALTH * 0.6) + 1)
			phaseNum = 0;
		else if(healthbarSlider.value >= (MAXHEALTH * 0.3) + 1)
			phaseNum = 1;
		else 
			phaseNum = 2;
		
		if(healthbarSlider.value <= 0)
		{
			pattern1.active = false;
			pattern2.active = false;
			pattern3.active = false;
			pattern4.active = false;
		
			Destroy(bossObject);
		}
	}
}
