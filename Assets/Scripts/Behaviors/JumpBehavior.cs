using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpBehavior : MonoBehaviour
{
	[SerializeField] protected float _jumpForce = 10.0f;
	protected Rigidbody _rigidbody;
	protected bool _jumpedLastFrame;

	private bool _isOnGround = false;
	public bool IsOnGround
	{
		get { return _isOnGround; }
	}

	// Get the rigidbody from this gameobject
	protected virtual void Awake()
	{
		_rigidbody = GetComponent<Rigidbody>();
	}

	// Check if the rigidbody is on the ground
	private void FixedUpdate()
	{
		if (_jumpedLastFrame)
		{
			_isOnGround = false;
			_jumpedLastFrame = false;
			return;
		}

		float offset = 0.1f;

		_isOnGround = Physics.Raycast(transform.position + Vector3.up * offset, Vector3.down, offset * 2) && _rigidbody.velocity.y <= 0;
	}

	// Jump function
	public virtual void Jump()
	{
		if (_isOnGround)
		{
			_rigidbody.AddForce(Vector3.up * _jumpForce, ForceMode.Impulse);
			_jumpedLastFrame = true;
			_isOnGround = false;
		}
	}
}
