using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MovementRockBehavior : MovementBehavior
{
	[SerializeField] private float _movementDistance = 2f;
	private Collider _collider;
	private Vector3 _moveDirection;
	private float _distanceSoFar;
	private bool _isMoving;

	// Get the collider from this gameobject
	protected override void Awake()
	{
		base.Awake();

		_collider = GetComponent<Collider>();
	}

	// Handle grid movement
	protected override void HandleMovement()
	{
		CalculateMoveDirection();
		Move();
	}

	// Helper functions
	private void CalculateMoveDirection()
	{
		// Null check
		if (_rigidbody == null) return;
		if (_collider == null) return;
		if (_isMoving) return;
		if (DesiredMovementDirection.sqrMagnitude <= float.Epsilon) return;

		// Shift direction to the closest xz-axis direction (force straight movement)
		//  i.e. (-0.3, 0, 0.7) -> (0, 0, 1)
		if (Mathf.Abs(DesiredMovementDirection.x) >= Mathf.Abs(DesiredMovementDirection.z))
		{
			_moveDirection.x = DesiredMovementDirection.x;
		}
		else
		{
			_moveDirection.z = DesiredMovementDirection.z;
		}
		_isMoving = true;
		_moveDirection.Normalize();

		// Don't move if this object will be obstructed by the world
		RaycastHit[] hits = _rigidbody.SweepTestAll(_moveDirection, _movementDistance, QueryTriggerInteraction.Collide);

		//Foreach hit:
		// 1. Get the max/min bounds of this object's collider
		// 2. Get the max/min bounds of the hitted collider
		// 3. Depending on the _moveDirection, check if X/Z are close to equal
		// 4. Always check if Y are close to equal
		foreach (RaycastHit hit in hits)
		{
			Vector3 min1 = _collider.bounds.min;
			Vector3 max1 = _collider.bounds.max;
			Vector3 min2 = hit.collider.bounds.min;
			Vector3 max2 = hit.collider.bounds.max;

			if (_moveDirection.x == 0f) // moving on Z
			{
				if (!(min1.x >= max2.x || max1.x <= min2.x))
				{
					if (!(min1.y >= max2.y || max1.y <= min2.y))
					{
						StopMoving();
					}
				}
			}
			else if (_moveDirection.z == 0f) // moving on X
			{
				if (!(min1.z >= max2.z || max1.z <= min2.z))
				{
					if (!(min1.y >= max2.y || max1.y <= min2.y))
					{
						StopMoving();
					}
				}
			}
		}
	}
	private void Move()
	{
		if (_rigidbody == null) return;

		if (_isMoving)
		{
			// Stop moving if the move distance has been reached
			if (_distanceSoFar >= _movementDistance)
			{
				StopMoving();
				return;
			}

			// Calculate the travel distance for this update
			float travelDistance = _movementSpeed * Time.deltaTime;
			_distanceSoFar += travelDistance;

			// If desired distance is reached, clamp travel distance
			if (_distanceSoFar >= _movementDistance)
			{
				travelDistance -= _distanceSoFar - _movementDistance;
			}

			// Move this object in the move direction
			_rigidbody.transform.position += _moveDirection * travelDistance;
		}
	}
	private void StopMoving()
	{
		_moveDirection = Vector3.zero;
		_isMoving = false;
		_distanceSoFar = 0f;
		DesiredMovementDirection = Vector3.zero;
	}
}
