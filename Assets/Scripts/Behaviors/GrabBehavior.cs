using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabBehavior : MonoBehaviour
{
	[SerializeField] private Transform _handPosition;
	private const string GRAB_TAG = "Grab";
	private GameObject _grabbableObject = null;

	private GameObject _heldObject = null;

	// Check if this object is near/far a grabbable object
	private void OnTriggerEnter(Collider other)
	{
		if (other.tag != GRAB_TAG) return;

		_grabbableObject = other.gameObject;
	}
	private void OnTriggerExit(Collider other)
	{
		if (other.gameObject != _grabbableObject) return;

		_grabbableObject = null;
	}

	// Grab function
	public void Grab()
    {
		// Drop the current grabbed object
		DropHeldObject();

		// Grab any nearby grabbable object
		GrabNearbyObject();
    }

	// Helper functions
	private void DropHeldObject()
	{
		if (_heldObject == null) return;

		_heldObject.transform.SetParent(null);
		_heldObject = null;
	}
	private void GrabNearbyObject()
	{
		if (_grabbableObject == null) return;

		_grabbableObject.transform.SetParent(_handPosition);
		_grabbableObject.transform.localPosition = Vector3.zero;
		_grabbableObject.transform.localRotation = Quaternion.identity;

		_heldObject = _grabbableObject;
		_grabbableObject = null;
	}
}
