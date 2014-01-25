using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SlotBehavior : MonoBehaviour {

	public ShapeType shapeType;



	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
		foreach (var shape in GameObject.FindGameObjectsWithTag("Shape")) {
			var comp = shape.gameObject.GetComponent<ShapeBehavior>();
			var dist = Vector2.Distance(comp.rigidbody2D.transform.position,rigidbody2D.transform.position);

			if (comp._shapeType == shapeType && dist < Globals.Instance.SLOT_TO_SHAPE_DISTANCE){
				Vector2 dir = comp.rigidbody2D.transform.position - rigidbody2D.transform.position;
				comp.rigidbody2D.AddForce(dir);
			}
		}

	}
}
