using UnityEngine;
using System.Collections;

public class ChangeVolumeGlitchMusic : MonoBehaviour {

	public AudioSource normalMusic;
	public AudioSource glitchMusic;
	
	public bool isNormalMusic = true;
	public bool isTeleport;
	
	public float TIMESKIP = 0.5f;
	public float MAXVOLUME = 0.4f;
	
	private const float NORMALPITCH = 1.0f;
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		
		if(isNormalMusic){
			normalMusic.volume = MAXVOLUME;
			glitchMusic.volume = 0.0f;
		} else {
			isTeleport = false;
			normalMusic.volume = 0.0f;
			glitchMusic.volume = MAXVOLUME;
		}
		
		if(isTeleport){
			isNormalMusic = true;
			normalMusic.pitch = normalMusic.pitch + TIMESKIP;
		} else{
			isNormalMusic = true;
			normalMusic.pitch = NORMALPITCH;
		}
	}
}
