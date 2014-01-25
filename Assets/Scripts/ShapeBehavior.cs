using UnityEngine;
using System.Collections;

public class ShapeBehavior : MonoBehaviour {


	private GameObject _target;
	public ShapeType _shapeType = ShapeType.Circle;

	private SkinnedMeshRenderer _meshRenderer;

	public float maxDistance;

	// Use this for initialization
	void Start () 
	{
		_meshRenderer = this.GetComponent<SkinnedMeshRenderer>();
		_meshRenderer.SetBlendShapeWeight(2,100);
	}
	
	// Update is called once per frame
	void Update () {
	
		if (_target == null) { //Do nothing

		} else if(_shapeType == ShapeType.Circle) { //move to target //_target.shapeType
			Vector3 dir =  _target.transform.position - transform.position;
			rigidbody2D.AddForce (dir);
		}

	}
}