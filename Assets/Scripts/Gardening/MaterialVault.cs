using System.Collections.Generic;
using UnityEngine;

namespace Gardening
{
	public class MaterialVault : MonoBehaviour
	{
		public static MaterialVault Instance
		{
			get;
			private set;
		}
		// contains transparent material and corresponding opaque material
		private                 Dictionary<Material, Material> _materials = new();
		private static readonly int                            BaseColor  = Shader.PropertyToID("_BaseColor");

		private void Awake() 
		{ 
			// If there is an instance, and it's not me, delete myself.
    
			if (Instance != null && Instance != this) 
			{ 
				Destroy(this); 
			} 
			else 
			{ 
				Instance = this; 
			} 
		}
	
		public void AddMaterial(Material transparentMaterial)
		{
			if (_materials.ContainsKey(transparentMaterial)) return;
		
			var newColor = transparentMaterial.GetColor(BaseColor);
			newColor.a = 1;
		
			var opaqueMaterial = new Material(transparentMaterial);
			opaqueMaterial.SetColor(BaseColor, newColor);
		
			_materials.Add(transparentMaterial, opaqueMaterial);
		}
	
		public Material GetOpaqueMaterial(Material transparentMaterial)
		{
			return !_materials.ContainsKey(transparentMaterial) ? null : _materials[transparentMaterial];
		}
	}
}
