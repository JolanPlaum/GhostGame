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
	private GrabBehavior _grabBehavior;


	// Get the extra behaviors
	protected override void Awake()
	{
		base.Awake();

		_dashBehavior = GetComponent<DashBehavior>();
		_possessBehavior = GetComponent<PossessBehavior>();

		_interactBehavior = GetComponent<InteractBehavior>();
		_grabBehavior = GetComponent<GrabBehavior>();
	}

	// Find the game mode in the scene
	void Start()
	{
		_gameMode = FindObjectOfType<GameMode>();
	}

	// Update to let the behaviors know whether they are active
	private void Update()
	{
		if (_gameMode == null) return;
		bool isOnGround = !(_jumpBehavior && _jumpBehavior.IsOnGround == false);

		if (_possessBehavior)
			_possessBehavior.IsActive = isOnGround && _gameMode.IsOverworld;

		if (_grabBehavior)
			_grabBehavior.IsActive = isOnGround && !_gameMode.IsOverworld;

		if (_interactBehavior)
			_interactBehavior.IsActive = isOnGround && !_gameMode.IsOverworld;
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
	public override void WorldSwitch()
	{
		if (_jumpBehavior && _jumpBehavior.IsOnGround == false) return;

		base.WorldSwitch();
	}

	// World specific actions
	public override void Action1()
	{
		if (_gameMode == null) return;
		if (_jumpBehavior && _jumpBehavior.IsOnGround == false) return;

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
		if (_gameMode == null) return;
		if (_jumpBehavior && _jumpBehavior.IsOnGround == false) return;

		// Overworld behavior
		if (_gameMode.IsOverworld)
		{
			_possessBehavior.Possess();
		}

		// Underworld behavior
		else
		{
			_grabBehavior.Grab();
		}
	}
}
