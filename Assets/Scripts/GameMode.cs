using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameMode : MonoBehaviour
{
    [SerializeField] private bool _isOverworld = true;
    public bool IsOverworld
    {
        get { return _isOverworld; }
        set
        {
            _isOverworld = value;
			OnWorldChanged?.Invoke(_isOverworld);
        }
    }

	public delegate void WorldChange(bool isOverworld);
    public event WorldChange OnWorldChanged;

	// Update is called once per frame
	void Update()
	{
        // Close the game
		if (Input.GetKeyDown(KeyCode.Escape))
			Application.Quit();

		// Reset the level
        if (Input.GetKeyDown(KeyCode.R) || Input.GetKeyDown(KeyCode.JoystickButton5))
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
	}
}
