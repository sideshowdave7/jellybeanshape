﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerController : MonoBehaviour {

	public ShapeType _currentShape;
	public int _followerCount = 0;
	public List<GameObject> _followers;
	public bool _beingConverted = false;

	public float _timeIdle = 0f;

	// Use this for initialization
	void Start () {

		_followers = new List<GameObject>();
		AudioManager.Instance.playLoop("MainDrumLoop");
	}
	
	// Update is called once per frame
	void FixedUpdate () 
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

		if(this.gameObject.rigidbody2D.velocity.magnitude <= 0.5f && _followerCount > 0)
		{
			_timeIdle += Time.deltaTime;
			if (_timeIdle > 2.25f)
			{
				_timeIdle = 0f;
				this.GetComponent<ShapeBehavior>().UpdateShape(_currentShape, null);
				foreach(GameObject go in _followers)
				{
					ShapeBehavior sh = go.GetComponent<ShapeBehavior>();
					sh._target = sh._originalParentNode;
					sh.transform.parent = sh._originalParentNode.transform;
				}
			}

		}

		//Check for other shapes in your vicinity
		GameObject[] nodes = GameObject.FindGameObjectsWithTag("Node");
		//bool c = this.gameObject.GetComponent<ShapeBehavior>()._beingConverted;
		if(!_beingConverted)
		{
			this.GetComponent<ShapeBehavior>().UpdateShape(_currentShape, null);
		}
		_beingConverted = false;
		foreach (GameObject g in nodes)
		{
			if (Vector2.Distance(g.transform.position, this.transform.position) <= Globals.Instance.INFLUENCE_RADIUS)
			{
				foreach (GameObject shape in g.GetComponent<ParentNode>().GetChildren())
				{
					if(shape.GetComponent<ShapeBehavior>()._target != this.gameObject)
					{
						g.GetComponent<ParentNode>()._beingConverted=true;
						ShapeBehavior s = shape.GetComponent<ShapeBehavior>();
						if (s.transform.parent != null)
						{
							if(s.transform.parent.GetComponent<ParentNode>().ChildCount() <= _followerCount + 1)
							{
								s.transform.parent.GetComponent<ParentNode>().UpdateChildren(_currentShape, this.gameObject);
							}
							else
							{
								this.GetComponent<ShapeBehavior>().UpdateShape(s._shapeType, s.transform.parent.GetComponent<ParentNode>());
								_beingConverted = true;
								for (int i = 0; i < _followerCount; i++)
								{
									_followers[i].GetComponent<ShapeBehavior>().UpdateShape(s._shapeType, null);
								}
							}
						}
					}
				}
			}
			else
			{
				g.GetComponent<ParentNode>()._beingConverted = false;
			}
		}

	
	}


}
