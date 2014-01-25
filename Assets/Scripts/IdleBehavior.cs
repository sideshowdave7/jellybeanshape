using UnityEngine;
using System.Collections;

public class IdleBehavior : MonoBehaviour {
	
	private bool _idleActive = true;
	private Vector2 _centerPosition;
	public float _radius = 0.8f;
	public float _power = 1;
	private Vector2 _curve;
	private Vector2 _movementVector = Vector2.zero;

	void Start ()
	{
		_movementVector = new Vector2(Random.Range (-1f * _power,1f * _power),Random.Range (-1f * _power,1f * _power));
		this.rigidbody2D.AddForce(_movementVector*10);
	}
	
	void Update () 
	{
		if (_idleActive)
		{
			//check if the game object is beyond the radius, if so pick a new movement vector
			if ( Mathf.Sqrt ( Vector2.Distance(transform.position,_centerPosition ) ) > _radius )
			{
				Return_To_Center();
				this.rigidbody2D.AddForce(_movementVector);
			}
			else
			{
				this.rigidbody2D.AddForce(_curve);
			}
		}
	}

	private void Return_To_Center ()
	{
		_movementVector = new Vector2(_centerPosition.x-transform.position.x, _centerPosition.y-transform.position.y);
		float value = Random.Range (-0.1f * _power,0.1f * _power);
		_curve = new Vector2(Random.Range (-0.2f * _power,0.2f * _power),Random.Range (-0.2f * _power,0.2f * _power));
	}

	public void Set_Idle ( bool input )
	{
		_idleActive = input;

		//set the center position for the new idle movment
		if ( _idleActive ) 
		{
			_centerPosition.x = this.transform.position.x; 
			_centerPosition.y = this.transform.position.y; 
			_movementVector = new Vector2(Random.Range (-1f * _power,1f * _power),Random.Range (-1f * _power,1f * _power));
			this.rigidbody2D.AddForce(_movementVector * 10 * _power);
		}
	}
}
