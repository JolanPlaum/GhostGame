using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashBehavior : MonoBehaviour
{
    [SerializeField] private float _dashSpeed = 50f;
    [SerializeField] private float _dashDistance = 5f;
	[SerializeField] private TrailRenderer _trail;
	private Rigidbody _rigidbody;
	private float _distanceSoFar;

	private bool _isDashing = false;
	public bool IsDashing
	{
		get { return _isDashing; }
		private set
		{
			_isDashing = value;

			if (_isDashing) _distanceSoFar = 0;
			if (_rigidbody) _rigidbody.useGravity = !_isDashing;
			if (_trail) _trail.enabled = _isDashing;
		}
	}

	// Get the rigidbody from this gameobject
	protected virtual void Awake()
	{
		_rigidbody = GetComponent<Rigidbody>();
		if (_trail) _trail.enabled = false;
	}

	// Dashing is movement so do it in fixed update
	private void FixedUpdate()
	{
		if (IsDashing)
		{
			// Calculate the travel distance for this update
			float travelDistance = _dashSpeed * Time.deltaTime;
			_distanceSoFar += travelDistance;

			// If desired distance is reached, stop dashing and clamp travel distance
			if (_distanceSoFar >= _dashDistance)
			{
				IsDashing = false;
				travelDistance -= _distanceSoFar - _dashDistance;
			}

			// Check if a wall will be hit
			if (Physics.Raycast(transform.position + transform.up, transform.forward, out RaycastHit hit, travelDistance))
			{
				IsDashing = false;
				travelDistance = hit.distance * 0.9f;
			}

			// Move this object in the direction it's facing
			transform.position += transform.forward * travelDistance;
		}
	}

	// Dash function
	public bool Dash()
    {
		if (_rigidbody == null || IsDashing) return false;

		// Not sure why but sometimes unity leaves the velocity at -2.384186E-06
		if (Mathf.Abs(_rigidbody.velocity.y) <= 0.00001)
		{
			IsDashing = true;
			return true;
		}

		return false;
    }
}
