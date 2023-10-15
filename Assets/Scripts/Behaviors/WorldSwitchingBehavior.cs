using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldSwitchingBehavior : MonoBehaviour
{
	private GameMode _gameMode = null;
	private const string WORLD_SWITCH_TAG = "WorldSwitch";
	private bool _isInsideTrigger = false;

	// Check if this object is entering/exiting a world switch
	private void OnTriggerEnter(Collider other)
	{
		if (other.tag != WORLD_SWITCH_TAG) return;

		_isInsideTrigger = true;
	}
	private void OnTriggerExit(Collider other)
	{
		if (other.tag != WORLD_SWITCH_TAG) return;

		_isInsideTrigger = false;
	}

	// Find game mode in the scene
	void Awake()
	{
		_gameMode = FindObjectOfType<GameMode>();
	}

	// Switch worlds
	public void WorldSwitch()
	{
		if (_isInsideTrigger == false) return;

		if (_gameMode)
		{
			_gameMode.IsOverworld = !_gameMode.IsOverworld;
		}
	}
}
