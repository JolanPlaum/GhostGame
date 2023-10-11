using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class MovementBehavior : MonoBehaviour
{
    [SerializeField] protected float _movementSpeed = 10.0f;
	[SerializeField] protected float _rotationSpeed = 90.0f;
	protected Rigidbody _rigidbody;

	private Vector3 _desiredMovementDirection = Vector3.zero;
	public Vector3 DesiredMovementDirection
	{
		get { return _desiredMovementDirection; }
		set { _desiredMovementDirection = value.normalized; }
	}

	private Vector3 _desiredRotationDirection = Vector3.zero;
	public Vector3 DesiredRotationDirection
	{
		get { return _desiredRotationDirection; }
		set { _desiredRotationDirection = value.normalized; }
	}

	// Get the rigidbody from this gameobject
	protected virtual void Awake()
	{
		_rigidbody = GetComponent<Rigidbody>();
	}

	// Movement is fixed, rotation happens once per frame
    protected virtual void FixedUpdate()
    {
		HandleMovement();
    }
	protected virtual void Update()
	{
		HandleRotation();
	}

	protected virtual void HandleMovement()
	{
		Vector3 movement = _desiredMovementDirection * _movementSpeed;
		movement.y = _rigidbody.velocity.y;
		_rigidbody.velocity = movement;
	}
	protected virtual void HandleRotation()
	{
		float targetAngle = Vector3.Angle(_desiredRotationDirection, transform.forward);

		if (Vector3.Cross(_desiredRotationDirection, transform.forward).y > 0)
		{
			targetAngle *= -1;
		}

		float rotationAngle = Mathf.Clamp(targetAngle, -_rotationSpeed, _rotationSpeed);

		transform.Rotate(0.0f, rotationAngle * Time.deltaTime, 0.0f);
	}
}
