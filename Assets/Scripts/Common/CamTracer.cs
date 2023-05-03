using UnityEngine;

namespace Kekw.Common
{
    /// <summary>
    /// Rotates object towards main camera around y axis.
    /// </summary>
    public class CamTracer: MonoBehaviour
    {
        private void Update()
        {
            this.transform.LookAt(Camera.main.transform.position);
        }
    }
}
