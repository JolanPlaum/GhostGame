using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MoveableBehavior : InteractableBehavior
{
	[SerializeField] private float _moveSpeed = 5f;
	[SerializeField] private float _moveDistance = 2f;
	private Rigidbody _rigidbody = null;
	private Vector3 _moveDirection = Vector3.zero;
	private bool _isMoving = false;
	private float _distanceSoFar;

	// Find the rigidbody in the parent class
	private void Awake()
	{
		_rigidbody = GetComponentInParent<Rigidbody>();
	}

	// Movement inside of fixed update
	private void Update()
	{
		if (_isMoving)
		{
			// Stop moving if the move distance has been reached
			if (_distanceSoFar >= _moveDistance)
			{
				StopMoving();
				return;
			}

			// Calculate the travel distance for this update
			float travelDistance = _moveSpeed * Time.deltaTime;
			_distanceSoFar += travelDistance;

			// If desired distance is reached, clamp travel distance
			if (_distanceSoFar >= _moveDistance)
			{
				travelDistance -= _distanceSoFar - _moveDistance;
			}

			// Move this object in the move direction
			_rigidbody.transform.position += _moveDirection * travelDistance;
		}
	}

	// Move behavior
	public override void Execute(GameObject other)
	{
		// Null check
		if (other == null) return;
		if (_rigidbody == null) return;
		if (_isMoving) return;

		// Calculate the direction from which the other object is pushing this one
		Vector3 actualDirection = transform.position - other.transform.position;

		// Shift direction to the closest xz-axis direction (force straight movement)
		//  i.e. (-0.3, 0, 0.7) -> (0, 0, 1)
		if (Mathf.Abs(actualDirection.x) >= Mathf.Abs(actualDirection.z))
		{
			_moveDirection.x = actualDirection.x;
		}
		else
		{
			_moveDirection.z = actualDirection.z;
		}
		_isMoving = true;
		_moveDirection.Normalize();

		// Don't move if this object will be obstructed by the world
		RaycastHit[] hits = _rigidbody.SweepTestAll(_moveDirection, _moveDistance);
		if (hits.Length > 0)
		{
			StopMoving();
		}
	}

	// Helper function
	private void StopMoving()
	{
		_moveDirection = Vector3.zero;
		_isMoving = false;
		_distanceSoFar = 0f;
	}
}
