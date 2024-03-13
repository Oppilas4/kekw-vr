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
            var emission = _seedParticles.emission;
            emission.enabled = _dropping;
        }

        private bool CheckTilt()
        {
            var angles = transform.rotation.eulerAngles;
            int dropAngle = 100;
            int modulo = (360 - dropAngle);
            return Mathf.Abs(angles.x) % modulo >= dropAngle || Mathf.Abs(angles.z) % modulo >= dropAngle;
        }
    }
}
