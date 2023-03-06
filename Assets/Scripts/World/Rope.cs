using UnityEngine;
using System.Linq;

namespace Kekw.World
{

    /// <summary>
    /// Creates rope "mesh" along given points.
    /// Adds and removes line renderer automagically to keep object manipulation easy.
    /// </summary>
    public class Rope : MonoBehaviour
    {
        [SerializeField]
        [Tooltip("Rope points")]
        GameObject[] _points;

        [SerializeField]
        [Tooltip("Rope width")]
        float _width;

        [SerializeField]
        [Tooltip("Material")]
        Material _material;

        LineRenderer _lineRenderer;

        // Start is called before the first frame update
        void Start()
        {
            _lineRenderer = this.gameObject.AddComponent<LineRenderer>();
            _lineRenderer = GetComponent<LineRenderer>();
            // Set positions
            _lineRenderer.positionCount = _points.Length;
            _lineRenderer.SetPositions(_points.Select(x => { return x.transform.position; }).ToArray());
            // Set even width
            _lineRenderer.startWidth = _width;
            _lineRenderer.endWidth = _width;
            // set material
            _lineRenderer.material = _material;
        }

        // Remove line renderer
        private void OnDestroy() => _lineRenderer = null;


        private void FixedUpdate()
        {
            // Update positions in sync with physics
            _lineRenderer.SetPositions(_points.Select(x => { return x.transform.position; }).ToArray());
        }
    } 
}
