using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PossessPlayerPawn : PlayerPawn
{
	[SerializeField] private GameObject _defaultPlayer;
	[SerializeField] private Transform _spawnPosition;

	// Unpossess behavior
	public override void Action2()
	{
		if (Controller == null) return;

		// Spawn new pawn and switch controller to it
		GameObject newPlayer = Instantiate(_defaultPlayer, _spawnPosition.position, Quaternion.identity);
		PlayerPawn pawn = newPlayer.GetComponent<PlayerPawn>();

		Controller.SwitchPawn(pawn);

		// If this object is still controlled after switch, delete the newly created object
		if (Controller != null)
		{
			Destroy(newPlayer);
		}
	}
}
