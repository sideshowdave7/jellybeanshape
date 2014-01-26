using UnityEngine;
using System.Collections;

public class BackgroundColors : MonoBehaviour 
{
	
	public GameObject Circle;
	public int Quantity;
	private GameObject[] circles;
	private Vector2[] circleVectors;
	
	void Start () 
	{
		circles = new GameObject[Quantity];
		circleVectors = new Vector2[Quantity];
		
		for ( int i = 0; i < Quantity ; i ++ )
		{
			circles[i] = (GameObject)Instantiate(Circle);
			circles[i].transform.position = new Vector3 (Random.Range (-60f,60f),Random.Range (-30f,30f),10);
			
			//Set Scales
			float Scale = Random.Range(0.5f, 6f);
			circles[i].transform.localScale = new Vector3 (Scale,Scale,1);
			
			//Set Colors
			float value = Random.Range(0.2f,0.3f);
			Color color = new Color (value,value,value, Random.Range(0.1f,0.2f) );
			circles[i].renderer.material.SetColor ("_TintColor", color);
			
			//set move vectors
			circleVectors[i] = new Vector2 (Random.Range(-0.003f,0.003f), Random.Range(-0.003f,0.003f));
		}
	}
	
	void Update () 
	{
		for ( int i = 0; i < Quantity ; i ++ )
		{
			circles[i].transform.Translate(circleVectors[i]);
			if ( Mathf.Abs (circles[i].transform.position.x ) > 60 || Mathf.Abs (circles[i].transform.position.y ) > 30 )
			{
				circles[i].transform.position = new Vector3 ( circles[i].transform.position.x * -1, circles[i].transform.position.y * -1, 0);
			}
			
			circleVectors[i].x += Random.Range(-0.0001f, 0.0001f );
			circleVectors[i].y += Random.Range(-0.0001f, 0.0001f );
			
			
		}
	}
}
