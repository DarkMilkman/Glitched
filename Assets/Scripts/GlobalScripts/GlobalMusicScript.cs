using UnityEngine;
using System.Collections;

public class GlobalMusicScript : MonoBehaviour {

	public GameObject gameObject;
	
	public AudioSource gunSF;
	public AudioSource jumpSF;
	public AudioSource landSF;
	public AudioSource hitSF;
	public AudioSource pingSF;
	
	public AudioSource healthSF;//done
	public AudioSource checkPointSF;//done
	public AudioSource spikeKingJumpSF;//done
	public AudioSource spikeKingWarningSF;//done
	public AudioSource bombThrowSF;//done
	public AudioSource bombExplodeSF;//done
	public AudioSource vanishingPlatformSF;//done
	public AudioSource switchSF;//done
	public AudioSource playerHitSF;//done
	
	
	public AudioSource lightFlashSF;
	public AudioSource diceThrowSF;
	
	// Use this for initialization
	void Start () {
		//gameObject.GetComponent<AudioSource>().enabled = true;
	}
	
	//pick ups
	public void playHealthSound(){
		healthSF.Play();
	}
	
	public void playCheckPointSound(){
		checkPointSF.Play();
	}
	
	//Des 
	public void playLightFlashSound(){
		lightFlashSF.Play();
	}
	
	public void playDiceThrowSound(){
		diceThrowSF.Play();
	}
	
	//spike king
	public void playSpikeKingJumpSound(){
		spikeKingJumpSF.Play();
	}
	
	public void playSpikeKingWarningSound(){
		spikeKingWarningSF.Play();
	}
	
	//bomb guys
	public void playBombThrowSound(){
		bombThrowSF.Play();
	}
	
	public void playBombExplodeSound(){
		bombExplodeSF.Play();
	}
	
	//interactable things 
	public void playVanishingPlatformSound(){
		vanishingPlatformSF.Play();
	}
	
	public void playSwitchSound(){
		switchSF.Play();
	}
	
	
	//player stuff 
	public void playPlayerHitSound(){
		playerHitSF.Play();
	}
	
	public void playGunSound(){
		gunSF.Play();
	}
	
	public void playJumpSound(){
		jumpSF.Play();
	}
	
	public void playLandSound(){
		landSF.Play();
	}
	
	public void playHitSound(){
		hitSF.Play();
	}
	
	public void playPingSound(){
		pingSF.Play();
	}
	
}
