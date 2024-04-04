using UnityEngine;

namespace Gardening
{
    public class IvyNode
    {
        private Vector3 _position;
        private Vector3 _normal;

        public IvyNode(Vector3 position, Vector3 normal)
        {
            _position = position;
            _normal = normal;
        }

        public Vector3 GetPosition() => _position;
        public Vector3 GetNormal() => _normal;
    }
}
