using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabBehavior : MonoBehaviour
{
	[SerializeField] private Transform _handPosition;
	private const string GRAB_TAG = "Grab";
	private GameObject _grabbableObject = null;
	private bool _isActive = false;

	private GameObject _heldObject = null;
	public GameObject HeldObject
	{
		get { return _heldObject; }
	}

	// Drop any held object if switching to the overworld
	private void Start()
	{
		GameMode mode = FindObjectOfType<GameMode>();
		if (mode != null)
		{
			mode.OnWorldChanged += WorldChanged;
			WorldChanged(mode.IsOverworld);
		}
	}

	// Check if this object is near/far a grabbable object
	private void OnTriggerEnter(Collider other)
	{
		if (other.tag != GRAB_TAG) return;

		SetShowInput(false);
		_grabbableObject = other.gameObject;
		if (_isActive) SetShowInput(true);
	}
	private void OnTriggerExit(Collider other)
	{
		if (other.gameObject != _grabbableObject) return;

		SetShowInput(false);
		_grabbableObject = null;
	}

	// Grab function
	public void Grab()
    {
		// Drop the current grabbed object
		DropHeldObject();

		// Grab any nearby grabbable object
		SetShowInput(false);
		GrabNearbyObject();
    }

	// Helper functions
	private void DropHeldObject()
	{
		if (_heldObject == null) return;

		_heldObject.transform.SetParent(null);
		_heldObject.transform.position = new Vector3(
			Mathf.Round(_heldObject.transform.position.x),
			Mathf.Round(_heldObject.transform.position.y),
			Mathf.Round(_heldObject.transform.position.z));
		_heldObject.transform.localRotation = Quaternion.identity;
		_heldObject = null;
	}
	private void GrabNearbyObject()
	{
		if (_grabbableObject == null) return;

		_grabbableObject.transform.localPosition = _handPosition.position;
		_grabbableObject.transform.localRotation = _handPosition.rotation;
		_grabbableObject.transform.SetParent(_handPosition);

		_heldObject = _grabbableObject;
		_grabbableObject = null;
	}
	private void SetShowInput(bool isShown)
	{
		if (_grabbableObject && _grabbableObject.TryGetComponent<ShowInputFeedback>(out var comp))
		{
			comp.IsShown = isShown;
		}
	}
	private void WorldChanged(bool isOverworld)
	{
		_isActive = !isOverworld;
		if (_isActive == false)
		{
			DropHeldObject();
			SetShowInput(false);
		}
		else
		{
			SetShowInput(true);
		}
	}
}
