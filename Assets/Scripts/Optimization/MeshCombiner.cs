using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Optimization
{
    /// <summary>
    /// Editor tool for combining meshes inside unity editor.
    /// </summary>
    public class MeshCombiner : MonoBehaviour
    {
        [SerializeField]
        [Header("Source meshes")]
        [Tooltip("Meshes that are combined")]
        MeshFilter[] _sourceMeshes;


        [SerializeField]
        [Header("Target meshes")]
        [Tooltip("Target mesh filter where ")]
        MeshFilter _combinedMesh;

        [SerializeField]
        [Header("Options")]
        [Tooltip("Should this combining be performed at start?")]
        bool _performAtSceneStart = true;

        [SerializeField]
        [Tooltip("Should seperate mesh gameobjects be deactivated.")]
        bool _boolDeactivateOldMesh = true;

        [SerializeField]
        [Tooltip("Enable this if source meshes have lightmap data")]
        bool _hasLightMapData = false;

        private void Start()
        {
            if (_performAtSceneStart)
            {
                Combine();
            }
        }

        /// <summary>
        /// This is available in component 3 dot menu inside inspector.
        /// </summary>
        [ContextMenu("Combine meshes")]
        private void Combine()
        {
            CombineInstance[] combine = new CombineInstance[_sourceMeshes.Length];

            for (int i = 0; i < _sourceMeshes.Length; i++)
            {
                combine[i].mesh = _sourceMeshes[i].sharedMesh;
                combine[i].transform = _sourceMeshes[i].transform.localToWorldMatrix;
                if (_performAtSceneStart)
                {
                    _sourceMeshes[i].gameObject.SetActive(false);
                }
            }

            Mesh newMesh = new Mesh();

            if (_hasLightMapData)
            {
                newMesh.CombineMeshes(combine, true, true, true);
            }
            else
            {
                newMesh.CombineMeshes(combine);
            }
            _combinedMesh.mesh = newMesh;
        }
    }
}