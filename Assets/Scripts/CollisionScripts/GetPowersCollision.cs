using UnityEngine;
using System.Collections;

public class GetPowersCollision : MonoBehaviour {

	public GameObject popUpText;
	public GameObject thisCollider;
	// Use this for initialization
	void Start () {
	
	}
	
	void OnTriggerEnter(Collider collider)
	{
		GameObject obj = collider.gameObject;
		
		if (obj.tag == "Player")
		{
			popUpText.GetComponent<PopUpGUI>().showText = true;
			obj.transform.parent.GetComponent<PlayerPowers>().canUsePowers = true;
			obj.transform.parent.GetComponent<SwitchingUI>().switchUI();
			Destroy(thisCollider);
			//SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
		}
	}
}
