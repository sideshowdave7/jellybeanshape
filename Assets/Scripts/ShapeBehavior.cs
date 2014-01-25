using UnityEngine;
using System.Collections;

public class ShapeBehavior : MonoBehaviour {


	private GameObject _target;
	public ShapeType _shapeType = ShapeType.Circle;


	public float maxDistance;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
		if (_target == null) { //Do nothing

		} else if(_shapeType == ShapeType.Circle) { //move to target //_target.shapeType
				Vector3 dir = Vector3.MoveTowards(rigidbody.position,_target.rigidbody.position,maxDistance);
				rigidbody.AddForce (dir);
		}

	}
}

public enum ShapeType {

	Circle,
	Square,
	Triangle

}
