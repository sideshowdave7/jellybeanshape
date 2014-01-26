using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerController : MonoBehaviour {

	public ShapeType _currentShape;
	public int _followerCount = 0;
	public List<GameObject> _followers;

	// Use this for initialization
	void Start () {

		_followers = new List<GameObject>();
		AudioManager.Instance.playLoop("MainDrumLoop");
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
		if (Input.GetKeyDown(KeyCode.Mouse0) || Input.GetKeyDown(KeyCode.Mouse1))
		{
			Vector2 mosPos = Input.mousePosition;
			Vector3 worldPos = Camera.main.ScreenToWorldPoint((Vector3)mosPos);
			Vector3 norm = (worldPos - this.transform.position).normalized;
			this.rigidbody2D.AddForce(norm * Globals.Instance.PLAYER_SPEED);
			if(worldPos.x > this.transform.position.x)
			{
				AudioManager.Instance.playClip("boss3");
			}
			if(worldPos.x < this.transform.position.x)
			{
				AudioManager.Instance.playClip("boss1");
			}
			if(worldPos.y < this.transform.position.y)
			{
				AudioManager.Instance.playClip("boss1");
			}
			if(worldPos.y > this.transform.position.y)
			{
				AudioManager.Instance.playClip("boss4");
			}
		}
		if (Input.GetKey(KeyCode.Mouse0) || Input.GetKey(KeyCode.Mouse1))
		{
			Vector2 mosPos = Input.mousePosition;
			Vector3 worldPos = Camera.main.ScreenToWorldPoint((Vector3)mosPos);
			Vector3 norm = (worldPos - this.transform.position).normalized;
			this.rigidbody2D.AddForce(norm * Globals.Instance.PLAYER_SPEED);

		}

		//Check for other shapes in your vicinity
		GameObject[] shapes = GameObject.FindGameObjectsWithTag("Shape");
		if(_followerCount == 0)
		{
			this.GetComponent<ShapeBehavior>().UpdateShape(_currentShape, this.gameObject, null);
		}
		foreach (GameObject g in shapes)
		{
			if (Vector2.Distance(g.transform.position, this.transform.position) <= Globals.Instance.INFLUENCE_RADIUS && g.GetComponent<ShapeBehavior>()._target != this.gameObject)
			{
				ShapeBehavior s = g.GetComponent<ShapeBehavior>();
				if (s.transform.parent != null)
				{
					if(s.transform.parent.GetComponent<ParentNode>().ChildCount() <= _followerCount + 1)
					{
						s.transform.parent.GetComponent<ParentNode>().UpdateChildren(_currentShape, this.gameObject);
					}
					else
					{
						this.GetComponent<ShapeBehavior>().UpdateShape(s._shapeType, this.gameObject, s.transform.parent.GetComponent<ParentNode>());
						for (int i = 0; i < _followerCount; i++)
						{
							_followers[i].GetComponent<ShapeBehavior>().UpdateShape(s._shapeType, this.gameObject,null);
						}
					}
				}
			}
		}

	
	}


}
