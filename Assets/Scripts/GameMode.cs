using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
		if (Input.GetKeyDown(KeyCode.Escape))
			Application.Quit();
	}
}
