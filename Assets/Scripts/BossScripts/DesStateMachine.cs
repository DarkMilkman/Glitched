using UnityEngine;
using System.Collections;

public class DesStateMachine : MonoBehaviour {
	
	public DesScript desScript;
	
	[HideInInspector] public IDesState currentState;
	[HideInInspector] public FloatState floatState;
    [HideInInspector] public LightBulbAttackState lightBulbState;
    [HideInInspector] public DieAttackState dieState;
    [HideInInspector] public CreateLevelAttackState createLevelState;
	
	private void Awake()
    {
		desScript = GetComponent<DesScript>();
		
		floatState = new FloatState (this);
        lightBulbState = new LightBulbAttackState (this);
        dieState = new DieAttackState (this);
        createLevelState = new CreateLevelAttackState (this);
    }

    // Use this for initialization
    void Start () 
    {
		//currentState = createLevelState;
        currentState = floatState;
    }
	
	// Update is called once per frame
	void Update () {
		currentState.UpdateState ();
	}
}
