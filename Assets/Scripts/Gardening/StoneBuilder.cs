using System.Linq;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

namespace Gardening
{
	public class StoneBuilder : MonoBehaviour
	{
		private const string BuildableTag = "Buildable";

		private void Start()
		{
			foreach (var buildable in GameObject.FindGameObjectsWithTag(BuildableTag))
			{
				MaterialVault.Instance.AddMaterial(buildable.GetComponent<MeshRenderer>().material);
			}

			GetComponent<XRGrabInteractable>().selectExited.AddListener(Build);
		}
	
		public void Build(SelectExitEventArgs args)
		{
			// choose bricks
			var brickToPaintCollider = Physics.
			                           SphereCastAll(transform.position, 0.5f, Vector3.down).
			                           Where(x => x.collider.CompareTag(BuildableTag)).
			                           OrderBy(x => Vector3.Distance(transform.position, x.transform.position)).FirstOrDefault().collider;
			if (brickToPaintCollider is null)
				return;
		
			brickToPaintCollider.tag = "Untagged";
			var meshRenderer = brickToPaintCollider.GetComponent<MeshRenderer>();
			meshRenderer.material = MaterialVault.Instance.GetOpaqueMaterial(meshRenderer.material);
			Destroy(gameObject);
		}
	}
}
