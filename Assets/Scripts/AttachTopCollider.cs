using System.Collections;
using System.Collections.Generic;
using UnityEditor.UIElements;
using UnityEngine;

public class AttachTopCollider : MonoBehaviour
{
	private List<Collider> _colliders = new List<Collider>();
	private const string DYNAMIC_LAYER = "DynamicLevel";

	// Check if an object is entering/exiting top trigger
	private void OnTriggerEnter(Collider other)
	{
		int mask = LayerMask.NameToLayer(DYNAMIC_LAYER);
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
