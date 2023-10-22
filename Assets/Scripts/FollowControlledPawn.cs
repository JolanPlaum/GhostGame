using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowControlledPawn : MonoBehaviour
{
	private PlayerController _controller;

	// Get the controller from this gameobject
	private void Awake()
	{
		_controller = GetComponent<PlayerController>();
	}

	// Set the position after all movement has been done
	private void LateUpdate()
	{
		if (_controller == null) return;

		transform.position = _controller.Pawn.transform.position;
	}
}
