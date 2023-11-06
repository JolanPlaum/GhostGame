using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorBehavior : InteractableBehavior
{
	[SerializeField] private GameObject _lockedVisuals;
	[SerializeField] private GameObject _openedVisuals;
	private bool _isOpen = false;

	// Ensure the door visuals are set to locked
	private void Awake()
	{
		if (_lockedVisuals) _lockedVisuals.SetActive(true);
		if (_openedVisuals) _openedVisuals.SetActive(false);
	}

	// Open door behavior
	public override void Execute(GameObject other)
	{
		// Check if the door is already open
		if (_isOpen) return;

		// Check if other object has a matching key
		GrabBehavior behavior = other.GetComponent<GrabBehavior>();
		if (behavior == null) return;
		if (behavior.HeldObject == null) return;
		if (behavior.HeldObject.GetComponent<KeyID>() == null) return;
		Destroy(behavior.HeldObject);

		// Switch the visuals to open
		if (_lockedVisuals) _lockedVisuals.SetActive(false);
		if (_openedVisuals) _openedVisuals.SetActive(true);

		// Disable the door collision
		Collider collider = GetComponent<Collider>();
		if (collider != null) collider.enabled = false;

		// Set the door as open
		_isOpen = true;
	}
	public override bool CanExecute(GameObject other)
	{
		// Check if the door is already open
		if (_isOpen) return false;

		// Check if other object has a matching key
		GrabBehavior behavior = other.GetComponent<GrabBehavior>();
		if (behavior == null) return false;
		if (behavior.HeldObject == null) return false;
		if (behavior.HeldObject.GetComponent<KeyID>() == null) return false;

		return true;
	}
}
