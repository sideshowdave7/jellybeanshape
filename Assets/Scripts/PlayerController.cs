using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(Input.GetButton("Horizontal"))
		{
			this.rigidbody2D.AddForce(new Vector2(Input.GetAxis("Horizontal") * Globals.Instance.PLAYER_SPEED,0));
		}

		if(Input.GetButton("Vertical"))
		{
			this.rigidbody2D.AddForce(new Vector2(0, Input.GetAxis("Vertical") * Globals.Instance.PLAYER_SPEED));
		}

		if (Input.GetKey(KeyCode.Mouse0) || Input.GetKey(KeyCode.Mouse1))
		{
			Vector2 mosPos = Input.mousePosition;
			Debug.Log("Mouse pos: " + mosPos.ToString());
		}
	
	}
}
