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

	public void UpdateShape(ShapeType goalShape, GameObject go)
	{
		//Transform to goal shape
		bool changed = false;
		if(goalShape != ShapeType.Circle)
		{
			float currentGoalWeight = _meshRenderer.GetBlendShapeWeight(ShapeToInt(goalShape));
			if(currentGoalWeight < 100)
			{
				_meshRenderer.SetBlendShapeWeight(ShapeToInt(goalShape), ++currentGoalWeight);
			}
			else
			{
				changed = true;
			}
		}
		for (int i = 0; i < 4; i++)
		{
			if (i != ShapeToInt(goalShape))
			{
				float currentWeight = _meshRenderer.GetBlendShapeWeight(i);
				if(currentWeight > 0)
				{
					_meshRenderer.SetBlendShapeWeight(i, --currentWeight);
				}
				else if (changed)
				{
					_target = go;
				}
			}
		}
	}

	private int ShapeToInt(ShapeType s)
	{
		switch (s)
		{
		case ShapeType.Triangle:
			return 0;
		case ShapeType.Star:
			return 1;
		case ShapeType.Hexagon:
			return 2;
		case ShapeType.Square:
			return 3;
		}
		return 0;
	}
}