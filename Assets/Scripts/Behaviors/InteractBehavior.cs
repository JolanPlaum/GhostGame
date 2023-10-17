using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractBehavior : MonoBehaviour
{
	private const string INTERACT_TAG = "Interact";
	private GameObject _interactableObject = null;
	private InteractableBehavior _interactableBehavior = null;

	// Check if this object is entering/exiting an interactable object
	private void OnTriggerEnter(Collider other)
	{
		if (other.tag != INTERACT_TAG) return;

		_interactableObject = other.gameObject;

		SetInteractableBehavior();
	}
	private void OnTriggerExit(Collider other)
	{
		if (other.gameObject != _interactableObject) return;

		_interactableObject = null;

		SetInteractableBehavior();
	}

	// Interact function
	public void Interact()
	{
		if (_interactableBehavior == null) return;

		// 1. Face the object you're interacting with
		// 2. Interact with the object
		// (3.) Make player immovable for a period of time

		_interactableBehavior.Execute();
	}

	// Wrap interactable object switching in a function
	private void SetInteractableBehavior()
	{
		// If we enter more than 1 trigger, make sure to deselect the previous one
		if (_interactableBehavior) _interactableBehavior.IsSelected = false;

		// Set the interactable behavior
		if (_interactableObject)
			_interactableBehavior = _interactableObject.GetComponent<InteractableBehavior>();
		else _interactableBehavior = null;

		// Let the new behavior know it's been selected (mainly for player feedback)
		if (_interactableBehavior) _interactableBehavior.IsSelected = true;
	}
}
