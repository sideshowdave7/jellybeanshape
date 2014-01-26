using UnityEngine;
using System.Collections;

public class ShapeBehavior : MonoBehaviour {


	public GameObject _target;
	public ShapeType _shapeType;
	public bool locked;
	public GameObject _originalParentNode;

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

	public void UpdateShape(ShapeType goalShape, ParentNode p)
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
					float newval = currentGoalWeight + 1f / (p!=null?((float)p.ChildCount()):1f);
					_meshRenderer.SetBlendShapeWeight(ShapeToInt(goalShape), newval);
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
						float newval = currentWeight - 1f / (p!=null?((float)p.ChildCount()):1f);
						_meshRenderer.SetBlendShapeWeight(i, newval);
					}

				}
			}
			if (changed && InCorrectShape(goalShape))
			{
				
				if(this.gameObject.tag == "Player" && p != null)
				{
					p.SetTargetForChildren(this.gameObject);
					this.gameObject.GetComponent<PlayerController>()._currentShape = goalShape;
					this._shapeType = goalShape;
				}
				else if(p!= null)
				{
					p.SetTargetForChildren(GameObject.FindGameObjectWithTag("Player"));
					this._shapeType = goalShape;
				}
				else if(this.gameObject.tag != "Player" && _target == _originalParentNode)
				{
					this.transform.parent = _originalParentNode.transform;
					_originalParentNode.GetComponent<CircleCollider2D>().isTrigger = false;
					if(GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>()._followers.Remove(this.gameObject))
					{
						GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>()._followerCount--;
						_shapeType = goalShape;
					}
						
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
					if(_meshRenderer.GetBlendShapeWeight(i) < 100)
						return false;
				}
				else
				{
					if (_meshRenderer.GetBlendShapeWeight(i) > 0)
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