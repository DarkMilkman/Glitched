using UnityEngine;
using System.Collections;

public class VoxelAnimationController : MonoBehaviour {

	private Animator anim;
	
	public AnimationClip idleAnim;
	public AnimationClip jumpAnim;
	public AnimationClip landAnim;
	public AnimationClip runAnim;
	
	//int jumpHash = Animator.StringToHash("Jump");
	//int runStateHash = Animator.StringToHash("Base Layer.Run");
	
	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator>();
	}
	
	
	// Update is called once per frame
	void Update () {
		
		//anim.Play("Idle");
		//float move = Input.GetAxis("Vertical");
		//anim.SetFloat("Speed", move); 
		//if(Input.GetKeyDown(KeyCode.Space))// && stateInfo.nameHash == runStateHash)
        //{
           // anim.SetTrigger (jumpHash);
        //}
	}
}
