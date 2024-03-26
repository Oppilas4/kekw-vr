using UnityEngine;

namespace Gardening
{
    public class SeedPacket : MonoBehaviour
    {
        public Plant associatedPlant;
        [SerializeField]
        private ParticleSystem _seedParticles;
        [SerializeField]
        private int _dropAngle;

        private void Update()
        {
            var emission = _seedParticles.emission;
            // Stops emission instead of turning off. This prevents delay on particle spawn
            // when re-enabling, as well as particles suddenly disappearing
            emission.enabled = CheckTilt();
        }

        /// <summary>
        /// Checks if the seed packet's rotation is suitable for dropping seeds
        /// </summary>
        private bool CheckTilt()
        {
            var angles = transform.rotation.eulerAngles;
            // Prevents the seed packet from acting weirdly with large rotation values
            int modulo = 360 - _dropAngle; 
            return Mathf.Abs(angles.x) % modulo >= _dropAngle || Mathf.Abs(angles.z) % modulo >= _dropAngle;
        }
    }
}
