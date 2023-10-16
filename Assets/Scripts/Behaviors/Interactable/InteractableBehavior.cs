using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableBehavior : MonoBehaviour
{
	private bool _isSelected;
	public bool IsSelected
	{
		get { return _isSelected; }
		set { _isSelected = value; }
	}

	// Public interface
	public virtual void Execute()
	{

	}
}
