using UnityEngine;
using System.Collections;

public class ForceCameraAspect : MonoBehaviour {

	// Use this for initialization
	void Start () {
		float targetAspect = 21.0f / 12.0f;
		float windowAspect = (float)Screen.width / (float)Screen.height;
		float scaleheight = windowAspect / targetAspect;
		Camera camera = GetComponent<Camera>();
		
		if (scaleheight < 1.0f)
		{  
			Rect rect = camera.rect;

			rect.width = 1.0f;
			rect.height = scaleheight;
			rect.x = 0;
			rect.y = (1.0f - scaleheight) / 2.0f;
			
			camera.rect = rect;
		}
		else // add pillarbox
		{
			float scalewidth = 1.0f / scaleheight;

			Rect rect = camera.rect;

			rect.width = scalewidth;
			rect.height = 1.0f;
			rect.x = (1.0f - scalewidth) / 2.0f;
			rect.y = 0;

			camera.rect = rect;
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
