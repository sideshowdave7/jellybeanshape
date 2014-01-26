using UnityEngine;
using System.Collections;

public class ParentNode : MonoBehaviour {

	public ShapeType _nodeType;
	public int _initialChildCount;
	public GameObject _shape;

	// Use this for initialization
	void Start () {
		for (int i = 0; i < _initialChildCount; i++)
		{
			GameObject go = (GameObject)Instantiate(_shape);
			go.GetComponent<ShapeBehavior>()._shapeType = _nodeType;
			go.GetComponent<ShapeBehavior>().Setup(_nodeType);
			go.transform.parent = this.transform;
			go.GetComponent<ShapeBehavior>()._target = this.gameObject;


			go.transform.position = new Vector2(this.transform.position.x + Random.Range(-3,3), this.transform.position.y + Random.Range(-3,3));

		}
	}
	
	// Update is called once per frame
	void Update () {
	
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

	public void UpdateChildren(ShapeType s, GameObject target)
	{
		for (int i = 0; i < ChildCount(); i++)
		{
			this.transform.GetChild(i).GetComponent<ShapeBehavior>().UpdateShape(s, target, this);
		}
	}
}
