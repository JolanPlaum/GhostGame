using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttachTopCollider : MonoBehaviour
{
	private List<Collider> _colliders = new List<Collider>();
	private const string DYNAMIC_LAYER = "DynamicLevel";

	// Check if an object is entering/exiting top trigger
	private void OnTriggerEnter(Collider other)
	{
		if (other.isTrigger == false && other.gameObject.layer == LayerMask.NameToLayer(DYNAMIC_LAYER))
		{
			_colliders.Add(other);
			other.transform.SetParent(transform);
		}
	}
	private void OnTriggerExit(Collider other)
	{
		if (_colliders.Remove(other))
		{
			other.transform.SetParent(null);
		}
	}
}
