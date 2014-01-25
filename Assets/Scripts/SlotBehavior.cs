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
			var comp = (ShapeBehavior)shape.gameObject.GetComponent<ShapeBehavior>();
			var dist = Vector2.Distance(comp.transform.position,transform.position);

			if(dist < .05f) {
				comp.rigidbody2D.velocity = Vector2.zero;
				comp.locked = true;
			} else if (comp._shapeType == shapeType && dist < Globals.Instance.SLOT_TO_SHAPE_DISTANCE && !comp.locked) {
				Vector2 dir = comp.transform.position - transform.position;
				comp.rigidbody2D.AddForce(-dir/(Globals.Instance.SLOT_TO_SHAPE_DISTANCE - dist));
			}


		}

	}
}
