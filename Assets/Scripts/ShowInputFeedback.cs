using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowInputFeedback : MonoBehaviour
{
	[SerializeField] private float _delayTime = 0f;
	[SerializeField] private GameObject _inputFeedback = null;
	private float _timer;

	private bool _isShown;
	public bool IsShown
	{
		get { return _isShown; }
		set
		{
			_isShown = value;

			if (_isShown == false)
			{
				_timer = 0f;
				if (_inputFeedback) _inputFeedback.SetActive(false);
			}
		}
	}

	// At the start of lifetime
	private void Start()
	{
		if (_inputFeedback) _inputFeedback.SetActive(false);
	}

	// Show input after the update loop is done
	private void LateUpdate()
	{
		if (_inputFeedback == null) return;

		if (_isShown)
		{
			if (_timer < _delayTime)
			{
				_timer += Time.deltaTime;
			}
			else
			{
				_inputFeedback.SetActive(true);
			}
		}
	}
}
