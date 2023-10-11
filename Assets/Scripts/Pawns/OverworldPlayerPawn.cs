using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OverworldPlayerPawn : PlayerPawn
{
	private DashBehavior _dashBehavior;
	private PossessBehavior _possessBehavior;

	// Get the extra behaviors
	protected override void Awake()
	{
		base.Awake();

		_dashBehavior = GetComponent<DashBehavior>();
		_possessBehavior = GetComponent<PossessBehavior>();
	}

	// Overworld specific actions
	public override void Action1()
	{
		//_dashBehavior.Dash();
	}
	public override void Action2()
	{
		//_possessBehavior.Possess();
	}
}
