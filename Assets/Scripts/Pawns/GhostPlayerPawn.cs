using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostPlayerPawn : PlayerPawn
{
	private GameMode _gameMode;

	// Overworld behaviors
	private DashBehavior _dashBehavior;
	private PossessBehavior _possessBehavior;


	// Get the extra behaviors
	protected override void Awake()
	{
		base.Awake();

		_dashBehavior = GetComponent<DashBehavior>();
		_possessBehavior = GetComponent<PossessBehavior>();
	}

	// Find the game mode in the scene
	void Start()
	{
		_gameMode = FindObjectOfType<GameMode>();
	}

	// World specific actions
	public override void Action1()
	{
		//_dashBehavior.Dash();
	}
	public override void Action2()
	{
		//_possessBehavior.Possess();
	}
}
