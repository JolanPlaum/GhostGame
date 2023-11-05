using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadNextScene : MonoBehaviour
{
	[SerializeField] private bool _resetCurrentScene = false;
	[SerializeField] private int _sceneIndex = -1;
	[SerializeField] private float _loadDelay = 1.0f;

	// Check if this object is near/far a grabbable object
	private void OnTriggerEnter(Collider other)
	{
		if (other.isTrigger == true) return;
        if (other.GetComponent<GhostPlayerPawn>() == null) return;

		Invoke(nameof(Load), _loadDelay);
	}

	// Load the next scene
	private void Load()
	{
		int buildIdx;

		// If no specific scene is set inside the editor, just go to the next scene in line
		if (_resetCurrentScene)
			buildIdx = SceneManager.GetActiveScene().buildIndex;
		else if (_sceneIndex <= -1)
			buildIdx = SceneManager.GetActiveScene().buildIndex + 1;
		else
			buildIdx = _sceneIndex;

		// Load the actual scene
		SceneManager.LoadScene(buildIdx);
	}
}
