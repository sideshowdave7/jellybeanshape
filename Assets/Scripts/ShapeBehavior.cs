using UnityEngine;
using System.Collections;

public class ShapeBehavior : MonoBehaviour {


	public GameObject _target;
	public ShapeType _shapeType;
	

	private SkinnedMeshRenderer _meshRenderer;

	public float maxDistance;

	// Use this for initialization
	void Start () 
	{

		_meshRenderer = this.GetComponent<SkinnedMeshRenderer>();
		if(_shapeType != ShapeType.Circle)
		{
			_meshRenderer.SetBlendShapeWeight(ShapeToInt(_shapeType),100);
		}
	}

	public void Setup(ShapeType s)
	{

	}
	
	// Update is called once per frame
	void Update () {
	
		if (_target == null) { //Do nothing

		} else{// if(_shapeType == ShapeType.Circle) { //move to target //_target.shapeType
			Vector3 dir =  _target.transform.position - this.gameObject.transform.position;
			rigidbody2D.AddForce (dir);
		}
	}

	public void UpdateShape(ShapeType goalShape, GameObject go, ParentNode p)
	{
		//Transform to goal shape
		bool changed = false;

		if (_meshRenderer != null)
		{
			if (goalShape != ShapeType.Circle)
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
			else
			{
				changed = true;
			}

		
			for (int i = 0; i < 5; i++)
			{
				if (i != ShapeToInt(goalShape))
				{
					float currentWeight = _meshRenderer.GetBlendShapeWeight(i);
					if(currentWeight > 0)
					{
						_meshRenderer.SetBlendShapeWeight(i, --currentWeight);
					}

				}
			}
			if (changed && InCorrectShape(goalShape))
			{
				//_target = go;
				//this.transform.parent = null;
				
				if(this.gameObject.tag == "Player" && p != null)
				{
					p.SetTargetForChildren(this.gameObject);
					this.gameObject.GetComponent<PlayerController>()._currentShape = goalShape;

				}
				else if(p!= null)
				{
					p.SetTargetForChildren(GameObject.FindGameObjectWithTag("Player"));

				}
			}
			
		}
	}

	/// <summary>
	/// Returns true if the shape is in the correct...shape	/// </summary>
	/// <returns><c>true</c>, if correct shape was ined, <c>false</c> otherwise.</returns>
	/// <param name="goalShape">Goal shape.</param>
	private bool InCorrectShape(ShapeType goalShape)
	{

		if(goalShape == ShapeType.Circle)
		{
			for (int i = 0; i < 4; i++)
			{
				if(_meshRenderer.GetBlendShapeWeight(i) > 0)
					return false;
			}
			return true;
		}
		else
		{
			for (int i = 0; i < 4; i++)
			{
				if(i == ShapeToInt(goalShape))
				{
					if(_meshRenderer.GetBlendShapeWeight(i) != 100)
						return false;
				}
				else
				{
					if (_meshRenderer.GetBlendShapeWeight(i) != 0)
						return false;
				}
			}
			return true;
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
		case ShapeType.Circle:
			return 4;
		}
		return 0;
	}
}