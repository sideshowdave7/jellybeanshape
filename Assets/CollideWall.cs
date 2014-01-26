using UnityEngine;
using System.Collections;

public class CollideWall : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {	


	}
	void OnCollisionEnter2D(Collision2D Box){
		if(Box.collider.gameObject.name == "MainCharacter")
		{
			AudioManager.Instance.playClip("triangle1");

		}
		if(Box.collider.gameObject.tag == "Shape")
		{
			AudioManager.Instance.playClip("triangle2");
			
		}


	}
}
