using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableBehavior : MonoBehaviour
{
	protected PlayerController _playerTarget;
	protected bool _isPlayerInFacinity;

	private bool _isSelected;
	public bool IsSelected
	{
		get { return _isSelected; }
		set { _isSelected = value; }
	}

	// Find the player at the start of the game
	protected virtual void Start()
	{
		_playerTarget = FindObjectOfType<PlayerController>();
	}

	// Check if player object is entering/exiting this object
	private void OnTriggerEnter(Collider other)
	{
		if (_playerTarget == null) return;
		if (other.gameObject != _playerTarget.Pawn.gameObject) return;

		_isPlayerInFacinity = true;
	}
	private void OnTriggerExit(Collider other)
	{
		if (_playerTarget == null) return;
		if (other.gameObject != _playerTarget.Pawn.gameObject) return;

		_isPlayerInFacinity = false;
	}

	// Public interface
	public virtual void Execute(GameObject other)
	{

	}
}
