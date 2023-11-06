using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PossessBehavior : MonoBehaviour
{
	private PlayerPawn _pawn = null;

	private const string POSSESS_TAG = "Possess";
	private GameObject _possessableObject = null;
	private PlayerPawn _possessablePawn = null;

	// Get the pawn of this gameobject
	private void Awake()
	{
		_pawn = GetComponent<PlayerPawn>();
	}

	// Check if this object is entering/exiting a possessable object
	private void OnTriggerEnter(Collider other)
	{
		if (other.tag != POSSESS_TAG) return;

		SetShowInput(false);
		_possessableObject = other.gameObject;
		_possessablePawn = _possessableObject.GetComponent<PlayerPawn>();
		SetShowInput(true);
	}
	private void OnTriggerExit(Collider other)
	{
		if (other.gameObject != _possessableObject) return;

		SetShowInput(false);
		_possessableObject = null;
		_possessablePawn = null;
	}

	// Possess function
	public void Possess()
    {
		if (_pawn == null || _pawn.Controller == null) return;

		_pawn.Controller.SwitchPawn(_possessablePawn);
		SetShowInput(false);

		// If this object is not controlled after switch, delete it
		if (_pawn.Controller == null)
		{
			Destroy(gameObject);
		}
	}

	// Helper function
	private void SetShowInput(bool isShown)
	{
		if (_possessablePawn && _possessableObject && _possessableObject.TryGetComponent<ShowInputFeedback>(out var comp))
		{
			comp.IsShown = isShown;
		}
	}
}
