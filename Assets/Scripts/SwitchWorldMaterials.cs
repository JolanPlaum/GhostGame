using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchWorldMaterials : MonoBehaviour
{
    [SerializeField] private Material _overworldMaterial;
    [SerializeField] private Material _underworldMaterial;

    [SerializeField] private List<Renderer> _attachedMaterials = new List<Renderer>();

    // Start is called before the first frame update
    void Start()
    {
        // Get all the renderers of all children of this component
        // BEWARE: if the materials are different at the start,
        //  they will become the same after a world change event
        if (_attachedMaterials.Count == 0)
        {

            Renderer[] renderers = transform.GetComponentsInChildren<Renderer>();
            foreach (Renderer renderer in renderers)
            {
                if (renderer)
                {
                    _attachedMaterials.Add(renderer);
                }
            }
        }

        // Bind material changes to world change event
        GameMode mode = FindObjectOfType<GameMode>();
        if (mode != null)
        {
            mode.OnWorldChanged += UpdateMaterials;
            UpdateMaterials(mode.IsOverworld);
        }
	}

	// Switches the materials of all children according to the world status
	private void UpdateMaterials(bool isOverworld)
    {
        if (_overworldMaterial != null && _underworldMaterial != null)
        {
            Material worldMat = isOverworld ? _overworldMaterial : _underworldMaterial;

            for (int i = 0; i < _attachedMaterials.Count; i++)
            {
                if (_attachedMaterials[i])
                {
                    _attachedMaterials[i].material = worldMat;
                }
            }
        }
    }
}
