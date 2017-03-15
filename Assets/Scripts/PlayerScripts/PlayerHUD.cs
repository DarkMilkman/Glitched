using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerHUD : MonoBehaviour {

	public Slider healthbarSlider;
	public Slider powerbarSlider;
	
	public GameObject glitchedPic;
	public GameObject normalPic;
	
	private const int MAX_HEALTH = 100;
	private const int MAX_POWER = 100;
	
	public bool knockBack;
	public bool frontHit;
	public bool backHit;
	private int power;
	private float health;
	private bool isColliding;
	
	private const float DEFAULTSHINY = 0.61f;
	
	//scripts
	private PlayerInput input;
	private InvincibililtyScript invinc;
	private GlobalMusicScript music;
	
	void Start () {
		
		frontHit = false;
		backHit = false;
		
		knockBack = false;
		
		isColliding = false;
		health = MAX_HEALTH;
		
		input = GetComponent<PlayerInput>();
		invinc = GetComponentInChildren<InvincibililtyScript>();
		music = GameObject.Find("GlobalSoundEffects").GetComponent<GlobalMusicScript>();
		
		normalPic.GetComponent<CanvasGroup>().alpha = 1.0f;
		glitchedPic.GetComponent<CanvasGroup>().alpha = 0.0f;
	}
	
	void LateUpdate () {
		isColliding = false;
	}
	
	public void damagedPlayer(float damageNum){
		
		if(!invinc.justHit){
			music.playPlayerHitSound();
			knockBack = true;
			
			if(isColliding) return;
				isColliding = true;
		
			input.canMove = false;
			invinc.justHit = true;
			
			health -= damageNum;
			healthbarSlider.value -= damageNum;
		
			if(health - damageNum + 2 >= 0)
				input.rumble();
		
		}
		
		if(health <= 0){
			Application.LoadLevel("DeathScreen");
		}
	}
	
	public void regainHealth(float healthNum){
		
		if(isColliding) return;
		isColliding = true;
		
		if(health + healthNum >= 100)
		{
			health = 100;
			healthbarSlider.value = 100;
		} else {
			health += healthNum;
			healthbarSlider.value += healthNum;
		}
	}
	
	public void regenPower(float num)
	{
		powerbarSlider.value += num;
		if(powerbarSlider.value >= MAX_POWER)
			powerbarSlider.value = MAX_POWER;
	}
	
	public bool usePower(int num)
	{
		if(powerbarSlider.value - num < 0)
			return false;
		else 
		{
			powerbarSlider.value -= num;
			return true;
		}
	}
}
