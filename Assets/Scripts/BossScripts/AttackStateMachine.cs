using UnityEngine;
using System.Collections;

public class AttackStateMachine : MonoBehaviour {
	
	public BossScript bossScript;

    [HideInInspector] public IBossState currentState;
    [HideInInspector] public JumpAttackState jumpState;
    [HideInInspector] public SpikesHorAttackState horState;
    [HideInInspector] public SpikesVertAttackState vertState;
	
	private void Awake()
    {
		bossScript = GetComponent<BossScript>();
		
        jumpState = new JumpAttackState (this);
        horState = new SpikesHorAttackState (this);
        vertState = new SpikesVertAttackState (this);
    }

    // Use this for initialization
    void Start () 
    {
        currentState = jumpState;
    }
	
	// Update is called once per frame
	void Update () {
		currentState.UpdateState ();
	}
}
