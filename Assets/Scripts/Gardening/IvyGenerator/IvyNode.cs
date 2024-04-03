using UnityEngine;

namespace Gardening
{
    public class IvyNode
    {
        Vector3 position;
        Vector3 normal;

        public IvyNode(Vector3 position, Vector3 normal)
        {
            this.position = position;
            this.normal = normal;
        }

        public Vector3 GetPosition() => position;
        public Vector3 GetNormal() => normal;

    }
}
