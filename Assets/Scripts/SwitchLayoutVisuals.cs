using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchLayoutVisuals : MonoBehaviour
{
	[SerializeField] private GameObject _keyboardFeedback = null;
	[SerializeField] private GameObject _gamepadFeedback = null;
	private PlayerController _playerController;

	private void Start()
	{
		_playerController = FindObjectOfType<PlayerController>();
	}

	private void LateUpdate()
	{
		if (_playerController)
		{
			if (_playerController.IsKeyboard)
			{
				if (_keyboardFeedback) _keyboardFeedback.SetActive(true);
				if (_gamepadFeedback) _gamepadFeedback.SetActive(false);
			}
			else
			{
				if (_keyboardFeedback) _keyboardFeedback.SetActive(false);
				if (_gamepadFeedback) _gamepadFeedback.SetActive(true);
			}
		}
	}
}
