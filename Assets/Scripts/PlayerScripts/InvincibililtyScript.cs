using UnityEngine;
using System.Collections;

public class InvincibililtyScript : MonoBehaviour {

	public bool justHit;
	private float invinibilityCounter;
	private const int MAXINVINCIBILITY = 1;
	
	private const float DEFAULTSHINY = 0.61f;
	
	private Renderer rend;
	
	private bool changeColor;
	
	// Use this for initialization
	void Start () {
		
		justHit = false;
		invinibilityCounter = 0.0f;
		
		rend = GetComponent<Renderer>();
        rend.material.shader = Shader.Find("Specular");
	}
	
	// Update is called once per frame
	void Update () {
		
		if(justHit){
			
			invinibilityCounter += Time.deltaTime;
			
			//print("Invincibile");
			float shiny = rend.material.GetFloat("_Shininess");
			changeColor = !changeColor;
			
			Color color = rend.GetComponent<Renderer>().material.color;
				
			if(shiny != 1.0f || changeColor)
			{
				shiny = 1.0f;
				
				color = new Vector4(1.0f, 1.0f, 1.0f, 1.0f);
				rend.GetComponent<Renderer>().material.color = color;
			}
			else
			{
				shiny = 0.0f;
				
				color = new Vector4(0.0f, 0.0f, 0.0f, 1.0f);
				rend.GetComponent<Renderer>().material.color = color;
			}
			
			    rend.material.SetFloat( "_Shininess", shiny);
				

			
			
				
			if(invinibilityCounter >= MAXINVINCIBILITY){
				justHit = false;
				invinibilityCounter = 0.0f;
				
				rend.material.SetFloat( "_Shininess", DEFAULTSHINY);
			
				//print("Not invinibility");
			}
		}
	}
}
