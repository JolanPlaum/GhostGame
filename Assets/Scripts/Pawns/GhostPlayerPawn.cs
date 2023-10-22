using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostPlayerPawn : PlayerPawn
{
	private GameMode _gameMode;

	// Overworld behaviors
	private DashBehavior _dashBehavior;
	private PossessBehavior _possessBehavior;

	// Underworld behaviors
	private InteractBehavior _interactBehavior;


	// Get the extra behaviors
	protected override void Awake()
	{
		base.Awake();

		_dashBehavior = GetComponent<DashBehavior>();
		_possessBehavior = GetComponent<PossessBehavior>();

		_interactBehavior = GetComponent<InteractBehavior>();
	}

	// Find the game mode in the scene
	void Start()
	{
		_gameMode = FindObjectOfType<GameMode>();
	}

	// Override base actions
	public override void Move(Vector2 input)
	{
		if (_dashBehavior && _dashBehavior.IsDashing) return;

		base.Move(input);
	}
	public override void Jump()
	{
		if (_dashBehavior && _dashBehavior.IsDashing) return;

		base.Jump();
	}

	// World specific actions
	public override void Action1()
	{
		if (_gameMode == null) return;

		// Overworld behavior
		if (_gameMode.IsOverworld)
		{
			if (_dashBehavior == null) return;
			if (_dashBehavior.Dash() == false) return;

			if (_movementBehavior)
			{
				_movementBehavior.DesiredMovementDirection = Vector3.zero;
				_movementBehavior.DesiredRotationDirection = Vector3.zero;
			}
		}

		// Underworld behavior
		else
		{
			if (_interactBehavior == null) return;
			_interactBehavior.Interact();
		}
	}
	public override void Action2()
	{
		//_possessBehavior.Possess();
	}
}
