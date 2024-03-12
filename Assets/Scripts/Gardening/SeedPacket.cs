using UnityEngine;

namespace Gardening
{
    public class SeedPacket : MonoBehaviour
    {
        [SerializeField]
        private ParticleSystem _seedParticles;
        [SerializeField]
        private Transform _seedDropPoint;

        private bool _dropping;

        private void Update()
        {
            _dropping = CheckTilt();
            _seedParticles.gameObject.SetActive(_dropping);
            if (_dropping)
            {
                DropSeeds();
            }
        }

        private void DropSeeds()
        {
            if (CheckCollisionWithPot())
            {
                // TODO
            }
        }

        private bool CheckTilt()
        {
            return Mathf.Abs(transform.rotation.eulerAngles.x) >= 100 || Mathf.Abs(transform.rotation.eulerAngles.z) >= 100;
        }

        private bool CheckCollisionWithPot()
        {
            var down = _seedDropPoint.TransformDirection(Vector3.down);
            if (Physics.Raycast(_seedDropPoint.position, down, out RaycastHit hit, Mathf.Infinity))
            {
                if (hit.collider.CompareTag("FlowerPot")) return true;
            }
            return false;
        }
    }
}
