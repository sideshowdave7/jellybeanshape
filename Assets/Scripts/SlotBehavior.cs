using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SlotBehavior : MonoBehaviour {

	public ShapeType shapeType;

	public bool locked = false;
	private bool _prevLocked = false;
	public bool hasShape = false;


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void FixedUpdate () {
	

		foreach (var shape in GameObject.FindGameObjectsWithTag("Shape")) {


				var comp = (ShapeBehavior)shape.gameObject.GetComponent<ShapeBehavior>();

				if (comp != null) {
				var dist = Vector2.Distance(comp.transform.position,transform.position);

				if(dist < .05f && !comp.locked && GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>()._followers.Contains((comp.gameObject))) {
				
					comp.rigidbody2D.velocity = Vector2.zero;
					comp.locked = true;
					locked = true;

					comp.transform.position = transform.position;
					comp.collider2D.enabled = false;

					if(GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>()._followers.Remove(comp.gameObject))
					{
						GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>()._followerCount--;
					}

				} else if (comp._shapeType == shapeType && dist < Globals.Instance.SLOT_TO_SHAPE_DISTANCE && !comp.tracking && !hasShape) {
					comp._target = this.gameObject;
					comp.tracking = true;
					hasShape = true;
					AudioManager.Instance.playClip("MorphTest2");
				}

				if (comp.tracking && comp._target == this.gameObject) {
					comp.rigidbody2D.velocity = Vector2.zero;
					comp.rigidbody2D.isKinematic = true;
					Vector2 pos = Vector2.MoveTowards(comp.transform.position,transform.position,dist/10f);
					comp.transform.position = new Vector3(pos.x, pos.y, 0f);
				}

				if (locked && !_prevLocked){
					AudioManager.Instance.playLoop("bossBG");
				}

				_prevLocked = locked;

			}
		}
	}
}
