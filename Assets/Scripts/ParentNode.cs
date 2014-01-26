using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ParentNode : MonoBehaviour {

	public ShapeType _nodeType;
	public int _initialChildCount;
	public GameObject _shape;
	public bool _beingConverted;


	// Use this for initialization
	void Start () {
		for (int i = 0; i < _initialChildCount; i++)
		{
			GameObject go = (GameObject)Instantiate(_shape);
			go.GetComponent<ShapeBehavior>()._shapeType = _nodeType;
			go.GetComponent<ShapeBehavior>()._originalParentNode = this.gameObject;
			go.GetComponent<ShapeBehavior>().Setup(_nodeType);
			go.transform.parent = this.transform;
			go.GetComponent<ShapeBehavior>()._target = this.gameObject;


			go.transform.position = new Vector2(this.transform.position.x + Random.Range(-3,3), this.transform.position.y + Random.Range(-3,3));

		}
	}
	
	// Update is called once per frame
	void FixedUpdate () 
	{

		if(!_beingConverted)
		{

			for (int i = 0; i < ChildCount(); i++)
			{
				this.transform.GetChild(i).GetComponent<ShapeBehavior>().UpdateShape(_nodeType, null);

			}
		}
	}

	public void SetTargetForChildren(GameObject t)
	{
		for (int i = 0; i < ChildCount(); i++)
		{
			if (this.transform.GetChild(i).GetComponent<ShapeBehavior>()._target.tag != "Slot"){

				this.transform.GetChild(i).GetComponent<ShapeBehavior>()._target = t;
				t.GetComponent<PlayerController>()._followerCount ++;
				t.GetComponent<PlayerController>()._followers.Add(this.transform.GetChild(i).gameObject);
				this.transform.GetChild(i).transform.parent = null;
			}
		}
		this.GetComponent<CircleCollider2D>().isTrigger = true;
	}

	public int ChildCount()
	{
		return this.transform.childCount;
	}

	public List<GameObject> GetChildren()
	{
		List<GameObject> children = new List<GameObject>();
		foreach (Transform t in this.transform)
		{
			children.Add(t.gameObject);
		}
		return children;
	}

	public void UpdateChildren(ShapeType s, GameObject target)
	{
		for (int i = 0; i < ChildCount(); i++)
		{
			this.transform.GetChild(i).GetComponent<ShapeBehavior>().UpdateShape(s, this);
		}
	}
}
