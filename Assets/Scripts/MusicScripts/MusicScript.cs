using UnityEngine;
using System.Collections;
using UnityEngine.Audio;

public class MusicScript : MonoBehaviour {

	public AudioSource audio;
	public AudioClip otherClip; 
	
	IEnumerator Start()
	{
		audio = GetComponent<AudioSource>();
    
        audio.Play();
        yield return new WaitForSeconds(audio.clip.length);
        audio.clip = otherClip;
        audio.Play();
		audio.loop = true;
	}
}
