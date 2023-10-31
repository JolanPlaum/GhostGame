using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MoveableBehavior : InteractableBehavior
{
	[SerializeField] private float _moveSpeed = 5f;
	[SerializeField] private float _moveDistance = 2f;
	[SerializeField] private Collider _triggerCollider;
	private Rigidbody _rigidbody = null;
	private Collider _collider = null;
	private Vector3 _moveDirection = Vector3.zero;
	private bool _isMoving = false;
	private float _distanceSoFar;

	// Find the rigidbody in the parent class
	private void Awake()
	{
		_rigidbody = GetComponentInParent<Rigidbody>();
		if (_rigidbody != null )
		{
			_collider = _rigidbody.GetComponent<Collider>();
		}
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
		if (_triggerCollider != null) _triggerCollider.enabled = false;
		RaycastHit[] hits = _rigidbody.SweepTestAll(_moveDirection, _moveDistance, QueryTriggerInteraction.Ignore);
		if (hits.Length > 0)
		{
			//Foreach hit:
			// 1. Get the hit collider
			// 2. Get the max/min bounds of the hit collider
			// 3. Get the max/min bounds of this object's collider
			// 4. Depending on the _moveDirection, check if X/Z are close to equal
			// 5. Always check if Y are close to equal
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
		if (_triggerCollider != null) _triggerCollider.enabled = true;
	}

	// Helper function
	private void StopMoving()
	{
		_moveDirection = Vector3.zero;
		_isMoving = false;
		_distanceSoFar = 0f;
	}
}
