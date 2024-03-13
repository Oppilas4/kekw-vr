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
            if (_dropping)
            {
                DropSeeds();
            }
            var emission = _seedParticles.emission;
            emission.enabled = _dropping;
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
            var angles = transform.rotation.eulerAngles;
            int dropAngle = 100;
            int modulo = (360 - dropAngle);
            return Mathf.Abs(angles.x) % modulo >= dropAngle || Mathf.Abs(angles.z) % modulo >= dropAngle;
        }

        private bool CheckCollisionWithPot()
        {
            var down = _seedDropPoint.TransformDirection(Vector3.down);
            Debug.DrawRay(_seedDropPoint.position, down, Color.green);
            if (Physics.Raycast(_seedDropPoint.position, down, out RaycastHit hit, Mathf.Infinity))
            {
                if (hit.collider.CompareTag("FlowerPot")) return true;
            }
            return false;
        }
    }
}
