using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableBehavior : MonoBehaviour
{
	protected GameObject _playerTarget;
	protected bool _isPlayerInFacinity;

	private bool _isSelected;
	public bool IsSelected
	{
		get { return _isSelected; }
		set { _isSelected = value; }
	}

	// Find the player at the start of the game
	private void Start()
	{
		GhostPlayerPawn player = FindObjectOfType<GhostPlayerPawn>();
		if (player) _playerTarget = player.gameObject;
	}

	// Check if player object is entering/exiting this object
	private void OnTriggerEnter(Collider other)
	{
		if (other.gameObject != _playerTarget) return;

		_isPlayerInFacinity = true;
	}
	private void OnTriggerExit(Collider other)
	{
		if (other.gameObject != _playerTarget) return;

		_isPlayerInFacinity = false;
	}

	// Public interface
	public virtual void Execute()
	{

	}
}
